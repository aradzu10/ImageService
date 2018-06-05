using ImageService.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logger
{
    class LogBackup
    {
        private static LogBackup logBackupInstance = null;
        public List<MessageRecievedEventArgs> messages { get; }

        public LogBackup()
        {
            messages = new List<MessageRecievedEventArgs>();
        }

        public void WriteMessage(Object sender, MessageRecievedEventArgs e)
        {
            messages.Add(e);
        }

        public static LogBackup Instance
        {
            get
            {
                if (logBackupInstance == null)
                {
                    logBackupInstance = new LogBackup();
                }
                return logBackupInstance;
            }
        }

    }
}
