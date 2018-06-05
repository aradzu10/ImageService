using ImageService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    /// <summary>
    /// exexutable command
    /// </summary>
    public interface ICommand
    {
        ExitCode Execute(string[] args, string[] output);
    }
}
