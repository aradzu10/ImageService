using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ImageService.Enums;

namespace ImageService.FileHandler
{
    /// <summary>
    /// minimum file handler behaviors
    /// </summary>
    public interface IFileHandler
    {
        ExitCode MoveFile(string from, string to);

        ExitCode CreateDir(string path);

        Image CreateThumbnail(string imagePath, int size, out ExitCode status);

        ExitCode SaveImage(string path, Image image);
    }
}
