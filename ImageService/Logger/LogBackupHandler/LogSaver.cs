using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Enums;
using Logger;
using Logger.Message;

namespace ImageService.Logger.LogBackupHandler
{
    public class LogSaver
    {
        private string path;

        public LogSaver(string path)
        {
            this.path = path;
            try
            {
                File.Delete(path);
            } catch (Exception) {}
        }

        public LogSaver() : this("log.txt") {}

        public void WriteMessage(Object sender, MessageRecievedEventArgs e)
        {
            // check - mutex needed? maybe while file open
            try
            {
                File.AppendAllText(path, StatusToString(e.Status) + Environment.NewLine
                    + e.Message + Environment.NewLine);
            } catch (Exception) {}
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
    }
}
