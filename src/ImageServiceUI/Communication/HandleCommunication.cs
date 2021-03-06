﻿using ImageServiceUI.Messages;
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
        public event EventHandler<String> OnHandelRemove;
        public event EventHandler<Settings> SetSettings;

        public HandleCommunication()
        {
            client = new Client();
        }

        public bool ConnectToServer()
        {
            return client.ConnectToServer();
        }

        public void Communication()
        {
            while (true)
            {
                string mess = client.ReadFromServer();
                MessageRecievedEventArgs message = MessageRecievedEventArgs.Deserialize(mess);
                if (message != null)
                {
                    HandleMessage(message);
                }
            }
        }

        public void SendMessage(object sender, MessageRecievedEventArgs message)
        {
            client.WriteToServer(message.Serialize());
        }

        public void RemoveHandler(string dir)
        {
            MessageRecievedEventArgs message = new MessageRecievedEventArgs(MessageTypeEnum.REMOVE_HANDLER, dir);
            client.WriteToServer(message.Serialize());
        }

        public void CloseClient()
        {
            client.StopCommunication();
        }

        private void HandleMessage(MessageRecievedEventArgs message)
        {
            switch(message.Status)
            {
                case MessageTypeEnum.SETTINGS:
                    Settings settings = Settings.Deserialize(message.Message);
                    SetSettings?.Invoke(this, settings);
                    break;
                case MessageTypeEnum.REMOVE_HANDLER:
                    OnHandelRemove?.Invoke(this, message.Message);
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
