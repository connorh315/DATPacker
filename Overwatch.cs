using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModLib;

namespace DATPacker
{
    internal static class Overwatch
    {
        private static FileSystemWatcher watcher;

        public static Dictionary<string, bool> needsUpdating;

        public static void WatchLocation(string location)
        {
            if (watcher != null)
            {
                watcher.Dispose();
            }

            watcher = new FileSystemWatcher(location);

            watcher.NotifyFilter = NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Size;

            watcher.Changed += OnChanged;
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            watcher.Renamed += OnRenamed;
            watcher.Error += OnError;

            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;

            needsUpdating = new();
            foreach (KeyValuePair<string, FileList> archive in Main.currentProject.Files)
            {
                needsUpdating.Add(archive.Key, false);
            }

            Logger.Log("Overwatch initialized to {0}", location);
        }

        private static string GetRelativePath(string path)
        {
            return path.Substring(watcher.Path.Length, path.Length - watcher.Path.Length).ToLower();
        }

        // This is pretty poor, and will probably be complained about by anyone who for some reason didn't use a file extension.
        private static bool IsFolder(string path)
        {
            for (int i = path.Length - 1; i >= 0; i--)
            {
                if (path[i] == '.') return false;
                else if (path[i] == '\\') return true;
            }
            return true;
        }

        private static bool IsExcluded(string path)
        {
            if (path.EndsWith("hprj"))
            {
                return true;
            }

            return false;
        }

        private static Dictionary<string, bool> primaryFiles = new()
        {
            {"\\stuff\\game.txt", true },

        };

        private static bool IsPrimaryFile(string path)
        {
            return primaryFiles.ContainsKey(path);
        }

        private static void OnError(object sender, ErrorEventArgs e)
        {
            Logger.Log("Filesystem error raised: {0}", e.ToString());
        }

        private static void OnRenamed(object sender, RenamedEventArgs e)
        {
            if (IsExcluded(e.FullPath)) return;

            Logger.Log(new LogSeg("% ", ConsoleColor.Yellow), new LogSeg(GetRelativePath(e.OldFullPath)), new LogSeg(" -> ", ConsoleColor.Yellow), new LogSeg(GetRelativePath(e.FullPath)));

            string relativeOldPath = GetRelativePath(e.OldFullPath);
            string relativePath = GetRelativePath(e.FullPath);

            if ((File.GetAttributes(e.FullPath) & FileAttributes.Directory) == FileAttributes.Directory)
            {
                string[] childFiles = Directory.GetFiles(e.FullPath);
                foreach (string file in childFiles)
                {
                    string rawFile = file.Substring(e.FullPath.Length + 1);
                    string oldChildFile = Path.Combine(relativeOldPath, rawFile);
                    foreach (KeyValuePair<string, FileList> archiveFiles in Main.currentProject.Files)
                    {
                        if (archiveFiles.Value.Contains(oldChildFile))
                        {
                            string relNewFile = Path.Combine(relativePath, rawFile);
                            archiveFiles.Value.Remove(oldChildFile);
                            archiveFiles.Value.Add(relNewFile);
                            Main.TriggerUpdate(archiveFiles.Key);
                            break;
                        }
                    }
                }
                return;
            }

            foreach (KeyValuePair<string, FileList> archiveFiles in Main.currentProject.Files)
            {
                if (archiveFiles.Value.Contains(relativeOldPath))
                {
                    archiveFiles.Value.Remove(relativeOldPath);
                    archiveFiles.Value.Add(relativePath);
                    Main.TriggerUpdate(archiveFiles.Key);
                    return;
                }
            }
        }

        private static void OnDeleted(object sender, FileSystemEventArgs e)
        {
            if (IsExcluded(e.FullPath)) return;

            Logger.Log(new LogSeg("- ", ConsoleColor.Red), new LogSeg(GetRelativePath(e.FullPath)));

            string relativePath = GetRelativePath(e.FullPath);
            foreach (KeyValuePair<string, FileList> archiveFiles in Main.currentProject.Files)
            {
                if (archiveFiles.Value.Contains(relativePath))
                {
                    archiveFiles.Value.Remove(relativePath);
                    Main.TriggerUpdate(archiveFiles.Key);
                    return;
                }
            }
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            if (IsFolder(e.FullPath) || IsExcluded(e.FullPath)) return;

            Logger.Log(new LogSeg("+ ", ConsoleColor.Green), new LogSeg(GetRelativePath(e.FullPath)));

            string relativePath = GetRelativePath(e.FullPath);
            KeyValuePair<string, FileList> archive = Main.currentProject.Files.Last(); // Just shove it in the last archive. Needs some work though in case the file needs to go in GAME.DAT (such as game.txt)
            archive.Value.Add(relativePath);
            Main.TriggerUpdate(archive.Key);
            return;
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (IsFolder(e.FullPath) || IsExcluded(e.FullPath)) return;

            Logger.Log(new LogSeg("* ", ConsoleColor.Yellow), new LogSeg(GetRelativePath(e.FullPath)));

            string relativePath = GetRelativePath(e.FullPath);
            foreach (KeyValuePair<string, FileList> archiveFiles in Main.currentProject.Files)
            {
                if (archiveFiles.Value.Contains(relativePath))
                {
                    Main.TriggerUpdate(archiveFiles.Key);
                }
            }
        }

        public static void Synchronise(DPProject project)
        {
            Logger.Log("Resynchronising:");
            Logger.Log("Comparing files on disk to files in project.");

            Dictionary<long, bool> filesFound = new();
            int relativeStart = project.OutputLocation.Length;
            int addedFiles = 0;
            foreach (string file in Directory.EnumerateFiles(project.OutputLocation, "*.*", SearchOption.AllDirectories))
            {
                if (IsExcluded(file)) continue;

                string relativeFile = file.Substring(relativeStart);
                bool found = false;

                filesFound.Add(Hash.Calculate(relativeFile.ToLower()), true);

                foreach (KeyValuePair<string, FileList> archiveFiles in project.Files)
                {
                    if (archiveFiles.Value.Contains(relativeFile))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    project.Files.Last().Value.Add(relativeFile);
                    addedFiles++;
                    if (addedFiles < 100)
                    {
                        Logger.Log(new LogSeg("+ ", ConsoleColor.Green), new LogSeg(relativeFile));
                    }
                    else if (addedFiles == 100)
                    {
                        Logger.Log("No longer declaring new files.");
                    }
                }
            }

            int removedFiles = 0;
            foreach (KeyValuePair<string, FileList> archiveFiles in project.Files)
            {
                FileList list = archiveFiles.Value;
                int totalFiles = list.Length;
                for (int i = 0; i < totalFiles; i++)
                {
                    if (!filesFound.ContainsKey(Hash.Calculate(list[i].ToLower())))
                    {
                        Logger.Log(new LogSeg("- ", ConsoleColor.Red), new LogSeg(list[i]));
                        list.Remove(list[i]);
                        totalFiles--;
                        removedFiles++;
                    }
                }
            }

            Logger.Log("{0} files removed!", removedFiles);
            Logger.Log("{0} files added!", addedFiles);
            Logger.Log("Finished resynchronising!");

            if (addedFiles != 0 || removedFiles != 0)
            {
                project.NeedsSaving = true;    
            }
        }
    }
}
