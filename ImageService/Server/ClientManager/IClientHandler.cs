using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Server.ClientManager
{
    public interface IClientHandler
    {
        void handleClient();
    }
}
