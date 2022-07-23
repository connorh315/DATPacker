using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HProjectLib;

namespace DATPacker
{
    public partial class DPProject
    {
        public string ProjectName;

        public string OutputLocation;

        public string BuildLocation;

        public Dictionary<string, FileList> Files = new();

        public string ProjectFileLocation;

        public bool NeedsSaving = false;

        public bool NameNeedsSaving = false;

        public DPProject(HProject hProject, string projectFileLocation)
        {
            ProjectName = hProject.ProjectName;
            ProjectFileLocation = projectFileLocation;
        }
    }
}
