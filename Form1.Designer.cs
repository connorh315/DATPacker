namespace DATPacker
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.FolderTextBox = new System.Windows.Forms.TextBox();
            this.SelectFolderButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SelectFileButton = new System.Windows.Forms.Button();
            this.FileTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SelectPatchButton = new System.Windows.Forms.Button();
            this.PatchTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folder to pack:";
            // 
            // FolderTextBox
            // 
            this.FolderTextBox.AllowDrop = true;
            this.FolderTextBox.Location = new System.Drawing.Point(243, 63);
            this.FolderTextBox.Name = "FolderTextBox";
            this.FolderTextBox.Size = new System.Drawing.Size(463, 27);
            this.FolderTextBox.TabIndex = 1;
            this.FolderTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
            this.FolderTextBox.DragOver += new System.Windows.Forms.DragEventHandler(this.textBox1_DragOver);
            // 
            // SelectFolderButton
            // 
            this.SelectFolderButton.Location = new System.Drawing.Point(203, 61);
            this.SelectFolderButton.Name = "SelectFolderButton";
            this.SelectFolderButton.Size = new System.Drawing.Size(33, 29);
            this.SelectFolderButton.TabIndex = 2;
            this.SelectFolderButton.UseVisualStyleBackColor = true;
            this.SelectFolderButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SelectFolderButton_MouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output file:";
            // 
            // SelectFileButton
            // 
            this.SelectFileButton.Location = new System.Drawing.Point(203, 135);
            this.SelectFileButton.Name = "SelectFileButton";
            this.SelectFileButton.Size = new System.Drawing.Size(33, 29);
            this.SelectFileButton.TabIndex = 5;
            this.SelectFileButton.UseVisualStyleBackColor = true;
            this.SelectFileButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SelectFileButton_MouseClick);
            // 
            // FileTextBox
            // 
            this.FileTextBox.AllowDrop = true;
            this.FileTextBox.Location = new System.Drawing.Point(243, 136);
            this.FileTextBox.Name = "FileTextBox";
            this.FileTextBox.Size = new System.Drawing.Size(463, 27);
            this.FileTextBox.TabIndex = 4;
            this.FileTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
            this.FileTextBox.DragOver += new System.Windows.Forms.DragEventHandler(this.textBox1_DragOver);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(39, 267);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(194, 56);
            this.button1.TabIndex = 6;
            this.button1.Text = "Build";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button1_MouseClick);
            // 
            // SelectPatchButton
            // 
            this.SelectPatchButton.Location = new System.Drawing.Point(203, 207);
            this.SelectPatchButton.Name = "SelectPatchButton";
            this.SelectPatchButton.Size = new System.Drawing.Size(33, 29);
            this.SelectPatchButton.TabIndex = 9;
            this.SelectPatchButton.UseVisualStyleBackColor = true;
            this.SelectPatchButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SelectPatchButton_MouseClick);
            // 
            // PatchTextBox
            // 
            this.PatchTextBox.AllowDrop = true;
            this.PatchTextBox.Location = new System.Drawing.Point(243, 208);
            this.PatchTextBox.Name = "PatchTextBox";
            this.PatchTextBox.Size = new System.Drawing.Size(463, 27);
            this.PatchTextBox.TabIndex = 8;
            this.PatchTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox1_DragDrop);
            this.PatchTextBox.DragOver += new System.Windows.Forms.DragEventHandler(this.textBox1_DragOver);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 211);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Update location:";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 451);
            this.Controls.Add(this.SelectPatchButton);
            this.Controls.Add(this.PatchTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SelectFileButton);
            this.Controls.Add(this.FileTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SelectFolderButton);
            this.Controls.Add(this.FolderTextBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "DATPacker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox FolderTextBox;
        private Button SelectFolderButton;
        private Label label2;
        private Button SelectFileButton;
        private TextBox FileTextBox;
        private Button button1;
        private Button SelectPatchButton;
        private TextBox PatchTextBox;
        private Label label3;
    }
}