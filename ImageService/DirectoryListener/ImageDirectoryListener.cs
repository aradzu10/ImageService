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
        private ILogger logger;
        private FileSystemWatcher dirListener;
        private string dirPath;
        private readonly string[] filters = { ".jpg", ".png", ".gif", ".bmp" };

        public ImageDirectoryListener(IImageController controller_, ILogger logger_)
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
            // check - log file got in folder 
            if (!filters.Contains(Path.GetExtension(e.FullPath)))
            {
                // check - log not copied
                return;
            }
            // check - log command and shck status
            ExitCode status = controller.ExecuteCommand(Command.BackupFile, new string[] { e.FullPath });
        }

    }
}
