using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        public LogSaver() : this("log.txt") {}

        public void writeMessage(Object sender, MessageRecievedEventArgs e)
        {
            // check - mutex needed? maybe while file open
            try
            {
                File.AppendAllText(path, statusToString(e.Status) + Environment.NewLine
                    + e.Message + Environment.NewLine);
            } catch (Exception) {}
        }

        private string statusToString(MessageTypeEnum messageType)
        {
            switch (messageType)
            {
                case MessageTypeEnum.INFO: return "INFO";
                case MessageTypeEnum.WARNING: return "WARNING";
                case MessageTypeEnum.FAIL: return "FAIL";
                default: return "";
            }
        }
    }
}
