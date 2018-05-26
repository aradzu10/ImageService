using ImageServiceUI.Model;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageServiceUI.ViewModel
{
    class SettingsVM : INotifyPropertyChanged
    {
        private SettingsModel settings;
        private String selectedItem;
        public ICommand RemoveCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public string OutputDirectory { get { return settings.OutputDir; } }

        public string SourceName { get { return settings.SourcegName; } }

        public string LogName { get { return settings.LogName; } }

        public string ThumbnailSize { get { return settings.ThumbSize; } }

        public ObservableCollection<string> Handlers { get { return settings.Directories; } }

        public SettingsVM()
        {
            settings = SettingsModel.Instance;
            settings.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e);
            
            };
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);

        }

        private void NotifyPropertyChanged(PropertyChangedEventArgs prop)
        {
            PropertyChanged?.Invoke(this, prop);
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
