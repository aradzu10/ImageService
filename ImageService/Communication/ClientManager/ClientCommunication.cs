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
using System.Drawing;

namespace ImageService.Communication.ClientManager
{
    class ClientCommunication
    {
        TcpClient client;
        public event EventHandler<MessageRecievedEventArgs> OnRemoveDir;
        public event EventHandler<MessageRecievedEventArgs> RemovePhoto;
        public event EventHandler SendAllPhotos;
        public event EventHandler OnStop;

        public ClientCommunication(TcpClient client_)
        {
            client = client_;
        }

        public void StartCommunication()
        {
            Settings settings = Settings.Instance;
            LogBackup logBackup = LogBackup.Instance;
            try
            {
                NetworkStream stream = client.GetStream();
                BinaryWriter writer = new BinaryWriter(stream);

                writer.Write(new MessageRecievedEventArgs(MessageTypeEnum.SETTINGS, settings.Serialize()).Serialize());

                foreach (MessageRecievedEventArgs mess in logBackup.messages)
                {
                    writer.Write(mess.Serialize());
                }                
            }
            catch (Exception) { }
        }

        public void WriteMessage(Object sender, MessageRecievedEventArgs e)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                BinaryWriter writer = new BinaryWriter(stream);

                writer.Write(e.Serialize());
                   
            }
            catch (Exception) { }
        }

        public void UpdatesListner()
        {
            try
            {
                NetworkStream stream = client.GetStream();
                BinaryReader reader = new BinaryReader(stream);

                while (true)
                {
                    // TODO - when client disconnect
                    string command = reader.ReadString();
                    MessageRecievedEventArgs message = MessageRecievedEventArgs.Deserialize(command);
                    ClientRequest(message);
                }
                   
            }
            catch (Exception)
            {
                StopCommunication(this, null);
            }
        }

        private void ClientRequest(MessageRecievedEventArgs message)
        {
            switch (message.Status)
            {
                case MessageTypeEnum.REMOVE_HANDLER:
                    OnRemoveDir?.Invoke(this, message);
                    break;
                case MessageTypeEnum.P_SENDALL:
                    SendAllPhotos?.Invoke(this, null);
                    break;
                case MessageTypeEnum.P_DELETE:
                    RemovePhoto?.Invoke(this, message);
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
