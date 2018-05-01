using ImageService.Enums;
using ImageService.ListenerManager;
using Logger.Message;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static ImageService.Logger.LogBackupHandler.LogReader;

namespace ImageService.Communication.ClientManager
{
    class ClientCommunication
    {
        TcpClient client;
        public event EventHandler<String> OnRemoveDir;

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
                    writer.Write(SC_MessageType.S_outputDir + "\n" + settings.outputPath);
                    writer.Write(SC_MessageType.S_sourceName + "\n" + settings.sourceName);
                    writer.Write(SC_MessageType.S_logName + "\n" + settings.logName);
                    writer.Write(SC_MessageType.S_thumbSize + "\n" + settings.thumbSize);
                    foreach (var dir in settings.directories)
                    {
                        writer.Write(SC_MessageType.S_dirListener + "\n" + dir);
                    }

                    string curLine;
                    ExitCode code;
                    while ((code = readLogFile.NextLine(out curLine)) == ExitCode.Success)
                    {
                        writer.Write(curLine);
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
                    writer.Write(StatusToString(e.Status) + "\n" + e.Message);
                }
            } catch (Exception) {}
        }

        public void RemoveDir(Object sender, String dir)
        {
            try
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(SC_MessageType.RemoveHandler + "\n" + dir);
                }
            }
            catch (Exception) { }
        }

        private SC_MessageType StatusToString(MessageTypeEnum messageType)
        {
            switch (messageType)
            {
                case MessageTypeEnum.INFO: return SC_MessageType.L_info;
                case MessageTypeEnum.WARNING: return SC_MessageType.L_warning;
                case MessageTypeEnum.FAIL: return SC_MessageType.L_fail;
                default: return SC_MessageType.Unknown;
            }
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
                        string commandLine = reader.ReadLine();
                        ClientRequest(commandLine, reader);
                    }
                }
            } catch (Exception) {}
        }

        private void ClientRequest(string message, StreamReader reader)
        {
            SC_MessageType task;
            try
            {
                task = (SC_MessageType)Enum.Parse(typeof(SC_MessageType), message);
            } catch (Exception) { return; }
            switch (task)
            {
                case SC_MessageType.RemoveHandler:
                    string dir = reader.ReadLine();
                    OnRemoveDir?.Invoke(this, dir);
                    break;
                default:
                    break;

            }
        }



        public void StopCommunication(object sender, System.EventArgs e)
        {
            client?.Close();
        }
    }
}
