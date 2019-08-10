using System;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
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
using Newtonsoft.Json;
using OAuth2;

namespace YoutubeUploadApp
{
    class Controller
    {
        // Instance variables for storing video information
        private string filePath;
        private string videoTitle;
        private string videoDescription;

        // Here is our view
        private YoutubeUploadAppGUI window;

        /// <summary>
        /// Cancellation token for cancel button
        /// </summary>
        private CancellationTokenSource tokenSource;

        /// <summary>
        /// Creates a controller and hooks alle vents
        /// </summary>
        /// <param name="window"></param>
        public Controller(YoutubeUploadAppGUI window)
        {
            this.window = window;
            window.Upload = HandleUploadAsync;
        }

        /// <summary>
        /// Handles when the upload button is pressed
        /// </summary>
        private async void HandleUploadAsync(string filePath, string videoTitle, string videoDesc)
        {
            this.filePath = filePath;
            this.videoTitle = videoTitle;
            this.videoDescription = videoDesc;

            using (HttpClient client = CreateClient())
            {
                tokenSource = new CancellationTokenSource();

                // Create body for HTTP request
                dynamic requestContent = new ExpandoObject();
                dynamic fileDetails = new ExpandoObject();
                fileDetails.fileName = filePath;
                dynamic snippet = new ExpandoObject();
                snippet.title = videoTitle;
                snippet.description = videoDesc;
                snippet.categoryId = "22";
                dynamic status = new ExpandoObject();
                status.privacyStatus = "unlisted";

                StringContent content = new StringContent(JsonConvert.SerializeObject(requestContent), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("videos", content, tokenSource.Token);

                if(response.IsSuccessStatusCode)
                {
                    // SHUT DOWN COMPUTER
                }
                else
                {
                    window.Message = "Error uploading: " + response.StatusCode + "\n" + response.ReasonPhrase;
                }
            }
        }

        /// <summary>
        /// Creates an HttpClient for communicating with the server.
        /// </summary>
        private static HttpClient CreateClient()
        {
            // Create a client whose base address is the GitHub server
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("https://www.googleapis.com/upload/youtube/v3/")
            };

            // Tell the server that the client will accept this particular type of response data
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Authorization", File.ReadAllText(@"C:\Users\evanv\Source\Repos\YoutubeUploadApp\GoogleApiToken.txt"););
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            // There is more client configuration to do, depending on the request.
            return client;
        }
    }
}
