using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using ModLib;
using DATExtract;
using HProjectLib;

namespace DATPacker
{
    internal partial class Project
    {
        

        private static uint GetIDFromName(string fileName)
        {
            string number = "";
            foreach (char letter in fileName)
            {
                if (char.IsNumber(letter))
                {
                    number += letter;
                }
            }
            if (number.Length > 0)
            {
                return uint.Parse(number);
            }

            return 0;
        }

        private static void PopulateLists(string fileName, Dictionary<long, bool> existingFiles, List<VirtualFile> rootFiles, Dictionary<string, List<string>> directories, Dictionary<long, List<VirtualFile>> allFiles, byte thisArchiveId)
        {
            existingFiles.Add(Hash.Calculate(fileName), true);
            string parentDir = Path.GetDirectoryName(fileName);
            var vFile = new VirtualFile(Path.GetFileName(fileName), thisArchiveId);

            string[] split = GetSplit(fileName);
            if (split.Length == 1)
            {
                rootFiles.Add(vFile);
                return;
            }

            string root = split[0];
            string remainder = split[1];
            if (!directories.ContainsKey(root))
            {
                directories.Add(root, new List<string>());
            }
            long parentHash = Hash.Calculate(parentDir);
            if (!allFiles.ContainsKey(parentHash))
            {
                directories[root].Add(remainder);
                allFiles.Add(parentHash, new List<VirtualFile>());
            }

            allFiles[parentHash].Add(vFile);
        }

        private static string SeekBackwards(string fileName)
        {
            for (int i = fileName.Length - 1; i >= 0; i--)
            {
                if (fileName[i] == '\\')
                {
                    return fileName.Substring(0, i);
                }
            }

            return "";
        }

        private static string[] GetSplit(string fileName)
        {

            string[] rawSplit = fileName.Split('\\', 3);
            if (rawSplit.Length == 2)
            {
                return new string[] { rawSplit[1] };
            }

            string[] split = new string[2];
            split[0] = rawSplit[1];

            string parentDir = SeekBackwards(rawSplit[2]);
            if (parentDir != "")
            {
                split[1] = parentDir;
                return split;
            }

            return split;
        }

        private static int DeclareSegment(string segment, ModFile nameTable, ModFile filenameTable, Dictionary<long, int> folderIds, long hash, int parentId, int totalFolders)
        {
            folderIds.Add(hash, totalFolders);

            long position = nameTable.Position;
            nameTable.WriteString(segment, 1);

            filenameTable.WriteInt(parentId);
            filenameTable.WriteInt((int)position);
            filenameTable.WriteByte(0xFF); // Declares that this is a folder

            return totalFolders + 1;
        }

        private static void DeclareFile(string segment, ModFile nameTable, ModFile filenameTable, int parentId, byte archiveId)
        {
            long position = nameTable.Position;
            nameTable.WriteString(segment, 1);

            filenameTable.WriteInt(parentId);
            filenameTable.WriteInt((int)position);
            filenameTable.WriteByte(archiveId);
        }

        private struct VirtualFile
        {
            public string Name;
            public byte ArchiveId;

            public VirtualFile(string Name, byte ArchiveId)
            {
                this.Name = Name;
                this.ArchiveId = ArchiveId;
            }
        }

