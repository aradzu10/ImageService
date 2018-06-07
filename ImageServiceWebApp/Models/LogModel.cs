using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ImageServiceWebApp.Models.Messages;

namespace ImageServiceWebApp.Models
{
    public class LogModel
    {
        public delegate void NotifyEvent();
        public NotifyEvent notify;

        public string lastType { get; set; }
        private static LogModel instance;
        public List<LogMessage> logMessages { get; private set; }
        private List<LogMessage> selectedLogMessages;
        public List<LogMessage> SelectedLogMessages
        {
            get
            {
                if (selectedLogMessages.Count == 0)
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
            logMessages.Add(new LogMessage(MessageTypeEnum.L_FAIL,"fail"));
            logMessages.Add(new LogMessage(MessageTypeEnum.L_INFO,"info"));
            logMessages.Add(new LogMessage(MessageTypeEnum.L_WARNING,"warning"));
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
            notify?.Invoke();
        }
    }
}