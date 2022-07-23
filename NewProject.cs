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
    public partial class NewProject : Form
    {
        public NewProject()
        {
            InitializeComponent();

            MainFolderButton.Image = IconExtractor.Extract(@"shell32.dll", 45, false).ToBitmap();

            BuildFolderButton.Image = IconExtractor.Extract(@"shell32.dll", 45, false).ToBitmap();

            PatchFolderButton.Image = IconExtractor.Extract(@"shell32.dll", 45, false).ToBitmap();

            OutputFolderButton.Image = IconExtractor.Extract(@"shell32.dll", 45, false).ToBitmap();
        }

        private void toolTip1_Popup_1(object sender, PopupEventArgs e)
        {

        }

        private void textBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0)
            {
                ((TextBox)sender).Text = files[0];
            }
        }

        private void textBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (ProjectNameTextBox.Text.Trim() == "") { MessageBox.Show("Project name is required!"); return; }
            if (MainFolderTextBox.Text.Trim() == "") { MessageBox.Show("DAT archive folder is required!"); return; }
            if (BuildFolderTextBox.Text.Trim() == "") { MessageBox.Show("Build folder is required!"); return; }
            if (OutputFolderTextBox.Text.Trim() == "") { MessageBox.Show("Output folder is required!"); return; }

            DialogResult getStarted = MessageBox.Show("This process requires roughly 30GB on your output folder drive.\nAre you ready to continue?", "Ready?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (getStarted == DialogResult.OK)
            {
                string projectFileLocation = Project.CreateNew(ProjectNameTextBox.Text, MainFolderTextBox.Text, OutputFolderTextBox.Text, PatchFolderTextBox.Text, BuildFolderTextBox.Text);

                Main.currentProject = Project.Load(projectFileLocation);

                this.Close();
            }
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

        private void MainFolderButton_Click(object sender, EventArgs e)
        {
            ButtonClick(MainFolderTextBox);
        }

        private void BuildFolderButton_Click(object sender, EventArgs e)
        {
            ButtonClick(BuildFolderTextBox);
        }

        private void PatchFolderButton_Click(object sender, EventArgs e)
        {
            ButtonClick(PatchFolderTextBox);
        }

        private void OutputFolderButton_Click(object sender, EventArgs e)
        {
            ButtonClick(OutputFolderTextBox);
        }
    }
}