        private static void WriteDATPackData(string outputFolder, string buildFolder, ModFile projectFile, Dictionary<string, List<string>> directories, Dictionary<long, List<VirtualFile>> allFiles, List<VirtualFile> rootFiles, Dictionary<string, byte> archives)
        {
            using (ModFile fileTable = ModFile.Create(), nameTable = ModFile.Create(), archiveTable = ModFile.Create())
            {
                Dictionary<long, int> folderIds = new();
                int totalFolders = 1; // root is 0
                uint totalSegments = 0;
                int totalFiles = 0;

                foreach (KeyValuePair<string, List<string>> rootDirectory in directories)
                {
                    rootDirectory.Value.Sort();

                    long rootHash = Hash.Calculate('\\' + rootDirectory.Key);
                    totalFolders = DeclareSegment(rootDirectory.Key, nameTable, fileTable, folderIds, rootHash, 0, totalFolders);
                    totalSegments++;
                    foreach (string directory in rootDirectory.Value)
                    {
                        long currentHash;
                        if (directory == null || directory == "")
                        {
                            currentHash = rootHash;
                        }
                        else
                        {
                            string[] segmented = directory.Split('\\');
                            if (segmented.Length == 1)
                            {
                                currentHash = Hash.Extend(rootHash, directory);
                                totalFolders = DeclareSegment(directory, nameTable, fileTable, folderIds, currentHash, folderIds[rootHash], totalFolders);
                                totalSegments++;
                            }
                            else
                            {
                                currentHash = rootHash;
                                for (int i = 0; i < segmented.Length; i++)
                                {
                                    int parentId = folderIds[currentHash];
                                    currentHash = Hash.Extend(currentHash, segmented[i]);
                                    if (!folderIds.ContainsKey(currentHash))
                                    {
                                        totalFolders = DeclareSegment(segmented[i], nameTable, fileTable, folderIds, currentHash, parentId, totalFolders);
                                        totalSegments++;
                                    }
                                }
                            }
                        }

                        int fileParentId = folderIds[currentHash];

                        foreach (VirtualFile vFile in allFiles[currentHash])
                        {
                            DeclareFile(vFile.Name, nameTable, fileTable, fileParentId, vFile.ArchiveId);
                            totalSegments++;
                            totalFiles++;
                        }
                    }
                }

                foreach (VirtualFile vFile in rootFiles)
                {
                    DeclareFile(vFile.Name, nameTable, fileTable, 0, vFile.ArchiveId);
                    totalSegments++;
                    totalFiles++;
                }

                archiveTable.WriteByte((byte)archives.Count);
                foreach (KeyValuePair<string, byte> archivePair in archives)
                {
                    archiveTable.WriteString(archivePair.Key, 1);
                    archiveTable.WriteByte(archivePair.Value);
                }

                archiveTable.Seek(0, SeekOrigin.Begin);
                archiveTable.fileStream.CopyTo(projectFile.fileStream);

                projectFile.WriteString(outputFolder, 1);
                projectFile.WriteString(buildFolder, 1);

                projectFile.WriteUint(totalSegments);
                projectFile.WriteUint((uint)totalFolders);
                projectFile.WriteUint((uint)totalFiles);
                projectFile.WriteUint((uint)nameTable.Position);

                nameTable.Seek(0, SeekOrigin.Begin);
                nameTable.fileStream.CopyTo(projectFile.fileStream);

                fileTable.Seek(0, SeekOrigin.Begin);
                fileTable.fileStream.CopyTo(projectFile.fileStream);

            }
        }

