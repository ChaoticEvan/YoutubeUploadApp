using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using OAuth2;

namespace YoutubeUploadApp
{
    class Controller
    {
        private string filePath;
        private string videoTitle;
        private string videoDescription;

        // Here is our view
        private YoutubeUploadAppGUI window;

        /// <summary>
        /// Creates a controller and hooks alle vents
        /// </summary>
        /// <param name="window"></param>
        public Controller(YoutubeUploadAppGUI window)
        {
            this.window = window;
            window.Upload = HandleUpload;
        }

        /// <summary>
        /// Handles when the upload button is pressed
        /// </summary>
        private void HandleUpload(string filePath, string videoTitle, string videoDesc)
        {
            this.filePath = filePath;
            this.videoTitle = videoTitle;
            videoDescription = videoDesc;

            try
            {
                Run().Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
        }

        private async Task Run()
        {
            UserCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                dsAuthorizationBroker.RedirectUri = "http://127.0.0.1:57617/authorize/";
                credential = await dsAuthorizationBroker.AuthorizeAsync(
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

            string[] filePathPartitioned = this.filePath.Split('\\');
            string videoTitle = filePathPartitioned[2] + " (" + DateTime.Now.ToString("MM/dd/yyyy") + ") " + this.videoTitle;

            var video = new Video();
            video.Snippet = new VideoSnippet();
            video.Snippet.Title = videoTitle;
            video.Snippet.Description = this.videoDescription;
            video.Status = new VideoStatus();
            video.Status.PrivacyStatus = "unlisted";
            var filePath = this.filePath;

            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                videosInsertRequest.ProgressChanged += videosInsertRequest_ProgressChanged;
                videosInsertRequest.ResponseReceived += videosInsertRequest_ResponseReceived;

                await videosInsertRequest.UploadAsync();
            }
        }

        void videosInsertRequest_ProgressChanged(IUploadProgress progress)
        {
            switch (progress.Status)
            {
                case UploadStatus.Uploading:
                    Console.WriteLine("{0} bytes sent.", progress.BytesSent);
                    break;

                case UploadStatus.Failed:
                    Console.WriteLine("An error prevented the upload from completing.\n{0}", progress.Exception);
                    break;
            }
        }

        void videosInsertRequest_ResponseReceived(Video video)
        {
            Console.WriteLine("Video id '{0}' was successfully uploaded.", video.Id);
            // Process.Start("shutdown", "/s /t 0");
        }
    }
}
