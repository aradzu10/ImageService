using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ImageServiceWebApp.Models.Messages;

namespace ImageServiceWebApp.Models
{
    public class LogModel
    {
        public string lastType { get; set; }
        private static LogModel instance;
        public List<LogMessage> logMessages { get; private set; }
        private List<LogMessage> selectedLogMessages;
        public List<LogMessage> SelectedLogMessages
        {
            get
            {
                if (selectedLogMessages.Count == 0 && lastType == "")
                {
                    selectedLogMessages.AddRange(logMessages);
                }
                return selectedLogMessages;
            }
            private set { }
        }

        private LogModel()
        {
            logMessages = new List<LogMessage>();
            selectedLogMessages = new List<LogMessage>();
            lastType = "";
        }

        public void AddToSelected(LogMessage message)
        {
            selectedLogMessages.Add(message);
        }

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