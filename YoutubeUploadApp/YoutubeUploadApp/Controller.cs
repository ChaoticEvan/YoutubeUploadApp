using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeUploadApp
{
    class Controller
    {
        private YoutubeUploadAppGUI window;

        public Controller(YoutubeUploadAppGUI window)
        {
            this.window = window;
            window.Open += HandleOpen;
        }

        private void HandleOpen(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
