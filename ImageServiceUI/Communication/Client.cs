using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceUI.Communication
{
    public class Client
    {
        private TcpClient client;
        private int port;

        public Client(int p)
        {
            port = p;
            client = new TcpClient();
        }

        public void ConnectToServer()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            client.Connect(ep);
        }

        public string ReadFromServer()
        {
            string line = "";
            try
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    // check - read all object settings
                    line = reader.ReadLine();
                }
            }
            catch (Exception) {}
            return line;
        }

        public void WriteToServer(string line)
        {
            try
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    // check - read all object settings
                    writer.WriteLine(line);
                }
            }
            catch (Exception) { }
        }

        public void StopCommunication()
        {
            client.Close();
        }

    }
}
