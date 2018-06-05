using ImageService.Enums;
using ImageService.ListenerManager;
using ImageService.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication.ClientManager
{
    class ClientHandler : IClientHandler
    {
        public void HandleClient(TcpClient tcpClient, ImageListenerManager imageListenerManager)
        {
            ClientCommunication client = new ClientCommunication(tcpClient);

            client.OnRemoveDir += imageListenerManager.StopListenToDirectory;
            client.SendAllPhotos += imageListenerManager.SendAllPhotos;
            client.OnStop += imageListenerManager.CloseClient;
            client.RemovePhoto += imageListenerManager.DeletePhoto;

            imageListenerManager.CloseAll += client.StopCommunication;
            imageListenerManager.RemoveDir += client.WriteMessage;
            imageListenerManager.Logger.MessageRecieved += client.WriteMessage;

            client.StartCommunication();
            client.UpdatesListner();
        }
    }
}
