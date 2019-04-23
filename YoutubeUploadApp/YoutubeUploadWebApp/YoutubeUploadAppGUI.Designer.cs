namespace YoutubeUploadApp
{
    partial class YoutubeUploadAppGUI
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
            this.FilePathTextLabel = new System.Windows.Forms.Label();
            this.FilePathTextBox = new System.Windows.Forms.TextBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.VideoTitleTextLabel = new System.Windows.Forms.Label();
            this.VideoTitleTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.VideoDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.UploadVideoButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FilePathTextLabel
            // 
            this.FilePathTextLabel.AutoSize = true;
            this.FilePathTextLabel.Location = new System.Drawing.Point(13, 13);
            this.FilePathTextLabel.Name = "FilePathTextLabel";
            this.FilePathTextLabel.Size = new System.Drawing.Size(51, 13);
            this.FilePathTextLabel.TabIndex = 1;
            this.FilePathTextLabel.Text = "File Path:";
            // 
            // FilePathTextBox
            // 
            this.FilePathTextBox.Location = new System.Drawing.Point(86, 10);
            this.FilePathTextBox.Name = "FilePathTextBox";
            this.FilePathTextBox.ReadOnly = true;
            this.FilePathTextBox.Size = new System.Drawing.Size(461, 20);
            this.FilePathTextBox.TabIndex = 2;
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(565, 10);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(75, 21);
            this.BrowseButton.TabIndex = 3;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // VideoTitleTextLabel
            // 
            this.VideoTitleTextLabel.AutoSize = true;
            this.VideoTitleTextLabel.Location = new System.Drawing.Point(13, 47);
            this.VideoTitleTextLabel.Name = "VideoTitleTextLabel";
            this.VideoTitleTextLabel.Size = new System.Drawing.Size(60, 13);
            this.VideoTitleTextLabel.TabIndex = 4;
            this.VideoTitleTextLabel.Text = "Video Title:";
            // 
            // VideoTitleTextBox
            // 
            this.VideoTitleTextBox.Location = new System.Drawing.Point(86, 44);
            this.VideoTitleTextBox.Name = "VideoTitleTextBox";
            this.VideoTitleTextBox.Size = new System.Drawing.Size(454, 20);
            this.VideoTitleTextBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Video Desc.";
            // 
            // VideoDescriptionTextBox
            // 
            this.VideoDescriptionTextBox.Location = new System.Drawing.Point(86, 76);
            this.VideoDescriptionTextBox.Multiline = true;
            this.VideoDescriptionTextBox.Name = "VideoDescriptionTextBox";
            this.VideoDescriptionTextBox.Size = new System.Drawing.Size(454, 110);
            this.VideoDescriptionTextBox.TabIndex = 7;
            // 
            // UploadVideoButton
            // 
            this.UploadVideoButton.Location = new System.Drawing.Point(86, 193);
            this.UploadVideoButton.Name = "UploadVideoButton";
            this.UploadVideoButton.Size = new System.Drawing.Size(75, 23);
            this.UploadVideoButton.TabIndex = 8;
            this.UploadVideoButton.Text = "Upload";
            this.UploadVideoButton.UseVisualStyleBackColor = true;
            this.UploadVideoButton.Click += new System.EventHandler(this.UploadVideoButton_Click);
            // 
            // YoutubeUploadAppGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.UploadVideoButton);
            this.Controls.Add(this.VideoDescriptionTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.VideoTitleTextBox);
            this.Controls.Add(this.VideoTitleTextLabel);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.FilePathTextBox);
            this.Controls.Add(this.FilePathTextLabel);
            this.Name = "YoutubeUploadAppGUI";
            this.Text = "YoutubeUploadAppGUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label FilePathTextLabel;
        private System.Windows.Forms.TextBox FilePathTextBox;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label VideoTitleTextLabel;
        private System.Windows.Forms.TextBox VideoTitleTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox VideoDescriptionTextBox;
        private System.Windows.Forms.Button UploadVideoButton;
    }
}