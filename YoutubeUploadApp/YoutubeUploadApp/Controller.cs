﻿namespace YoutubeUploadApp
{
    class Controller
    {
        // Here is our view
        private readonly YoutubeUploadAppGUI window;

        private YoutubeUploadAppModel model;

        /// <summary>
        /// Creates a controller and hooks all events
        /// </summary>
        /// <param name="window">the current view</param>
        public Controller(YoutubeUploadAppGUI window)
        {
            this.window = window;
            window.Upload = HandleUpload;
            this.model = new YoutubeUploadAppModel();
        }

        /// <summary>
        /// Handles when the upload button is pressed
        /// </summary>
        private void HandleUpload(string filePath, string videoTitle, string videoDesc)
        {
            model = new YoutubeUploadAppModel(filePath, videoTitle, videoDesc);
        }       
    }
}
