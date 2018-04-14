using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Commands;
using ImageService.Controller;
using Logger;
using System.IO;
using ImageService.Enums;

namespace ImageService.DirectoryListener
{
    class ImageDirectoryListener : IDirectoryListener
    {
        private IImageController controller;
        private ILoggingService logger;
        private FileSystemWatcher dirListener;
        private string dirPath;
        private readonly string[] filters = { ".jpg", ".png", ".gif", ".bmp", ".jepg" };

        public ImageDirectoryListener(IImageController controller_, ILoggingService logger_)
        {
            controller = controller_;
            logger = logger_;
            dirListener = new FileSystemWatcher();
        }

        public void StartListenDirectory(string dirPath_)
        {
            logger.Log("Add folder: " + dirPath_, Logger.Message.MessageTypeEnum.INFO);
            dirPath = dirPath_;
            dirListener.Path = dirPath;
            dirListener.Filter = "*.*";
            dirListener.Created += new FileSystemEventHandler(OnChanged);
            dirListener.EnableRaisingEvents = true;
        }

        public void StopListenDirectory(object sender, System.EventArgs e)
        {
            logger.Log("Stop listen to: " + dirPath, Logger.Message.MessageTypeEnum.INFO);
            dirListener.EnableRaisingEvents = false;
        }

        public void OnChanged(object source, FileSystemEventArgs e)
        {
            logger.Log("New file detected in \"" + dirPath + "\"", Logger.Message.MessageTypeEnum.INFO);
            if (!filters.Contains(Path.GetExtension(e.FullPath)))
            {
                logger.Log("File \"" + Path.GetFileName(e.FullPath) + "\" isnt an image", Logger.Message.MessageTypeEnum.INFO);
                return;
            }
            logger.Log("Backup \"" + Path.GetFileName(e.FullPath) + "\" is an image", Logger.Message.MessageTypeEnum.INFO);
            ExitCode status = controller.ExecuteCommand(Command.BackupFile, new string[] { e.FullPath });
            if (status != ExitCode.Success)
            {
                logger.Log("Failed to back up \"" + Path.GetFileName(e.FullPath) + "\" reson of failuer: " + GetFailedReson(status), Logger.Message.MessageTypeEnum.FAIL);
            }

        }

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
