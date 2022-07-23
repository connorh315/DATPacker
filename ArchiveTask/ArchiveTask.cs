using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATPacker
{
    public enum ArchiveTaskType
    {
        Created,
        Deleted,
        Renamed,
        Edited
    }

    internal class ArchiveTask
    {
        public string Path;
        public ArchiveTaskType Type;

        public string OldPath;

        public ArchiveTask(string Path, ArchiveTaskType Type)
        {
            this.Path = Path;
            this.Type = Type;
        }

        public ArchiveTask(string Path, string OldPath, ArchiveTaskType Type)
        {
            this.Path = Path;
            this.Type = Type;
            this.OldPath = OldPath;
        }
    }
}
