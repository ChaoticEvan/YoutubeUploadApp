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
        /// Open file event
        /// </summary>
        public event Action<string> Open;

        /// <summary>
        /// Sets the title of the window
        /// </summary>
        public string Title { set { Text = value; } }

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
        /// Closes the window
        /// </summary>
        public void DoClose()
        {
            Close();
        }
    }
}
