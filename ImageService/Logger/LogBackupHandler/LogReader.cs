using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Enums;
using Messages;
using Newtonsoft.Json;

namespace ImageService.Logger.LogBackupHandler
{
    public class LogReader
    {
        string path;

        public LogReader(string path_)
        {
            path = path_;
        }
        public LogReader() : this("log.txt") { }

        public LogFileReader ReadLogFile()
        {
            return new LogFileReader(path);
        }

        public class LogFileReader
        {
            int lastLineNumber;
            string path;

            public LogFileReader(string path_)
            {
                lastLineNumber = 0;
                path = path_;
            }

            public ExitCode NextLine(out string line)
            {
                line = "";
                // check - mutex? maybe while file open
                try
                {
                    // check - if last line == 0 ok
                    // check - what happen in EOF and return exit code done
                    // check - json line by line
                    var lines = File.ReadLines(path);
                    line = lines.Skip(lastLineNumber).First();
                    lastLineNumber++;
                }
                catch (Exception) { return ExitCode.Failed; }

                return ExitCode.Success;
            }
        }

    }
}
