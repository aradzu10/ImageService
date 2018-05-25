using ImageServiceUI.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private SettingsModel()
        {
            Settings = new Settings();
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
            Settings = settings;
        }

        public void RemoveHandler(object sender, String dir)
        {
            Settings.RemoveDirectories(dir);
        }

        public void NotifyRemoveHandler(string dir)
        {
            MessageRecievedEventArgs messageRecievedEventArgs = new MessageRecievedEventArgs(MessageTypeEnum.REMOVE_HANDLER, dir);
            NotifyHandlerChange?.Invoke(this, messageRecievedEventArgs);
        }

        public String GetOutputDir()
        {
            return Settings.OutputPath;
        }

        public String GetLogName()
        {
            return Settings.LogName;
        }

        public String GetSourcegName()
        {
            return Settings.SourceName;
        }

        public String GetThumbSize()
        {
            return "" + Settings.ThumbSize;
        }

        public ObservableCollection<string> GetDirectories()
        {
            return new ObservableCollection<string>(Settings.Directories);
        }
    }
}
