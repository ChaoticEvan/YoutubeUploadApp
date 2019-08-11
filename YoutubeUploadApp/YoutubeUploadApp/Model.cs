using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace YoutubeUploadApp
{
    class YoutubeUploadAppModel
    {
        // Properties for storing video information
        public string filePath { private get; set; }
        public string videoTitle { private get; set; }
        public string videoDesc { private get; set; }

        /// <summary>
        /// StringBuilder to contain logs
        /// </summary>
        private static StringBuilder log;

        /// <summary>
        /// Path for creating/Adding to log file
        /// </summary>
        private static string logFilePath;

        /// <summary>
        /// Creates a YoutubeUploadAppModel object
        /// </summary>
        public YoutubeUploadAppModel()
        {
            log = new StringBuilder();
            logFilePath = @"C:\Users\evanv\source\repos\YoutubeUploadApp\logs\" + DateTime.Now.ToString("mm-dd-yyyy hh-mm") + "_YoutubeUploadApp.txt";
        }

        /// <summary>
        /// Creates a YoutubeUploadAppModel object with video details
        /// </summary>
        /// <param name="filePath">Path to the video file to be uploaded</param>
        /// <param name="videoTitle">Title of the video</param>
        /// <param name="videoDesc">Video description</param>
        public YoutubeUploadAppModel(string filePath, string videoTitle, string videoDesc)
        {
            log = new StringBuilder();
            logFilePath = @"C:\Users\evanv\source\repos\YoutubeUploadApp\logs\" + DateTime.Now.ToString("mm-dd-yyyy hh-mm") + "_YoutubeUploadApp.txt";
            this.filePath = filePath;
            this.videoTitle = videoTitle;
            this.videoDesc = videoDesc;
        }

        /// <summary>
        /// Helper method for starting the upload process
        /// and logging errors during the process
        /// </summary>
        [STAThread]
        public void Upload()
        {
            try
            {
                Run().Wait();
            }
            catch (AggregateException ex)
            {
                // Log all exceptions caught while trying to upload
                foreach (var e in ex.InnerExceptions)
                {
                    log.AppendLine(DateTime.UtcNow.ToString("mm/dd/yyyy hh:mm:ss") + " ERROR -  Error occured while uploading");
                    log.AppendLine(DateTime.UtcNow.ToString("mm/dd/yyyy hh:mm:ss") + e.Message);
                    log.AppendLine(DateTime.UtcNow.ToString("mm/dd/yyyy hh:mm:ss") + e.StackTrace);
                    AppendLogs();
                }
            }
        }

        /// <summary>
        /// Establishes the connection to YouTube and uploads the video        
        /// </summary>
        /// <returns>async task</returns>
        private async Task Run()
        {
            UserCredential credential;
            using (var stream = new FileStream(@"C:\Users\evanv\Source\Repos\YoutubeUploadApp\YoutubeUploadApp\YoutubeUploadApp\client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    // This OAuth 2.0 access scope allows an application to upload files to the
                    // authenticated user's YouTube channel, but doesn't allow other types of access.
                    new[] { YouTubeService.Scope.YoutubeUpload },
                    "user",
                    CancellationToken.None
                );
            }

            // Creates the client service
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
            });

            // Build the video
            log.AppendLine(DateTime.UtcNow.ToString("mm/dd/yyyy hh:mm:ss") + " INFO -  Building video");
            var video = new Video();
            video.Snippet = new VideoSnippet();
            video.Snippet.Title = this.videoTitle;
            video.Snippet.Description = this.videoDesc;
            video.Status = new VideoStatus();
            video.Status.PrivacyStatus = "unlisted";
            var filePath = this.filePath;
            AppendLogs();

            // Establish connection
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                videosInsertRequest.ProgressChanged += videosInsertRequest_ProgressChanged;
                videosInsertRequest.ResponseReceived += videosInsertRequest_ResponseReceived;

                log.AppendLine(DateTime.UtcNow.ToString("mm/dd/yyyy hh:mm:ss") + " INFO -  Starting to upload video");
                AppendLogs();
                await videosInsertRequest.UploadAsync();
            }
        }

        /// <summary>
        /// Method that logs progress update
        /// </summary>
        /// <param name="progress">Progress report</param>
        static void videosInsertRequest_ProgressChanged(IUploadProgress progress)
        {
            switch (progress.Status)
            {
                case UploadStatus.Uploading:
                    log.AppendLine(DateTime.UtcNow.ToString("mm/dd/yyyy hh:mm:ss") + "INFO - " + progress.BytesSent + " bytes sent.");
                    AppendLogs();
                    break;

                case UploadStatus.Failed:
                    log.AppendLine(DateTime.UtcNow.ToString("mm/dd/yyyy hh:mm:ss") + "ERROR - An error prevented the upload from completing.\n" + progress.Exception);
                    AppendLogs();
                    break;
            }
        }

        /// <summary>
        /// Log that video was uploaded 
        /// </summary>
        /// <param name="video"></param>
        static void videosInsertRequest_ResponseReceived(Video video)
        {
            log.AppendLine(DateTime.UtcNow.ToString("mm/dd/yyyy hh:mm:ss") + "SUCCESS - Video id " + video.Id + " was successfully uploaded.");
            AppendLogs();
        }

        /// <summary>
        /// Method for appending logs to a file and clearing current logs
        /// </summary>
        private static void AppendLogs()
        {
            File.AppendAllText(@logFilePath, log.ToString());
            log.Clear();
        }
    }
}
