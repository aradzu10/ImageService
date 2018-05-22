using ImageService.ListenerManager;
using ImageService.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication.ClientManager
{
    public interface IClientHandler
    {
        void HandleClient(TcpClient tcpClient, Settings settings, ImageListenerManager imageListenerManager);
    }
}
