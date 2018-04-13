using ImageService.Enums;
using ImageService.FileHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    public class BackupImageCommand : ICommand
    {
        private ImageServiceFileHandler imageFileHandler;

        public BackupImageCommand(ImageServiceFileHandler imageFileHandler_)
        {
            imageFileHandler = imageFileHandler_;
        }

        public ExitCode Execute(string[] args)
        {
            return imageFileHandler.BackupImage(args[0]);
        }
    }
}
