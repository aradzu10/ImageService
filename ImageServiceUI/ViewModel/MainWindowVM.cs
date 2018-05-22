using ImageServiceUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System.ComponentModel;

namespace ImageServiceUI.ViewModel
{
    class MainWindowVM
    {
        public ICommand CloseCommand { get; set; }

        private MainWindowModel MainWindowModel;
        public bool IsConnected
        {
            get
            {
                return MainWindowModel.IsConnected;
            }
        }

        public MainWindowVM()
        {
            MainWindowModel = new MainWindowModel();

            CloseCommand = new DelegateCommand<object>(OnClose, CanDo);
        }

        private void OnClose(object obj)
        {
            MainWindowModel.CloseCommunication();
        }

        private bool CanDo(object obj)
        {
            return true;
        }
    }
}
