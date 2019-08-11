using System;
using System.IO;
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
    class Controller
    {
        // Instance variables for storing video information
        private string filePath;
        private string videoTitle;
        private string videoDescription;
        private static StringBuilder log;
        private static string logFilePath;

        // Here is our view
        private readonly YoutubeUploadAppGUI window;

        /// <summary>
        /// Creates a controller and hooks all events
        /// </summary>
        /// <param name="window"></param>
        public Controller(YoutubeUploadAppGUI window)
        {
            this.window = window;
            window.Upload = HandleUpload;
            log = new StringBuilder();
            logFilePath = @"C:\Users\evanv\source\repos\YoutubeUploadApp\logs\" + DateTime.Now.ToString("mm-dd-yyyy hh-mm") + "_YoutubeUploadApp.txt";
        }

        /// <summary>
        /// Handles when the upload button is pressed
        /// </summary>
        private void HandleUpload(string filePath, string videoTitle, string videoDesc)
        {
            this.filePath = filePath;
            this.videoTitle = videoTitle;
            this.videoDescription = videoDesc;

            log.AppendLine(DateTime.UtcNow.ToString("mm/dd/yyyy hh:mm:ss") + "INFO - Beginning upload process");
            AppendLogs();

            Upload();
        }

        [STAThread]
        void Upload()
        {
            try
            {
                Run().Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    log.AppendLine(DateTime.UtcNow.ToString("mm/dd/yyyy hh:mm:ss") + " ERROR -  Error occured while uploading");
                    log.AppendLine(DateTime.UtcNow.ToString("mm/dd/yyyy hh:mm:ss") + e.Message);
                    log.AppendLine(DateTime.UtcNow.ToString("mm/dd/yyyy hh:mm:ss") + e.StackTrace);
                    AppendLogs();
                }
            }
        }

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

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
            });

            log.AppendLine(DateTime.UtcNow.ToString("mm/dd/yyyy hh:mm:ss") + " INFO -  Building video");
            var video = new Video();
            video.Snippet = new VideoSnippet();
            video.Snippet.Title = this.videoTitle;
            video.Snippet.Description = this.videoDescription;
            video.Snippet.CategoryId = "22"; // See https://developers.google.com/youtube/v3/docs/videoCategories/list
            video.Status = new VideoStatus();
            video.Status.PrivacyStatus = "unlisted";
            var filePath = this.filePath;
            AppendLogs();

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

    /// <summary>
    /// YouTube Data API v3 sample: upload a video.
    /// Relies on the Google APIs Client Library for .NET, v1.7.0 or higher.
    /// See https://developers.google.com/api-client-library/dotnet/get_started
    /// </summary>
    internal class UploadVideo
    {
        
    }
}
