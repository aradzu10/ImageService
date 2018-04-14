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

        /// <summary>
        /// backup image command
        /// </summary>
        /// <param name="imageFileHandler_">need image file handler</param>
        public BackupImageCommand(ImageServiceFileHandler imageFileHandler_)
        {
            imageFileHandler = imageFileHandler_;
        }

        /// <summary>
        /// execute backup command
        /// </summary>
        /// <param name="args">args for command</param>
        /// <returns>exit code - if success or were an error</returns>
        public ExitCode Execute(string[] args)
        {
            return imageFileHandler.BackupImage(args[0]);
        }
    }
}
