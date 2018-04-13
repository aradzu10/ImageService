using ImageService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.DirectoryListener
{
    interface IDirectoryListener
    {
        void StartListenDirectory(string dirPath);
        void StopListenDirectory(object sender, System.EventArgs e);
    }
}
