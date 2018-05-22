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

            //IsConnected = handler.ConnectToServer();
            // TODO - sign to events
            //handler.Communication();
        }


        public void CloseCommunication()
        {
            handler.CloseClient();
        }
    }
}
