namespace MyCompilerV2.View
{
    partial class NewFormView
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
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.textBox_path = new System.Windows.Forms.TextBox();
            this.button_cancel = new System.Windows.Forms.Button();
            this.checkBox_subdirectory = new System.Windows.Forms.CheckBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_chooseFolder = new System.Windows.Forms.Button();
            this.groupBox_newProject = new System.Windows.Forms.GroupBox();
            this.label_folder = new System.Windows.Forms.Label();
            this.label_name = new System.Windows.Forms.Label();
            this.groupBox_newProject.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(4, 45);
            this.textBox_name.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(241, 20);
            this.textBox_name.TabIndex = 0;
            // 
            // textBox_path
            // 
            this.textBox_path.Enabled = false;
            this.textBox_path.Location = new System.Drawing.Point(4, 93);
            this.textBox_path.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox_path.Name = "textBox_path";
            this.textBox_path.Size = new System.Drawing.Size(241, 20);
            this.textBox_path.TabIndex = 1;
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(138, 232);
            this.button_cancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(104, 26);
            this.button_cancel.TabIndex = 2;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // checkBox_subdirectory
            // 
            this.checkBox_subdirectory.AutoSize = true;
            this.checkBox_subdirectory.Location = new System.Drawing.Point(50, 179);
            this.checkBox_subdirectory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox_subdirectory.Name = "checkBox_subdirectory";
            this.checkBox_subdirectory.Size = new System.Drawing.Size(117, 17);
            this.checkBox_subdirectory.TabIndex = 3;
            this.checkBox_subdirectory.Text = "Create subdirectory";
            this.checkBox_subdirectory.UseVisualStyleBackColor = true;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(16, 232);
            this.button_ok.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(104, 26);
            this.button_ok.TabIndex = 4;
            this.button_ok.Text = "Ok";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_chooseFolder
            // 
            this.button_chooseFolder.Location = new System.Drawing.Point(56, 131);
            this.button_chooseFolder.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button_chooseFolder.Name = "button_chooseFolder";
            this.button_chooseFolder.Size = new System.Drawing.Size(111, 24);
            this.button_chooseFolder.TabIndex = 5;
            this.button_chooseFolder.Text = "Choose Folder";
            this.button_chooseFolder.UseVisualStyleBackColor = true;
            this.button_chooseFolder.Click += new System.EventHandler(this.button_chooseFolder_Click);
            // 
            // groupBox_newProject
            // 
            this.groupBox_newProject.Controls.Add(this.label_folder);
            this.groupBox_newProject.Controls.Add(this.button_chooseFolder);
            this.groupBox_newProject.Controls.Add(this.checkBox_subdirectory);
            this.groupBox_newProject.Controls.Add(this.label_name);
            this.groupBox_newProject.Controls.Add(this.textBox_name);
            this.groupBox_newProject.Controls.Add(this.textBox_path);
            this.groupBox_newProject.Location = new System.Drawing.Point(9, 10);
            this.groupBox_newProject.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox_newProject.Name = "groupBox_newProject";
            this.groupBox_newProject.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox_newProject.Size = new System.Drawing.Size(254, 210);
            this.groupBox_newProject.TabIndex = 6;
            this.groupBox_newProject.TabStop = false;
            this.groupBox_newProject.Text = "New project";
            // 
            // label_folder
            // 
            this.label_folder.AutoSize = true;
            this.label_folder.Location = new System.Drawing.Point(4, 77);
            this.label_folder.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_folder.Name = "label_folder";
            this.label_folder.Size = new System.Drawing.Size(39, 13);
            this.label_folder.TabIndex = 3;
            this.label_folder.Text = "Folder:";
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Location = new System.Drawing.Point(4, 28);
            this.label_name.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(38, 13);
            this.label_name.TabIndex = 2;
            this.label_name.Text = "Name:";
            // 
            // NewFormView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 270);
            this.Controls.Add(this.groupBox_newProject);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.button_cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "NewFormView";
            this.Text = "New Project";
            this.groupBox_newProject.ResumeLayout(false);
            this.groupBox_newProject.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.TextBox textBox_path;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.CheckBox checkBox_subdirectory;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_chooseFolder;
        private System.Windows.Forms.GroupBox groupBox_newProject;
        private System.Windows.Forms.Label label_folder;
        private System.Windows.Forms.Label label_name;
    }
}