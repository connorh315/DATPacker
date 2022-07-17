using DATPack.Packer;

namespace DATPacker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            SelectFolderButton.Image = IconExtractor.Extract(@"shell32.dll", 45, false).ToBitmap();

            SelectFileButton.Image = IconExtractor.Extract(@"shell32.dll", 258, false).ToBitmap();

            SelectPatchButton.Image = IconExtractor.Extract(@"shell32.dll", 45, false).ToBitmap();

            //Patch.CheckSet();

            Console.WriteLine("Please read the Github README: https://github.com/connorh315/datpacker before continuing!");
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0)
            {
                ((TextBox)sender).Text = files[0];
            }
        }

        private void textBox1_DragOver(object sender, DragEventArgs e)
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

        private void SelectFolderButton_MouseClick(object sender, MouseEventArgs e)
        {
            var dialog = new FolderPicker();
            if (dialog.ShowDialog(Handle) == true)
            {
                FolderTextBox.Text = dialog.ResultPath;
            }
        }

        private void SelectFileButton_MouseClick(object sender, MouseEventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "DAT file|*.DAT";
                saveFileDialog.Title = "Set output location";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                {
                    FileTextBox.Text = saveFileDialog.FileName;
                }
            }
        }

        private void PackDAT()
        {
            string[] files = Pack.PackFromFolder(FolderTextBox.Text, ArchiveType.LegoDimensions, FileTextBox.Text);

            //if (PatchTextBox.Text != "")
            //{
            //    Pack.PatchFromFolder(PatchTextBox.Text, new string[0]);
            //}
            //else
            //{
            //    Console.WriteLine("No existing archives were edited, as a patch location was not given.");
            //}
        }

        private Thread packThread;

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            if (packThread == null)
            {
                packThread = new Thread(PackDAT);
                packThread.Start();
            }
            else if (packThread.ThreadState != ThreadState.Running)
            {
                packThread.Start();
            }

            MessageBox.Show("Please see console for progress.", "Progress", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SelectPatchButton_MouseClick(object sender, MouseEventArgs e)
        {
            var dialog = new FolderPicker();
            if (dialog.ShowDialog(Handle) == true)
            {
                PatchTextBox.Text = dialog.ResultPath;
            }
        }
    }
}