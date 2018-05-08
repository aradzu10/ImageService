using ImageService.Enums;
using ImageService.ListenerManager;
using Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static ImageService.Logger.LogBackupHandler.LogReader;
using Newtonsoft.Json;

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

        public void StartCommunication(Settings settings, LogFileReader readLogFile)
        {
            try
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(settings.Serialize());

                    string curMessage;
                    ExitCode code;
                    while ((code = readLogFile.NextLine(out curMessage)) == ExitCode.Success)
                    {
                        writer.WriteLine(curMessage);
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
            } catch (Exception) {}
        }

        public void RemoveDir(Object sender, MessageRecievedEventArgs dir)
        {
            try
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(dir.Serialize());
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
                        // check - do command
                        // when client disconnect
                        string command = reader.ReadLine();
                        MessageRecievedEventArgs message = (MessageRecievedEventArgs)JsonConvert.DeserializeObject(command);
                        ClientRequest(message, reader);
                    }
                }
            } catch (Exception) {
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
