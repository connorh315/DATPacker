namespace DATPacker
{
    partial class ProjectSettings
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
            this.BuildFolderButton = new System.Windows.Forms.Button();
            this.BuildFolderTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ProjectNameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.OutputFolderButton = new System.Windows.Forms.Button();
            this.OutputFolderTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BuildFolderButton
            // 
            this.BuildFolderButton.Location = new System.Drawing.Point(446, 109);
            this.BuildFolderButton.Name = "BuildFolderButton";
            this.BuildFolderButton.Size = new System.Drawing.Size(27, 27);
            this.BuildFolderButton.TabIndex = 22;
            this.BuildFolderButton.UseVisualStyleBackColor = true;
            this.BuildFolderButton.Click += new System.EventHandler(this.BuildFolderButton_Click);
            // 
            // BuildFolderTextBox
            // 
            this.BuildFolderTextBox.AllowDrop = true;
            this.BuildFolderTextBox.Location = new System.Drawing.Point(163, 109);
            this.BuildFolderTextBox.Name = "BuildFolderTextBox";
            this.BuildFolderTextBox.PlaceholderText = "A:\\Dimensions\\buildfolder";
            this.BuildFolderTextBox.Size = new System.Drawing.Size(284, 27);
            this.BuildFolderTextBox.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 20);
            this.label5.TabIndex = 20;
            this.label5.Text = "Build folder";
            // 
            // ProjectNameTextBox
            // 
            this.ProjectNameTextBox.AllowDrop = true;
            this.ProjectNameTextBox.Location = new System.Drawing.Point(163, 46);
            this.ProjectNameTextBox.Name = "ProjectNameTextBox";
            this.ProjectNameTextBox.PlaceholderText = "My Dimensions Project";
            this.ProjectNameTextBox.Size = new System.Drawing.Size(284, 27);
            this.ProjectNameTextBox.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 20);
            this.label4.TabIndex = 15;
            this.label4.Text = "Project name";
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(148, 334);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(200, 73);
            this.SaveButton.TabIndex = 29;
            this.SaveButton.Text = "Save project settings";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // OutputFolderButton
            // 
            this.OutputFolderButton.Location = new System.Drawing.Point(446, 169);
            this.OutputFolderButton.Name = "OutputFolderButton";
            this.OutputFolderButton.Size = new System.Drawing.Size(27, 27);
            this.OutputFolderButton.TabIndex = 28;
            this.OutputFolderButton.UseVisualStyleBackColor = true;
            this.OutputFolderButton.Click += new System.EventHandler(this.OutputFolderButton_Click);
            // 
            // OutputFolderTextBox
            // 
            this.OutputFolderTextBox.AllowDrop = true;
            this.OutputFolderTextBox.Location = new System.Drawing.Point(163, 169);
            this.OutputFolderTextBox.Name = "OutputFolderTextBox";
            this.OutputFolderTextBox.PlaceholderText = "A:\\Dimensions\\outputfiles";
            this.OutputFolderTextBox.Size = new System.Drawing.Size(284, 27);
            this.OutputFolderTextBox.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 20);
            this.label3.TabIndex = 26;
            this.label3.Text = "Output folder";
            // 
            // ProjectSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 453);
            this.Controls.Add(this.BuildFolderButton);
            this.Controls.Add(this.BuildFolderTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ProjectNameTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.OutputFolderButton);
            this.Controls.Add(this.OutputFolderTextBox);
            this.Controls.Add(this.label3);
            this.Name = "ProjectSettings";
            this.Text = "Project Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button BuildFolderButton;
        private TextBox BuildFolderTextBox;
        private Label label5;
        private TextBox ProjectNameTextBox;
        private Label label4;
        private Button SaveButton;
        private Button OutputFolderButton;
        private TextBox OutputFolderTextBox;
        private Label label3;
    }
}