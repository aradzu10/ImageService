using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Commands;
using ImageService.Enums;

namespace ImageService.Controller
{
    public interface IImageController
    {
        ExitCode ExecuteCommand(Command commandID, string[] args);          // Executing the Command Requet
    }
}
