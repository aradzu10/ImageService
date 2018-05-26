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
    class SettingsModel
    {
        private static SettingsModel instance;
        public Settings Settings { get; private set; }
        public event EventHandler<MessageRecievedEventArgs> NotifyHandlerChange;
        public event PropertyChangedEventHandler PropertyChanged;

        private SettingsModel()
        {
            Settings = Settings.Instance;
        }

        public static SettingsModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SettingsModel();
                }
                return instance;
            }
            private set {}
        }

        public void SetSettings(object sender, Settings settings)
        {
            Settings.SetSettings(settings);
            OnPropertyChanged("OutputDirectory");
            OnPropertyChanged("SourceName");
            OnPropertyChanged("LogName");
            OnPropertyChanged("ThumbnailSize");
            OnPropertyChanged("Handlers");
        }

        public void RemoveHandler(object sender, String dir)
        {
            Settings.RemoveDirectories(dir);
            OnPropertyChanged("Handlers");
        }

        public void NotifyRemoveHandler(string dir)
        {
            MessageRecievedEventArgs messageRecievedEventArgs = new MessageRecievedEventArgs(MessageTypeEnum.REMOVE_HANDLER, dir);
            NotifyHandlerChange?.Invoke(this, messageRecievedEventArgs);
        }

        public String OutputDir
        {
            get
            {
                return Settings.OutputPath;
            }
            private set {}
        }

        public String LogName
        {
            get
            {
                return Settings.LogName;
            }
            private set {}
        }

        public String SourcegName
        {
            get
            {
                return Settings.SourceName;
            }
            private set {}
        }

        public String ThumbSize
        {
            get
            {
                if (Settings.ThumbSize != -1)
                    return "" + Settings.ThumbSize;
                return "";
            }
            private set { }
            
        }

        public ObservableCollection<string> Directories
        {
            get
            {
                return new ObservableCollection<string>(Settings.Directories);
            }
            private set {}
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
