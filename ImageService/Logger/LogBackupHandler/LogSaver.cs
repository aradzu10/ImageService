using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Enums;
using Logger;
using Messages;

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
                // check - json write in one line
                File.AppendAllText(path, e.Serialize() + "\n");
            } catch (Exception) {}
        }
    }
}
