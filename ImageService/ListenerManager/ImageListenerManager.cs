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

        public ImageListenerManager(ILoggingService logger_, string outputDir, int thumbSize)
        {
            ImageServiceFileHandler imageServiceFile = new ImageServiceFileHandler(outputDir, thumbSize);
            logger = logger_;
            controller = new ImageController(imageServiceFile);
        }

        public void StartListenDir(string[] directories)
        {
            logger.Log("Start listening to folders", Logger.Message.MessageTypeEnum.INFO);
            foreach (string dir in directories)
            {
                CreateDirListener(dir);
            }
        }

        private void CreateDirListener(string dir)
        {
            IDirectoryListener directoryListener = new ImageDirectoryListener(controller, logger);
            directoryListener.StartListenDirectory(dir);
            CloseListener += directoryListener.StopListenDirectory;
        }

        public void StopListening()
        {
            CloseListener?.Invoke(this, null);
            logger.Log("Stop listening to all folders", Logger.Message.MessageTypeEnum.INFO);
        }
    }
}
