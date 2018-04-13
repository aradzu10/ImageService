using ImageService.Controller;
using ImageService.DirectoryListener;
using ImageService.FileHandler;
using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.ListenerManager
{
    public class ImageListenerManager
    {
        #region Members
        private ILoggingService logger;
        private IImageController controller;
        #endregion

        public event EventHandler CloseListener;

        public ImageListenerManager(string outputDir, int thumbSize)
        {
            ImageServiceFileHandler imageServiceFile = new ImageServiceFileHandler(outputDir, thumbSize);
            logger = new LoggingService();
            controller = new ImageController(imageServiceFile);
        }

        public void StartListenDir(string[] directories)
        {
            // check - log
            foreach (string dir in directories)
            {
                MyLogger.MyLogger.Log(dir); 
                CreateDirListener(dir);
            }
        }

        private void CreateDirListener(string dir)
        {
            IDirectoryListener directoryListener = new ImageDirectoryListener(controller, logger);
            directoryListener.StartListenDirectory(dir);
            CloseListener += directoryListener.StopListenDirectory;
            // check - log
        }

        public void StopListening()
        {
            CloseListener?.Invoke(this, null);
            // check - log
        }
    }
}
