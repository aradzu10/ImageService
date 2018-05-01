using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Enums;

namespace ImageService.Logger.LogBackupHandler
{
    public class LogReader
    {
        private string path;

        public LogReader(string path)
        {
            this.path = path;
        }

        public LogReader() : this("log.txt") {}

        public ReadLogFile readLog()
        {
            return new ReadLogFile(path);
        }

        public class ReadLogFile
        {
            int lastLineNumber;
            string path;

            public ReadLogFile(string path_)
            {
                lastLineNumber = 0;
                path = path_;
            }

            public ExitCode nextLine(out string line)
            {
                line = "";
                // check - mutex? maybe while file open
                try
                {
                    // check - if last line == 0 ok
                    // check - what happen in EOF and return exit code done
                    var lines = File.ReadLines(path);
                    line = lines.Skip(lastLineNumber).First();
                    lastLineNumber++;
                } catch (Exception) { return ExitCode.Failed; }

                return ExitCode.Success;
            }
        }
    }
}
