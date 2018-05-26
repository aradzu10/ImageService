using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Commands;
using ImageService.Controller;
using Logger;
using ImageService.Messages;
using System.IO;
using ImageService.Enums;
using ImageService.ListenerManager;

namespace ImageService.DirectoryListener
{
    class ImageDirectoryListener : IDirectoryListener
    {
        private IImageController controller;
        private ILogger logger;
        private FileSystemWatcher dirListener;
        private string dirPath;
        private readonly string[] filters = { ".jpg", ".png", ".gif", ".bmp", ".jpeg" };

        /// <summary>
        /// constractor of listener
        /// </summary>
        /// <param name="controller_">Image controller</param>
        /// <param name="logger_">Service logger</param>
        public ImageDirectoryListener(IImageController controller_, ILogger logger_)
        {
            controller = controller_;
            logger = logger_;
            dirListener = new FileSystemWatcher();
        }

        /// <summary>
        /// start listen to directory
        /// </summary>
        /// <param name="dirPath_">path to listen to</param>
        public ExitCode StartListenDirectory(string dirPath_)
        {
            // make sure dir exist
            // if not exit
            // else initate all settings
            logger.Log("Start listening to: " + dirPath_, MessageTypeEnum.L_INFO);
            if (!Directory.Exists(dirPath_))
            {
                Directory.CreateDirectory(dirPath_);
                logger.Log("Directory \"" + dirPath_ + "\" doesn't exist\n didn't add directory",
                    MessageTypeEnum.L_FAIL);
                return ExitCode.Failed;
            }
            dirPath = dirPath_;
            dirListener.Path = dirPath;
            dirListener.Filter = "*.*";
            dirListener.Created += new FileSystemEventHandler(OnChanged);
            dirListener.EnableRaisingEvents = true;
            return ExitCode.Success;
        }

        /// <summary>
        /// delete listener
        /// </summary>
        /// <param name="sender">event caller</param>
        /// <param name="e">args - not use</param>
        public void StopListenDirectory(object sender, System.EventArgs e)
        {
            logger.Log("Stop listening to: " + dirPath, MessageTypeEnum.L_INFO);
            dirListener.EnableRaisingEvents = false;
            dirListener.Dispose();
            ((ImageListenerManager)sender).CloseAll -= this.StopListenDirectory;
        }

        /// <summary>
        /// when file created
        /// </summary>
        /// <param name="source">event caller</param>
        /// <param name="e">the created file</param>
        public void OnChanged(object source, FileSystemEventArgs e)
        {
            // check if file is an image
            // if it is, backup 
            logger.Log("New file detected in \"" + dirPath + "\"", MessageTypeEnum.L_INFO);
            if (!filters.Contains(Path.GetExtension(e.FullPath)))
            {
                logger.Log("File \"" + Path.GetFileName(e.FullPath) + "\" isnt an image",
                    MessageTypeEnum.L_INFO);
                return;
            }
            logger.Log("Backup \"" + Path.GetFileName(e.FullPath) + "\" is an image",
                MessageTypeEnum.L_INFO);
            ExitCode status = controller.ExecuteCommand(Command.BackupFile, new string[] { e.FullPath });
            if (status != ExitCode.Success)
            {
                logger.Log("Failed to back up \"" + Path.GetFileName(e.FullPath) + "\" reson of failuer: " +
                    GetFailedReson(status), MessageTypeEnum.L_FAIL);
            }
            else
            {
                logger.Log("Successfully Backup \"" + Path.GetFileName(e.FullPath) + "\" and created thumbnail",
                    MessageTypeEnum.L_INFO);
            }

        }

        /// <summary>
        /// get right message by failed type
        /// </summary>
        /// <param name="status">error code</param>
        /// <returns>message to print</returns>
        private string GetFailedReson(ExitCode status)
        {
            switch (status)
            {
                case ExitCode.Failed:
                    return "unknown";
                case ExitCode.F_Create_Dir:
                    return "unable to create image directory";
                case ExitCode.F_Create_Thumb:
                    return "unable to create thumbnail image";
                case ExitCode.F_Invalid_Input:
                    return "image doesn't exist";
                case ExitCode.F_Missing_Date:
                    return "missing image date of photo";
                case ExitCode.F_Move:
                    return "unable to move image to directory";
                default:
                    return "";
            }
        }

    }
}