        public static string CreateNew(string projectName, string mainFolder, string outputFolder, string patchFolder, string buildFolder)
        {
            Logger.ToConsole("Let's get this show on the road.");
            Logger.Log(new LogSeg("Preparing new project "), new LogSeg(projectName, ConsoleColor.DarkCyan));

            Logger.ToConsole("Looking for archives in main directory:");

            List<string> primaryDatArchives = new();

            foreach (string datArchive in Directory.GetFiles(mainFolder))
            {
                if (datArchive.ToLower().EndsWith(".dat"))
                {
                    primaryDatArchives.Add(datArchive);
                    
                    Logger.Log(new LogSeg("+ ", ConsoleColor.Green), new LogSeg(Path.GetFileName(datArchive)));
                }
            }

            Logger.ToConsole("Found {0} primary archives.", primaryDatArchives.Count);


            List<string> secondaryDatArchives = new();

            if (patchFolder != null && patchFolder != "")
            {
                Logger.ToConsole("Looking for archives in update directory:");

                foreach (string datArchive in Directory.GetFiles(patchFolder))
                {
                    string lowered = Path.GetFileName(datArchive.ToLower());
                    if (lowered.StartsWith("patch") && lowered.EndsWith(".dat"))
                    {
                        secondaryDatArchives.Add(datArchive);

                        Logger.Log(new LogSeg("+ ", ConsoleColor.Green), new LogSeg(Path.GetFileName(datArchive)));
                    }
                }
                
                Logger.ToConsole("Found {0} update archives. \nBeginning extraction.", secondaryDatArchives.Count);
            }
            else
            {
                Logger.Warn("You did not provide an update location. Turn back now if you have updated your game as the project will not be valid without it.");
            }

            ExtractWorker.extractLocation = outputFolder;

            Dictionary<string, DATFile> datFiles = new();

            foreach (string datArchive in primaryDatArchives)
            {
                string datRaw = Path.GetFileName(datArchive);
                Logger.ToConsole(datRaw + ":");
                DATFile file = DATFile.Open(datArchive);
                datFiles.Add(datRaw, file);
                ExtractWorker.AddPrimaryTask(file);
            }

            Dictionary<string, DATFile> secondaryFiles = new();

            foreach (string patchArchive in secondaryDatArchives)
            { // Patch archives must be extracted after main GAME archives.
                string datRaw = Path.GetFileName(patchArchive);
                Logger.ToConsole(datRaw + ":");
                DATFile file = DATFile.Open(patchArchive);
                secondaryFiles.Add(datRaw, file);
                ExtractWorker.AddSecondaryTask(file);
            }

            // Set the little children off on their merry way and we do some work by ourselves.
            ExtractWorker worker = new ExtractWorker();
            worker.Setup();
            worker.Start();

            using (ModFile dpChunk = ModFile.Create())
            {
                Dictionary<string, byte> archives = new();
                byte lastDeclared = 0;
                foreach (KeyValuePair<string, DATFile> datFile in datFiles)
                {
                    archives.Add(datFile.Key, lastDeclared);
                    lastDeclared++;
                }

                uint lastArchiveNumber = 0;
                Dictionary<string, List<string>> directories = new();
                Dictionary<long, List<VirtualFile>> allFiles = new();
                List<VirtualFile> rootFiles = new();

                Dictionary<long, bool> existingFiles = new();
                foreach (KeyValuePair<string, DATFile> datFilePair in datFiles)
                {
                    string datFileName = datFilePair.Key;
                    DATFile datFile = datFilePair.Value;

                    byte thisArchiveId = archives[datFilePair.Key];
                    for (int i = 0; i < datFile.files.Length; i++)
                    {
                        PopulateLists(datFile.files[i].path, existingFiles, rootFiles, directories, allFiles, thisArchiveId);
                    }
                    lastArchiveNumber = GetIDFromName(datFileName);
                }

                Dictionary<string, List<string>> secondaryOverviews = new();
                foreach (KeyValuePair<string, DATFile> datFilePair in secondaryFiles)
                {
                    string datFileName = datFilePair.Key;
                    DATFile datFile = datFilePair.Value;

                    List<string> files = new();
                    for (int i = 0; i < datFile.files.Length; i++)
                    {
                        files.Add(datFile.files[i].path);
                    }
                    secondaryOverviews.Add(datFileName, files);
                }

                datFiles.Clear();
                secondaryFiles.Clear();

                foreach (KeyValuePair<string, List<string>> filesPair in secondaryOverviews)
                {
                    List<string> files = filesPair.Value;
                    for (int i = files.Count - 1; i >= 0; i--)
                    { // Safe way of iterating while removing
                        string fileName = files[i];
                        if (existingFiles.ContainsKey(Hash.Calculate(fileName)))
                        {
                            files.RemoveAt(i);
                        }
                    }

                    string thisArchiveName = "GAME" + (lastArchiveNumber + 1) + ".DAT";
                    lastArchiveNumber++;
                    archives.Add(thisArchiveName, lastDeclared);
                    byte thisArchiveId = lastDeclared;
                    lastDeclared++;
                    for (int i = 0; i < files.Count; i++)
                    {
                        PopulateLists(files[i], existingFiles, rootFiles, directories, allFiles, thisArchiveId);
                    }
                }

                WriteDATPackData(outputFolder, buildFolder, dpChunk, directories, allFiles, rootFiles, archives);

                return HProject.CreateProject(projectName, outputFolder, "DATPACK", 1, dpChunk);
            }
        }

