using ImageService.Communication;
using ImageService.Communication.ClientManager;
using ImageService.Controller;
using ImageService.DirectoryListener;
using ImageService.Enums;
using ImageService.FileHandler;
using ImageService.Logger.LogBackupHandler;
using Logger;
using Logger.Message;
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
        private ILogger logger;
        private IImageController controller;
        private Dictionary<string, IDirectoryListener> directories;
        private Settings settings;
        private Server server;

        public event EventHandler CloseAll;
        public event EventHandler<String> RemoveDir;

        public ImageListenerManager(ILogger logger_, string outputDir, string sourceName, string logName, int thumbSize)
        {
            // create controller
            // and check that output dirctory successfully creates
            logger = logger_;
            ImageServiceFileHandler imageServiceFile = new ImageServiceFileHandler(outputDir, thumbSize, out ExitCode status);
            controller = new ImageController(imageServiceFile);
            if (status == ExitCode.F_Create_Dir)
            {
                logger.Log("Cannot create output image folder.\nFatal error cannot recover, exiting",
                    MessageTypeEnum.FAIL);
                Environment.Exit(1);
            }

            int port = 6145;
            string logPath = "log.txt";
            LogSaver logSaver = new LogSaver(logPath);
            logger.MessageRecieved += logSaver.WriteMessage;
            directories = new Dictionary<string, IDirectoryListener>();
            settings = new Settings(outputDir, sourceName, logName, logPath, thumbSize);
            server = new Server(port, new ClientHandler(), settings, this);
        }

        /// <summary>
        /// start all listeners
        /// </summary>
        /// <param name="directories">all dirs to listen</param>
        public void StartListenDir(string[] directories)
        {
            logger.Log("Start listening to folders", MessageTypeEnum.INFO);
            foreach (string dir in directories)
            {
                CreateDirListener(dir);
            }

            server.Start();
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
                directories[dir] = directoryListener;
                settings.AddDirectories(dir);
                CloseAll += directoryListener.StopListenDirectory;
            }
        }

        /// <summary>
        /// stop all listeners
        /// </summary>
        public void StopListening()
        {
            CloseAll?.Invoke(this, null);
            logger.Log("Stop listening to all folders", MessageTypeEnum.INFO);
        }

        public void StopListenToDirectory(object sender, String dir)
        {
            if (!directories.ContainsKey(dir))
            {
                return;
            }
            directories[dir].StopListenDirectory(this, null);
            directories.Remove(dir);
            settings.RemoveDirectories(dir);
            RemoveDir?.Invoke(this, dir);
            logger.Log("Stop listening to: " + dir, MessageTypeEnum.INFO); 
        }
    }
}
