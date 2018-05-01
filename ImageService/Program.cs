using ImageService.Controller;
using ImageService.DirectoryListener;
using ImageService.FileHandler;
using ImageService.ListenerManager;
using Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ImageService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// starts the service
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ImageService()
            };
            ServiceBase.Run(ServicesToRun);
            /* my tests
            ------------
            string outputFolder = ConfigurationManager.AppSettings.Get("OutputDir");
            int ThumbnailSize = Int32.Parse(ConfigurationManager.AppSettings.Get("ThumbnailSize"));
            ILoggingService logger = new LoggingService();
            ImageListenerManager listenerManager = new ImageListenerManager(logger, outputFolder, ThumbnailSize);
            string[] folderToListen = (ConfigurationManager.AppSettings.Get("Handler").Split(';'));
            listenerManager.StartListenDir(folderToListen);
            while (true) { }
            */
        }
    }
}
