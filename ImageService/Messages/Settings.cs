using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Messages
{
    public class Settings
    {
        public string outputPath { get; }
        public string sourceName { get; }
        public string logName { get; }
        public string logPath { get; }
        public int thumbSize { get; }
        public List<string> directories { get; }

        public Settings(string outputPath, string sourceName, string logName, string logPath, int thumbSize)
        {
            this.outputPath = outputPath;
            this.sourceName = sourceName;
            this.logName = logName;
            this.logPath = logPath;
            this.thumbSize = thumbSize;
            this.directories = new List<string>();
        }

        public void AddDirectories(string dir)
        {
            directories.Add(dir);
        }

        public void RemoveDirectories(string dir)
        {
            directories.Remove(dir);
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
