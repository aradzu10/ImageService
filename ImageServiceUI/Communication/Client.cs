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
        private static int port = 6145;
        private TcpClient client;

        public Client()
        {
            client = new TcpClient();
        }

        public bool ConnectToServer()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            try
            {
                client.Connect(ep);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
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
