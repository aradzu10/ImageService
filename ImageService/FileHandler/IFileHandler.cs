using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageService.Modal
{
    public interface IFileHandler
    {
        /// <summary>
        /// The Function Addes A file to the system
        /// </summary>
        /// <param name="path">The Path of the Image from the file</param>
        /// <returns>Indication if the Addition Was Successful</returns>
        bool MoveFile(string from, string to);

        void CreateDir(string path);

        Image CreateThumbnail(string imagePath, int size); // should return Image

        bool SaveImage(string path, Image image);

        bool CheckIfFileExist(string path);
    }
}
