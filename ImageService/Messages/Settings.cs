using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ImageService.Messages
{
    public class Settings
    {
        private static Settings instnace;
        
        public string OutputPath { get; set; }
        public string SourceName { get; set; }
        public string LogName { get; set; }
        public int ThumbSize { get; set; }
        public List<string> Directories { get; set; }
        public int PicturesCounter { get; set; }

        private Settings()
        {
            OutputPath = "";
            SourceName = "";
            LogName = "";
            ThumbSize = -1;
            PicturesCounter = 0;
            Directories = new List<string>();
        }

        public static Settings Instance
        {
            get
            {
                if (instnace == null)
                {
                    instnace = new Settings();
                }
                return instnace;
            }
            private set {}
        }

        public void SetSettings(Settings settings)
        {
            SetSettings(settings.OutputPath, settings.SourceName, settings.LogName, settings.ThumbSize, settings.PicturesCounter);
            Directories = new List<string>(settings.Directories); 
        }

        public void SetSettings(string outputPath, string sourceName, string logName, int thumbSize, int picCounter)
        {
            OutputPath = outputPath;
            SourceName = sourceName;
            LogName = logName;
            ThumbSize = thumbSize;
            PicturesCounter = picCounter;
        }

        public void AddDirectories(string dir)
        {
            Directories.Add(dir);
        }

        public void RemoveDirectories(string dir)
        {
            Directories.Remove(dir);
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Settings Deserialize(string obj)
        {
            return JsonConvert.DeserializeObject<Settings>(obj);
        }
    }
}
