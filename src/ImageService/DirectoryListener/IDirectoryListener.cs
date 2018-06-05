using ImageService.Commands;
using ImageService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.DirectoryListener
{
    /// <summary>
    /// directory listener
    /// must start and stop 
    /// </summary>
    interface IDirectoryListener
    {
        ExitCode StartListenDirectory(string dirPath);
        void StopListenDirectory(object sender, System.EventArgs e);
    }
}