        public static DPProject Load(string projectFileLocation)
        {
            Logger.ToConsole("Opening project file: {0}", Path.GetFileName(projectFileLocation));

            HProject hProject = HProject.LoadPartial(projectFileLocation, "DATPACK");
            DPProject project = new DPProject(hProject, projectFileLocation);
            using (ModFile projectFile = hProject.SegmentFile) {
                Dictionary<string, FileList> archives = new();
                Dictionary<byte, FileList> allFiles = new();

                byte archiveCount = projectFile.ReadByte();
                for (int i = 0; i < archiveCount; i++)
                {
                    string archiveName = projectFile.ReadNullString();
                    byte archiveId = projectFile.ReadByte();
                    FileList fileList = new FileList();
                    archives.Add(archiveName, fileList);
                    allFiles.Add(archiveId, fileList);
                }

                project.OutputLocation = projectFile.ReadNullString();
                project.BuildLocation = projectFile.ReadNullString();
                project.Files = archives;

                uint segmentCount = projectFile.ReadUint();
                uint totalFolders = projectFile.ReadUint();
                uint totalFiles = projectFile.ReadUint();
                uint nameTableSize = projectFile.ReadUint();

                Logger.Log(new LogSeg("Project "), new LogSeg(hProject.ProjectName, ConsoleColor.DarkCyan), new LogSeg(" acknowledges "), new LogSeg(totalFiles.ToString(), ConsoleColor.Magenta), new LogSeg(" files. Please be patient."));

                long nameTableStart = projectFile.Position;
                projectFile.Seek(nameTableSize, SeekOrigin.Current);

                string[] qualifiedPaths = new string[totalFolders + 1]; // 0 is root
                int lastDeclaredFolder = 1;
                qualifiedPaths[0] = "\\";
                for (int i = 0; i < segmentCount; i++)
                {
                    int parentId = projectFile.ReadInt();
                    int namePos = projectFile.ReadInt();
                    byte archiveId = projectFile.ReadByte();

                    long returnPoint = projectFile.Position;

                    projectFile.Seek(nameTableStart + namePos, SeekOrigin.Begin);
                    string segment = projectFile.ReadNullString();
                    if (archiveId == 0xff) // folder
                    {
                        qualifiedPaths[lastDeclaredFolder] = qualifiedPaths[parentId] + segment + '\\';
                        lastDeclaredFolder++;
                    }
                    else
                    {
                        allFiles[archiveId].Add(qualifiedPaths[parentId] + segment);
                    }
                    projectFile.Seek(returnPoint, SeekOrigin.Begin);
                }

                Logger.Log("Successfully read project file.");
            }

            return project;
        }

        public static void Save(DPProject project)
        {
            Logger.Log("Saving project file.");

            Dictionary<string, byte> archives = new();
            byte lastDeclared = 0;
            foreach (KeyValuePair<string, FileList> archive in project.Files)
            {
                archives.Add(archive.Key, lastDeclared);
                lastDeclared++;
            }

            Dictionary<string, List<string>> directories = new();
            Dictionary<long, List<VirtualFile>> allFiles = new();
            List<VirtualFile> rootFiles = new();
            Dictionary<long, bool> existingFiles = new();
            foreach (KeyValuePair<string, FileList> datFilePair in project.Files)
            {
                FileList datFile = datFilePair.Value;

                byte thisArchiveId = archives[datFilePair.Key];
                for (int i = 0; i < datFile.Length; i++)
                {
                    PopulateLists(datFile[i], existingFiles, rootFiles, directories, allFiles, thisArchiveId);
                }
            }

            using (ModFile dpChunk = ModFile.Create())
            {
                WriteDATPackData(project.OutputLocation, project.BuildLocation, dpChunk, directories, allFiles, rootFiles, archives);
                HProject.SaveProjectSegment(project.ProjectFileLocation, "DATPACK", 1, dpChunk);
            }

            if (project.NameNeedsSaving)
            {
                HProject.RenameProject(project.ProjectFileLocation, project.ProjectName);
                project.NameNeedsSaving = false;
            }

            project.NeedsSaving = false;

            Logger.Log("Project file saved.");
        }
    }
}
