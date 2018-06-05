using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Commands;
using ImageService.Controller;
using ImageService.Logger;
using ImageService.Messages;
using System.IO;
using ImageService.Enums;
using ImageService.ListenerManager;
using ImageService.PhotosHandler;

namespace ImageService.DirectoryListener
{
    class ImageDirectoryListener : IDirectoryListener
    {
        private IImageController controller;
        private ILogger logger;
        private ILogger photoUpdate;
        private FileSystemWatcher dirListener;
        private string dirPath;
        private readonly string[] filters = { ".jpg", ".png", ".gif", ".bmp", ".jpeg" };

        /// <summary>
        /// constractor of listener
        /// </summary>
        /// <param name="controller_">Image controller</param>
        /// <param name="logger_">Service logger</param>
        public ImageDirectoryListener(IImageController controller_, ILogger logger_, ILogger photoUpdate_)
        {
            controller = controller_;
            logger = logger_;
            photoUpdate = photoUpdate_;
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
            string path = e.FullPath;

            logger.Log("New file detected in \"" + dirPath + "\"", MessageTypeEnum.L_INFO);
            if (!filters.Contains(Path.GetExtension(path)))
            {
                logger.Log("File \"" + Path.GetFileName(path) + "\" isnt an image",
                    MessageTypeEnum.L_INFO);
                return;
            }
            logger.Log("Backup \"" + Path.GetFileName(path) + "\" is an image",
                MessageTypeEnum.L_INFO);

            string[] output = { "", "" };

            ExitCode status = controller.ExecuteCommand(Command.BackupFile, new string[] { path }, output);
            if (status != ExitCode.Success)
            {
                logger.Log("Failed to back up \"" + Path.GetFileName(path) + "\" reson of failuer: " +
                    GetFailedReson(status), MessageTypeEnum.L_FAIL);
            }
            else
            {
                logger.Log("Successfully Backup \"" + Path.GetFileName(path) + "\" and created thumbnail",
                    MessageTypeEnum.L_INFO);

                MessageRecievedEventArgs message = PhotoExtractor.GetPhotos(output[0], output[1]);
                if (message != null)
                {
                    photoUpdate.Log(message.Message, message.Status);
                }

                Settings settings = Settings.Instance;
                settings.PicturesCounter++;
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
