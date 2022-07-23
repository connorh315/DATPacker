using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DATPacker
{
    public partial class ProjectSettings : Form
    {
        public ProjectSettings()
        {
            InitializeComponent();

            BuildFolderButton.Image = IconExtractor.Extract(@"shell32.dll", 45, false).ToBitmap();

            OutputFolderButton.Image = IconExtractor.Extract(@"shell32.dll", 45, false).ToBitmap();

            DPProject currentProject = Main.currentProject;

            ProjectNameTextBox.Text = currentProject.ProjectName;
            BuildFolderTextBox.Text = currentProject.BuildLocation;
            OutputFolderTextBox.Text = currentProject.OutputLocation;
        }

        private void ButtonClick(TextBox textBox)
        {
            var dialog = new FolderPicker();
            if (Directory.Exists(textBox.Text))
            {
                dialog.InputPath = textBox.Text;
            }
            if (dialog.ShowDialog(Handle) == true)
            {
                textBox.Text = dialog.ResultPath;
            }
        }

        private void BuildFolderButton_Click(object sender, EventArgs e)
        {
            ButtonClick(BuildFolderTextBox);
        }

        private void OutputFolderButton_Click(object sender, EventArgs e)
        {
            ButtonClick(OutputFolderTextBox);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            DPProject currentProject = Main.currentProject;

            if (ProjectNameTextBox.Text.Trim() == "") { MessageBox.Show("Project name is required!"); return; }
            if (BuildFolderTextBox.Text.Trim() == "") { MessageBox.Show("Build folder is required!"); return; }
            if (OutputFolderTextBox.Text.Trim() == "") { MessageBox.Show("Output folder is required!"); return; }

            bool needsSaving = false;
            if (currentProject.ProjectName != ProjectNameTextBox.Text)
            {
                currentProject.ProjectName = ProjectNameTextBox.Text;
                currentProject.NeedsSaving = currentProject.NameNeedsSaving = needsSaving = true;
            }
            if (currentProject.BuildLocation != BuildFolderTextBox.Text)
            {
                currentProject.BuildLocation = BuildFolderTextBox.Text;
                currentProject.NeedsSaving = needsSaving = true;
            }
            if (currentProject.OutputLocation != OutputFolderTextBox.Text)
            {
                currentProject.OutputLocation = OutputFolderTextBox.Text;
                currentProject.NeedsSaving = needsSaving = true;
                Overwatch.WatchLocation(currentProject.OutputLocation);
            }

            if (needsSaving)
            {
                var shouldSave = MessageBox.Show("Would you like to save your project to file now?", "Save project now?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (shouldSave == DialogResult.Yes)
                {
                    Project.Save(currentProject);
                }
            }

            this.Close();
        }
    }
}
