using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModLib;

namespace DATPacker
{
    internal class ArchiveTaskQueue
    {
        private static Dictionary<string, List<ArchiveTask>> archiveTasks = new Dictionary<string, List<ArchiveTask>>();

        public static void AddTask(ArchiveTask task)
        {
            DPProject project = Main.currentProject;

            if (task.Type == ArchiveTaskType.Created)
            {
                KeyValuePair<string, List<ArchiveTask>> archive = archiveTasks.Last();
                archive.Value.Add(task);
                Main.TriggerUpdate(archive.Key);
            }
            else
            {
                string path = task.Type == ArchiveTaskType.Renamed ? task.OldPath : task.Path;
                foreach (KeyValuePair<string, FileList> archiveFiles in project.Files)
                {
                    if (archiveFiles.Value.Contains(path))
                    {
                        List<ArchiveTask> tasks = archiveTasks[archiveFiles.Key];
                        tasks.Add(task);
                        Main.TriggerUpdate(archiveFiles.Key);
                        return;
                    }
                }

                Logger.Error("Could not find file {0} in archives.", path);
            }
        }

        private static void WarnDesync()
        {
            Logger.Warn("De-synchronisation may have occurred. If you experience issues with getting files to work in game, you may need to Re-sync files!");
        }

        public static void Setup(Dictionary<string, FileList> archives)
        {
            archiveTasks.Clear();

            foreach (KeyValuePair<string, FileList> archive in archives)
            {
                archiveTasks.Add(archive.Key, new List<ArchiveTask>());
            }
        }
    }
}
