using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.MyLogger
{
    class MyLogger
    {
        public static void Log(string mess)
        {
            File.AppendAllText(@"C:\Users\Matan\Desktop\log.txt", mess + Environment.NewLine);
        }
    }
}
