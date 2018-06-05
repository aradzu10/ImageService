using ImageService.Commands;
using ImageService.FileHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Enums;

namespace ImageService.Controller
{
    public class ImageController : IImageController
    {
        private ImageServiceFileHandler imageFileHandler;                      // The Modal Object
        private Dictionary<Command, ICommand> commands;

        public ImageController(ImageServiceFileHandler imageFileHandler_)
        {
            imageFileHandler = imageFileHandler_;                    // Storing the Modal Of The System
            commands = new Dictionary<Command, ICommand>()
            {
                { Command.BackupFile, new BackupImageCommand(imageFileHandler) }
            };
        }
        public ExitCode ExecuteCommand(Command commandID, string[] args, string[] output)
        {
            if (commands.ContainsKey(commandID) == false) return ExitCode.Failed;
            return commands[commandID].Execute(args, output);
        }
    }
}
