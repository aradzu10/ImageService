using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ImageService.Enums;

namespace ImageService.FileHandler
{
    public interface IFileHandler
    {
        /// <summary>
        /// The Function Addes A file to the system
        /// </summary>
        /// <param name="path">The Path of the Image from the file</param>
        /// <returns>Indication if the Addition Was Successful</returns>
        ExitCode MoveFile(string from, string to);

        ExitCode CreateDir(string path);

        Image CreateThumbnail(string imagePath, int size, out ExitCode status);

        ExitCode SaveImage(string path, Image image);
    }
}
