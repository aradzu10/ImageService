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
using Newtonsoft.Json;
using ImageService.Logger;

namespace ImageService.Communication.ClientManager
{
    class ClientCommunication
    {
        TcpClient client;
        public event EventHandler<MessageRecievedEventArgs> OnRemoveDir;
        public event EventHandler OnStop;

        public ClientCommunication(TcpClient client_)
        {
            client = client_;
        }

        public void StartCommunication(Settings settings)
        {
            LogBackup logBackup = LogBackup.Instance;
            try
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(new MessageRecievedEventArgs(MessageTypeEnum.SETTINGS, settings.Serialize()).Serialize());

                    foreach (MessageRecievedEventArgs mess in logBackup.messages)
                    {
                        writer.WriteLine(mess.Serialize());
                    }
                }
            }
            catch (Exception) { }


        }

        public void WriteMessage(Object sender, MessageRecievedEventArgs e)
        {
            try
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(e.Serialize());
                }
            }
            catch (Exception) { }
        }

        public void UpdatesListner()
        {
            try
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (true)
                    {
                        // TODO - when client disconnect
                        string command = reader.ReadLine();
                        MessageRecievedEventArgs message = MessageRecievedEventArgs.Deserialize(command);
                        ClientRequest(message, reader);
                    }
                }
            }
            catch (Exception)
            {
                StopCommunication(this, null);
            }
        }

        private void ClientRequest(MessageRecievedEventArgs message, StreamReader reader)
        {
            switch (message.Status)
            {
                case MessageTypeEnum.REMOVE_HANDLER:
                    OnRemoveDir?.Invoke(this, message);
                    break;
                default:
                    break;

            }
        }

        public void StopCommunication(object sender, System.EventArgs e)
        {
            client?.Close();
            OnStop?.Invoke(this, null);
        }
    }
}
