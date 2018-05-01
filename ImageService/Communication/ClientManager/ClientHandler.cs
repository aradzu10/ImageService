using ImageService.Enums;
using ImageService.ListenerManager;
using Logger.Message;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static ImageService.Logger.LogBackupHandler.LogReader;

namespace ImageService.Communication.ClientManager
{
    class ClientHandler : IClientHandler
    {
        public void HandleClient(TcpClient tcpClient, Settings settings, ImageListenerManager imageListenerManager)
        {
            ClientCommunication client = new ClientCommunication(tcpClient);
            client.OnRemoveDir += imageListenerManager.StopListenToDirectory;
            imageListenerManager.CloseAll += client.StopCommunication;
            LogFileReader reader = new LogFileReader(settings.logPath);
            client.StartCommunication(settings, reader);
            client.UpdatesListner();
        }
    }
}
