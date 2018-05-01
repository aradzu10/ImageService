using ImageService.Controller;
using ImageService.DirectoryListener;
using ImageService.Enums;
using ImageService.FileHandler;
using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.ListenerManager
{
    /// <summary>
    /// handle all directory listeners
    /// </summary>
    public class ImageListenerManager
    {
        private ILoggingService logger;
        private IImageController controller;

        public event EventHandler CloseListener;

        public ImageListenerManager(ILoggingService logger_, string outputDir, int thumbSize)
        {
            // create controller
            // and check that output dirctory successfully creates
            ExitCode status;
            ImageServiceFileHandler imageServiceFile = new ImageServiceFileHandler(outputDir, thumbSize, out status);
            logger = logger_;
            controller = new ImageController(imageServiceFile);
            if (status == ExitCode.F_Create_Dir)
            {
                logger.Log("Cannot create output image folder.\nFatal error cannot recover, exiting",
                    Logger.Message.MessageTypeEnum.FAIL);
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// start all listeners
        /// </summary>
        /// <param name="directories">all dirs to listen</param>
        public void StartListenDir(string[] directories)
        {
            logger.Log("Start listening to folders", Logger.Message.MessageTypeEnum.INFO);
            foreach (string dir in directories)
            {
                CreateDirListener(dir);
            }
        }

        /// <summary>
        /// create spesific dir
        /// </summary>
        /// <param name="dir"></param>
        private void CreateDirListener(string dir)
        {
            // check that dir exist
            IDirectoryListener directoryListener = new ImageDirectoryListener(controller, logger);
            if (directoryListener.StartListenDirectory(dir) == ExitCode.Success)
            {
                CloseListener += directoryListener.StopListenDirectory;
            }
        }

        /// <summary>
        /// stop all listeners
        /// </summary>
        public void StopListening()
        {
            CloseListener?.Invoke(this, null);
            logger.Log("Stop listening to all folders", Logger.Message.MessageTypeEnum.INFO);
        }
    }
}
