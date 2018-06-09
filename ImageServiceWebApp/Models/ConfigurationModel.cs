using ImageServiceWebApp.Models.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImageServiceWebApp.Models
{
    public class ConfigurationModel
    {
        private Settings settings;
        public event EventHandler<MessageRecievedEventArgs> NotifyHandlerChange;

        private static ConfigurationModel instance;
        public static ConfigurationModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConfigurationModel();
                }
                return instance;
            }
            private set { }
        }

        [Required]
        [Display(Name = "Output Directory")]
        public String OutputDir
        {
            get
            {
                return settings.OutputPath;
            }
            private set { }
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Log Name")]
        public String LogName
        {
            get
            {
                return settings.LogName;
            }
            private set { }
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Source Name")]
        public String SourceName
        {
            get
            {
                return settings.SourceName;
            }
            private set { }
        }

        [Required]
        [Display(Name = "Thumbnail Size")]
        public String ThumbSize
        {
            get
            {
                if (settings.ThumbSize != -1)
                    return "" + settings.ThumbSize;
                return "";
            }
            private set { }

        }

        [Required]
        [Display(Name = "Handlers")]
        public List<string> Directories
        {
            get
            {
                return settings.Directories;
            }
            private set { }
        }

        private ConfigurationModel() {
            settings = Settings.Instance;
        }

        public void SetSettings(object sender, Settings settings_)
        {
            settings.SetSettings(settings_);
        }

        public void RemoveHandler(object sender, String dir)
        {
            settings.RemoveDirectories(dir);
        }

        public void NotifyRemoveHandler(string dir)
        {
            MessageRecievedEventArgs messageRecievedEventArgs = new MessageRecievedEventArgs(MessageTypeEnum.REMOVE_HANDLER, dir);
            NotifyHandlerChange?.Invoke(this, messageRecievedEventArgs);
        }
    }
}