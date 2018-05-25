using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ImageServiceUI.Messages
{
    public class Settings
    {
        public string OutputPath { get; }
        public string SourceName { get; }
        public string LogName { get; }
        public int ThumbSize { get; }
        public List<string> Directories { get; }

        public Settings() : this("", "", "", 0) {}

        public Settings(string outputPath, string sourceName, string logName, int thumbSize)
        {
            this.OutputPath = outputPath;
            this.SourceName = sourceName;
            this.LogName = logName;
            this.ThumbSize = thumbSize;
            this.Directories = new List<string>();
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
