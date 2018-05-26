using ImageServiceUI.Communication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceUI.Model
{
    class MainWindowModel
    {

        private HandleCommunication handler;

        public bool IsConnected { get; private set; }

        public MainWindowModel()
        {
            handler = new HandleCommunication();

            SettingsModel settingsModel = SettingsModel.Instance;
            handler.SetSettings += settingsModel.SetSettings;
            handler.OnHandelRemove += settingsModel.RemoveHandler;

            settingsModel.NotifyHandlerChange += handler.SendMessage;

            LogModel logModel = LogModel.Instance;
            handler.OnLogMessage += logModel.AddMassage;

            IsConnected = handler.ConnectToServer();
            new Task(() =>
            {
                handler.Communication();
            }).Start();
        }


        public void CloseCommunication()
        {
            handler.CloseClient();
        }
    }
}
