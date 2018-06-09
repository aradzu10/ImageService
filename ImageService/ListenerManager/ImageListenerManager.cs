using ImageService.Communication;
using ImageService.Communication.ClientManager;
using ImageService.Controller;
using ImageService.DirectoryListener;
using ImageService.Enums;
using ImageService.FileHandler;
using ImageService.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Messages;
using ImageService.PhotosHandler;

namespace ImageService.ListenerManager
{
    /// <summary>
    /// handle all directory listeners
    /// </summary>
    public class ImageListenerManager
    {
        public ILogger Logger { get; private set; }
        public ILogger PhotoUpdate { get; private set; }
        private IImageController Controller;
        private ImageServiceFileHandler ImageServiceFile;
        private Dictionary<string, IDirectoryListener> directories;
        private Settings settings;
        private Server server;

        public event EventHandler CloseAll;
        public event EventHandler<MessageRecievedEventArgs> RemoveDir;

        public ImageListenerManager(ILogger logger_, string outputDir, string sourceName, string logName, int thumbSize)
        {
            // create controller
            // and check that output dirctory successfully creates
            Logger = logger_;
            PhotoUpdate = new PhotoUpdater();

            settings = Settings.Instance;
            settings.SetSettings(outputDir, sourceName, logName, thumbSize, 0);

            ImageServiceFile = new ImageServiceFileHandler(out ExitCode status);
            Controller = new ImageController(ImageServiceFile);
            if (status == ExitCode.F_Create_Dir)
            {
                Logger.Log("Cannot create output image folder.\nFatal error cannot recover, exiting",
                    MessageTypeEnum.L_FAIL);
                Environment.Exit(1);
            }

            int port = 6145;
            LogBackup logBackup = LogBackup.Instance;
            Logger.MessageRecieved += logBackup.WriteMessage;
            directories = new Dictionary<string, IDirectoryListener>();
            server = new Server(port, new ClientHandler(), this);
        }

        /// <summary>
        /// start all listeners
        /// </summary>
        /// <param name="directories">all dirs to listen</param>
        public void StartListenDir(string[] directories)
        {
            Logger.Log("Start listening to folders", MessageTypeEnum.L_INFO);
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
            IDirectoryListener directoryListener = new ImageDirectoryListener(Controller, Logger, PhotoUpdate);
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
            Logger.Log("Stop listening to all folders", MessageTypeEnum.L_INFO);
        }

        public void SendAllPhotos(object sender, System.EventArgs e)
        {
            ClientCommunication client = (ClientCommunication) sender;
            PhotoUpdate.MessageRecieved += client.WriteMessage;
            client.WriteMessage(this, PhotoExtractor.GetAllPhotos());
        }

        public void DeletePhoto(object sender, MessageRecievedEventArgs message)
        {
            PhotoPackage photo = PhotoPackage.Deserialize(message.Message);

            if (ImageServiceFile.DeleteFile(photo.PhotoPath) == ExitCode.Success &&
            ImageServiceFile.DeleteFile(photo.PhotoThumbnailPath) == ExitCode.Success)
            {
                PhotoUpdate.Log(message);
            }
        }

        public void StopListenToDirectory(object sender, MessageRecievedEventArgs dir)
        {
            if (!directories.ContainsKey(dir.Message))
            {
                return;
            }
            directories[dir.Message].StopListenDirectory(this, null);
            directories.Remove(dir.Message);
            settings.RemoveDirectories(dir.Message);
            RemoveDir?.Invoke(this, dir);
        }

        public void CloseClient(object sender, System.EventArgs e)
        {
            ClientCommunication client = (ClientCommunication) sender;
            CloseAll -= client.StopCommunication;
            Logger.MessageRecieved -= client.WriteMessage;
            PhotoUpdate.MessageRecieved -= client.WriteMessage;
        }
    }
}
