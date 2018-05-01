using ImageService.ListenerManager;
using ImageService.Communication.ClientManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication
{
    class Server
    {
        private int port;
        private TcpListener listener;
        private IClientHandler ch;
        private Settings settings;
        private ImageListenerManager imageListenerManager;

        public Server(int port, IClientHandler ch, Settings settings_, ImageListenerManager imageListenerManager_)
        {
            this.port = port;
            this.ch = ch;
            settings = settings_;
            imageListenerManager = imageListenerManager_;
            imageListenerManager.CloseAll += StopServer;
        }

        public void Start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);

            listener.Start();
            Task task = new Task(() => {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        new Task(() =>
                        {
                            ch.HandleClient(client, settings, imageListenerManager);
                        }).Start();
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
            });
            task.Start();
        }

        public void StopServer(object sender, System.EventArgs e)
        {
            listener?.Stop();
        }
    }
}
