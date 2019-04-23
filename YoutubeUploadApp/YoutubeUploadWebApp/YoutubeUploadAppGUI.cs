using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoutubeUploadApp
{
    public partial class YoutubeUploadAppGUI : Form
    {
        /// <summary>
        /// Action to handle upload Event
        /// </summary>
        public Action<string, string, string> Upload;

        /// <summary>
        /// Sets the title of the window
        /// </summary>
        public string Title { set { Text = value; } }

        /// <summary>
        /// Property that when set will display a message
        /// </summary>
        public string Message
        {
            set { MessageBox.Show(value); }
        }

        /// <summary>
        /// Auto generated constructor
        /// </summary>
        public YoutubeUploadAppGUI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When the browse button is clicked open a file dialog to select file to upload
        /// </summary>
        /// <param name="sender">NOT USED</param>
        /// <param name="e">NOT USED</param>
        private void BrowseButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "MP4 Videos (*.mp4)|*.mp4| All Files (*.*) | *.*";

            openFileDialog1.Title = "Open Video File";

            DialogResult result = this.openFileDialog1.ShowDialog();
            if (result == DialogResult.Yes || result == DialogResult.OK)
            {
                FilePathTextBox.Text = openFileDialog1.FileName;
            }
        }

        /// <summary>
        /// When the upload button is pressed, check to make sure that the parameters are filled out
        /// and calls the hooked event.
        /// </summary>
        /// <param name="sender">NOT USED</param>
        /// <param name="e">NOT USED</param>
        private void UploadVideoButton_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(FilePathTextBox.Text) || String.IsNullOrEmpty(VideoTitleTextBox.Text) || String.IsNullOrEmpty(VideoDescriptionTextBox.Text))
            {
                this.Message = "Check that the parameters are filled out";
                return;
            }

            Upload(FilePathTextBox.Text, VideoTitleTextBox.Text, VideoDescriptionTextBox.Text);
        }

        /// <summary>
        /// Closes the window
        /// </summary>
        public void DoClose()
        {
            Close();
        }

    }
}
