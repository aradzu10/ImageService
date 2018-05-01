using Logger.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Server.ClientManager
{
    class ClientHandler : IClientHandler
    {
        TcpClient client;

        public ClientHandler(TcpClient client_)
        {
            client = client_;
        }

        public void handleClient()
        {
            throw new NotImplementedException();
        }

        public void startCommunication()
        {
            /*
             * send all settings, send all log
             */
        }

        public void writeMessage(Object sender, MessageRecievedEventArgs e)
        {
            // send log message
        }


        public void updatesListner()
        {
            // wait for client messages
        }
    }
}
