using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ImageServiceUI.Communication
{
    class HandleCommunication
    {
        private Client client;
        public event EventHandler<MessageRecievedEventArgs> OnLogMessage;
        public event EventHandler<MessageRecievedEventArgs> OnHandelRemove;
        public event EventHandler<Settings> SetSettings;

        public HandleCommunication(Client client_)
        {
            client = client_;
        }

        public void GetSettings()
        {
            string mess = client.ReadFromServer();
            Settings settings = (Settings)JsonConvert.DeserializeObject(mess);
            SetSettings?.Invoke(this, settings);
        }

        public void GetMessages()
        {
            while (true)
            {
                string mess = client.ReadFromServer();
                MessageRecievedEventArgs message = (MessageRecievedEventArgs)JsonConvert.DeserializeObject(mess);
                HandleMessage(message);
            }
        }

        public void RemoveHandler(string dir)
        {
            MessageRecievedEventArgs message = new MessageRecievedEventArgs(MessageTypeEnum.REMOVE_HANDLER, dir);
            client.WriteToServer(message.Serialize());
        }

        private void HandleMessage(MessageRecievedEventArgs message)
        {
            switch(message.Status)
            {
                case MessageTypeEnum.REMOVE_HANDLER:
                    OnHandelRemove?.Invoke(this, message);
                    break;
                case MessageTypeEnum.L_FAIL:
                case MessageTypeEnum.L_INFO:
                case MessageTypeEnum.L_WARNING:
                    OnLogMessage?.Invoke(this, message);
                    break;
                default:
                    break;
            }
        }
    }
}
