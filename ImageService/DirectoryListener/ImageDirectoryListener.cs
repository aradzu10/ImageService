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
        private readonly string[] filters = { ".jpg", ".png", ".gif", ".bmp" };

        public ImageDirectoryListener(IImageController controller_, ILoggingService logger_)
        {
            controller = controller_;
            logger = logger_;
            dirListener = new FileSystemWatcher();
        }

        public void StartListenDirectory(string dirPath_)
        {
            dirPath = dirPath_;
            dirListener.Path = dirPath;
            dirListener.Filter = "*.*";
            dirListener.Created += new FileSystemEventHandler(OnChanged);
            dirListener.EnableRaisingEvents = true;
        }

        public void StopListenDirectory(object sender, System.EventArgs e)
        {
            dirListener.EnableRaisingEvents = false;
            // check - log here
        }

        public void OnChanged(object source, FileSystemEventArgs e)
        {
            MyLogger.MyLogger.Log("In: " + dirPath + " in on change");
            // check - log file got in folder 
            if (!filters.Contains(Path.GetExtension(e.FullPath)))
            {
                // check - log not copied
                MyLogger.MyLogger.Log("In: " + dirPath + " worng format");
                return;
            }
            // check - log command and shck status
            ExitCode status = controller.ExecuteCommand(Command.BackupFile, new string[] { e.FullPath });
        }

    }
}
