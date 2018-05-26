using ImageServiceUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceUI.ViewModel
{
    class LogVM : INotifyPropertyChanged
    {
        private LogModel logModel;
        public event PropertyChangedEventHandler PropertyChanged;

        public LogVM()
        {
            logModel = LogModel.Instance;
            logModel.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e);
            
            };
        }

        private void NotifyPropertyChanged(PropertyChangedEventArgs prop)
        {
            PropertyChanged?.Invoke(this, prop);
        }

        public ObservableCollection<LogMessage> LogMessages
        {
            get { return logModel.LogMessages; }
            private set {}
        }
    }
}
