using ImageServiceUI.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceUI.Model
{
    class LogModel
    {
        private static LogModel instance;
        private List<LogMessage> logMessages;
        public event PropertyChangedEventHandler PropertyChanged;

        private LogModel() { logMessages = new List<LogMessage>(); }

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
            private set {}
        }

        public void AddMassage(object sender, MessageRecievedEventArgs message)
        {
            logMessages.Add(new LogMessage(message));
            OnPropertyChanged("LogMessages");
        }
        
        public ObservableCollection<LogMessage> LogMessages
        {
            get
            {
                try
                {
                    return new ObservableCollection<LogMessage>(logMessages);
                }
                catch (Exception)
                {
                    return LogMessages;
                }
            }
            private set {}
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
