using ImageService.ListenerManager;
using Logger.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ImageService.Logger.LogBackupHandler;
using static ImageService.Logger.LogBackupHandler.LogReader;

namespace ImageService.Communication.ClientManager
{
    public interface IClientHandler
    {
        void HandleClient(TcpClient tcpClient, Settings settings, ImageListenerManager imageListenerManager);
    }
}
