using ImageServiceUI.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceUI.Model
{
    class LogModel
    {
        private static LogModel instance;
        private List<LogMessage> logMessages;

        private LogModel() { }

        public static LogModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogModel();
                }
                return instance;
            }
            private set { }
        }

        public void AddMassage(object sender, MessageRecievedEventArgs message)
        {
            logMessages.Add(new LogMessage(message));
        }
        
    }
}
