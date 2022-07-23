using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ModLib;

namespace DATPacker
{
    public partial class Main : Form
    {
        public static DPProject currentProject;

        public Main()
        {
            InitializeComponent();
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProject form = new NewProject();
            form.ShowDialog(this);
            if (currentProject != null)
            {
                ProjectSetup();

                MessageBox.Show("Project file has finished building! Please read the following message boxes for important information.", "Project done.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MessageBox.Show("Please move any PATCH*.DAT files away from Cemu/RPCS3 to ensure they are not loaded, otherwise your files will not load in game!", "Move update files", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                var startFirstBuild = MessageBox.Show("All archives need to be built for the first time, are you ready to do this now? They will be placed in the build location you specified.", "First build", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (startFirstBuild == DialogResult.OK)
                {
                    BuildAll();
                }
                else
                {
                    MessageBox.Show("You can build the archives using Build->Build all archives when you are ready.", "Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private static Dictionary<string, CustomProgressBar> progressBars;
        private static Dictionary<string, Button> buttons;
        private static Dictionary<string, bool> needsUpdating;

        private void ProjectSetup()
        {
            int archives = 0;

            progressBars = new();
            needsUpdating = new();
            buttons = new();

            foreach (KeyValuePair<string, FileList> archivePair in currentProject.Files)
            {
                Label archiveNameLabel = new Label();
                archiveNameLabel.Text = archivePair.Key;
                archiveNameLabel.TextAlign = ContentAlignment.MiddleLeft;
                archiveNameLabel.Bounds = new Rectangle(12, 20 + (40 * archives), 100, 30);
                MainPanel.Controls.Add(archiveNameLabel);

                CustomProgressBar progressBar = new CustomProgressBar();
                progressBar.CustomText = "No changes pending";
                progressBar.TextColor = Brushes.Black;
                progressBar.Bounds = new Rectangle(120, 20 + (40 * archives), 200, 30);
                MainPanel.Controls.Add(progressBar);
                progressBars.Add(archivePair.Key, progressBar);
                needsUpdating.Add(archivePair.Key, false);

                Button buildButton = new Button();
                buildButton.Text = "Build";
                buildButton.BackColor = SystemColors.ControlLightLight;
                buildButton.FlatStyle = FlatStyle.Standard;
                buildButton.Bounds = new Rectangle(328, 20 + (40 * archives), 100, 30);
                buildButton.Tag = archivePair.Key;
                buildButton.Click += BuildButton_Click;
                buttons.Add(archivePair.Key, buildButton);
                MainPanel.Controls.Add(buildButton);

                archives++;
            }

            Overwatch.WatchLocation(currentProject.OutputLocation);
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "hprj files (*.hprj)|*.hprj";
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentProject = Project.Load(openFileDialog.FileName);
                    ProjectSetup();
                }
            }
        }

        private void BuildButton_Click(object? sender, EventArgs e)
        {
            Button button = sender as Button;
            button.Enabled = false;
            string archiveName = (string)button.Tag;
            BuildWorker.Setup();
            BuildTask task = new BuildTask(currentProject, (string)button.Tag);
            task.ProgressBar = progressBars[archiveName];
            task.Button = button;
            BuildWorker.AddTask(task);
            BuildWorker.Start();
        }

        public static void TriggerUpdate(string archive)
        {
            CustomProgressBar progressBar = progressBars[archive];
            progressBar.CustomText = "Pending changes";
            progressBar.TextColor = Brushes.Red;
            progressBar.Invalidate();
            currentProject.NeedsSaving = true;
            needsUpdating[archive] = true;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentProject != null && currentProject.NeedsSaving)
            {
                var saveProject = MessageBox.Show("Your project file needs saving! Otherwise you will need to re-synchronise on your next use.\nWould you like to save now?", "Save project?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (saveProject == DialogResult.Yes)
                {
                    Project.Save(currentProject);
                }
            }
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentProject == null) return;

            Project.Save(currentProject);
        }

        private void BuildAll()
        {
            BuildWorker.Setup();
            foreach (KeyValuePair<string, FileList> file in currentProject.Files)
            {
                BuildTask task = new BuildTask(currentProject, file.Key);
                task.ProgressBar = progressBars[file.Key];
                Button button = buttons[file.Key];
                button.Enabled = false;
                task.Button = button;
                BuildWorker.AddTask(task);
            }
            BuildWorker.Start();
        }

        private void buildAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentProject == null) return;

            BuildAll();
        }

        private void resynchroniseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentProject == null) return;

            var messageBox = MessageBox.Show("Do not edit any files until this is complete!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (messageBox == DialogResult.OK)
            {
                Overwatch.Synchronise(currentProject);
            }
            else if (messageBox == DialogResult.Cancel)
            {
                Logger.Log("User cancelled re-synchronisation.");
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentProject == null) return;

            ProjectSettings projectSettings = new ProjectSettings();
            projectSettings.ShowDialog(this);
        }

        private void buildSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Placeholder. Serves no purpose currently.");
        }

        private void moveFilesInArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Placeholder. Serves no purpose currently.");
        }
    }
}
