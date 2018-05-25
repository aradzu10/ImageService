using ImageServiceUI.Model;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageServiceUI.ViewModel
{
    class SettingsVM
    {
        private SettingsModel settings;
        private String selectedItem;
        public ICommand RemoveCommand { get; set; }

        public string OutputDirectory { get { return settings.GetOutputDir(); } }

        public string SourceName { get { return settings.GetSourcegName(); } }

        public string LogName { get { return settings.GetLogName(); } }

        public string TumbnailSize { get { return settings.GetThumbSize(); } }

        public ObservableCollection<string> Handlers { get { return settings.GetDirectories(); } }

        public SettingsVM()
        {
            settings = SettingsModel.Instance;
            
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);

        }

        private bool CanRemove(object obj)
        {
            return selectedItem != null ? true : false;
        }

        public string SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                var command = RemoveCommand as DelegateCommand<object>;
                command.RaiseCanExecuteChanged();
            }
        }

        private void OnRemove(object obj)
        {
            settings.NotifyRemoveHandler(selectedItem);
        }

    }
}
