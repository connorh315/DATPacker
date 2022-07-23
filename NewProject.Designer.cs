namespace DATPacker
{
    partial class NewProject
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.MainFolderTextBox = new System.Windows.Forms.TextBox();
            this.MainFolderButton = new System.Windows.Forms.Button();
            this.PatchFolderButton = new System.Windows.Forms.Button();
            this.PatchFolderTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.OutputFolderButton = new System.Windows.Forms.Button();
            this.OutputFolderTextBox = new System.Windows.Forms.TextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.VideoTutorialButton = new System.Windows.Forms.Button();
            this.TextTutorialButton = new System.Windows.Forms.Button();
            this.ProjectNameTextBox = new System.Windows.Forms.TextBox();
            this.BuildFolderButton = new System.Windows.Forms.Button();
            this.BuildFolderTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "DAT archive folder";
            this.toolTip1.SetToolTip(this.label1, "Directory containing main GAME.DAT files");
            // 
            // MainFolderTextBox
            // 
            this.MainFolderTextBox.AllowDrop = true;
            this.MainFolderTextBox.Location = new System.Drawing.Point(165, 127);
            this.MainFolderTextBox.Name = "MainFolderTextBox";
            this.MainFolderTextBox.PlaceholderText = "A:\\Dimensions\\gamedatfiles";
            this.MainFolderTextBox.Size = new System.Drawing.Size(284, 27);
            this.MainFolderTextBox.TabIndex = 3;
            this.MainFolderTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox_DragDrop);
            this.MainFolderTextBox.DragOver += new System.Windows.Forms.DragEventHandler(this.textBox_DragOver);
            // 
            // MainFolderButton
            // 
            this.MainFolderButton.Location = new System.Drawing.Point(448, 127);
            this.MainFolderButton.Name = "MainFolderButton";
            this.MainFolderButton.Size = new System.Drawing.Size(27, 27);
            this.MainFolderButton.TabIndex = 4;
            this.MainFolderButton.UseVisualStyleBackColor = true;
            this.MainFolderButton.Click += new System.EventHandler(this.MainFolderButton_Click);
            // 
            // PatchFolderButton
            // 
            this.PatchFolderButton.Location = new System.Drawing.Point(448, 247);
            this.PatchFolderButton.Name = "PatchFolderButton";
            this.PatchFolderButton.Size = new System.Drawing.Size(27, 27);
            this.PatchFolderButton.TabIndex = 10;
            this.PatchFolderButton.UseVisualStyleBackColor = true;
            this.PatchFolderButton.Click += new System.EventHandler(this.PatchFolderButton_Click);
            // 
            // PatchFolderTextBox
            // 
            this.PatchFolderTextBox.AllowDrop = true;
            this.PatchFolderTextBox.Location = new System.Drawing.Point(165, 247);
            this.PatchFolderTextBox.Name = "PatchFolderTextBox";
            this.PatchFolderTextBox.PlaceholderText = "A;\\Dimensions\\patchdatfiles";
            this.PatchFolderTextBox.Size = new System.Drawing.Size(284, 27);
            this.PatchFolderTextBox.TabIndex = 9;
            this.PatchFolderTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox_DragDrop);
            this.PatchFolderTextBox.DragOver += new System.Windows.Forms.DragEventHandler(this.textBox_DragOver);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 250);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Update folder";
            this.toolTip1.SetToolTip(this.label2, "Directory containing PATCH.DAT archives");
            // 
            // toolTip1
            // 
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 304);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Output folder";
            this.toolTip1.SetToolTip(this.label3, "Directory where archives will be extracted to");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Project name";
            this.toolTip1.SetToolTip(this.label4, "Name of your project. Make it something nice!");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "Build folder";
            this.toolTip1.SetToolTip(this.label5, "Directory to build custom archives into\r\n\r\n");
            // 
            // OutputFolderButton
            // 
            this.OutputFolderButton.Location = new System.Drawing.Point(448, 301);
            this.OutputFolderButton.Name = "OutputFolderButton";
            this.OutputFolderButton.Size = new System.Drawing.Size(27, 27);
            this.OutputFolderButton.TabIndex = 13;
            this.OutputFolderButton.UseVisualStyleBackColor = true;
            this.OutputFolderButton.Click += new System.EventHandler(this.OutputFolderButton_Click);
            // 
            // OutputFolderTextBox
            // 
            this.OutputFolderTextBox.AllowDrop = true;
            this.OutputFolderTextBox.Location = new System.Drawing.Point(165, 301);
            this.OutputFolderTextBox.Name = "OutputFolderTextBox";
            this.OutputFolderTextBox.PlaceholderText = "A:\\Dimensions\\outputfiles";
            this.OutputFolderTextBox.Size = new System.Drawing.Size(284, 27);
            this.OutputFolderTextBox.TabIndex = 12;
            this.OutputFolderTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox_DragDrop);
            this.OutputFolderTextBox.DragOver += new System.Windows.Forms.DragEventHandler(this.textBox_DragOver);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(150, 359);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(200, 73);
            this.StartButton.TabIndex = 14;
            this.StartButton.Text = "Create project";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // VideoTutorialButton
            // 
            this.VideoTutorialButton.Location = new System.Drawing.Point(12, 12);
            this.VideoTutorialButton.Name = "VideoTutorialButton";
            this.VideoTutorialButton.Size = new System.Drawing.Size(132, 29);
            this.VideoTutorialButton.TabIndex = 15;
            this.VideoTutorialButton.Text = "Video tutorial";
            this.VideoTutorialButton.UseVisualStyleBackColor = true;
            // 
            // TextTutorialButton
            // 
            this.TextTutorialButton.Location = new System.Drawing.Point(165, 12);
            this.TextTutorialButton.Name = "TextTutorialButton";
            this.TextTutorialButton.Size = new System.Drawing.Size(132, 29);
            this.TextTutorialButton.TabIndex = 16;
            this.TextTutorialButton.Text = "Text tutorial";
            this.TextTutorialButton.UseVisualStyleBackColor = true;
            // 
            // ProjectNameTextBox
            // 
            this.ProjectNameTextBox.AllowDrop = true;
            this.ProjectNameTextBox.Location = new System.Drawing.Point(165, 71);
            this.ProjectNameTextBox.Name = "ProjectNameTextBox";
            this.ProjectNameTextBox.PlaceholderText = "My Dimensions Project";
            this.ProjectNameTextBox.Size = new System.Drawing.Size(284, 27);
            this.ProjectNameTextBox.TabIndex = 1;
            // 
            // BuildFolderButton
            // 
            this.BuildFolderButton.Location = new System.Drawing.Point(448, 188);
            this.BuildFolderButton.Name = "BuildFolderButton";
            this.BuildFolderButton.Size = new System.Drawing.Size(27, 27);
            this.BuildFolderButton.TabIndex = 7;
            this.BuildFolderButton.UseVisualStyleBackColor = true;
            this.BuildFolderButton.Click += new System.EventHandler(this.BuildFolderButton_Click);
            // 
            // BuildFolderTextBox
            // 
            this.BuildFolderTextBox.AllowDrop = true;
            this.BuildFolderTextBox.Location = new System.Drawing.Point(165, 188);
            this.BuildFolderTextBox.Name = "BuildFolderTextBox";
            this.BuildFolderTextBox.PlaceholderText = "A:\\Dimensions\\buildfolder";
            this.BuildFolderTextBox.Size = new System.Drawing.Size(284, 27);
            this.BuildFolderTextBox.TabIndex = 6;
            this.BuildFolderTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox_DragDrop);
            this.BuildFolderTextBox.DragOver += new System.Windows.Forms.DragEventHandler(this.textBox_DragOver);
            // 
            // NewProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 453);
            this.Controls.Add(this.BuildFolderButton);
            this.Controls.Add(this.BuildFolderTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ProjectNameTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TextTutorialButton);
            this.Controls.Add(this.VideoTutorialButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.OutputFolderButton);
            this.Controls.Add(this.OutputFolderTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PatchFolderButton);
            this.Controls.Add(this.PatchFolderTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MainFolderButton);
            this.Controls.Add(this.MainFolderTextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewProject";
            this.Text = "New Project";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox MainFolderTextBox;
        private Button MainFolderButton;
        private Button PatchFolderButton;
        private TextBox PatchFolderTextBox;
        private Label label2;
        private ToolTip toolTip1;
        private Button OutputFolderButton;
        private TextBox OutputFolderTextBox;
        private Label label3;
        private Button StartButton;
        private Button VideoTutorialButton;
        private Button TextTutorialButton;
        private TextBox ProjectNameTextBox;
        private Label label4;
        private Button BuildFolderButton;
        private TextBox BuildFolderTextBox;
        private Label label5;
    }
}