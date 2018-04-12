using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ImageService.Modal
{
    class ImageServiceFileHandler
    {
        #region Members
        private string outputFolder;            // The Output Folder
        private int thumbnailSize;              // The Size Of The Thumbnail Size
        private IFileHandler fileHandler;
        #endregion

        public ImageServiceFileHandler(string outputDir, int thumbSize, IFileHandler file_Handler)
        {
            outputFolder = outputDir;
            thumbnailSize = thumbSize;
            fileHandler = file_Handler;
        }

        public ImageServiceFileHandler(string outputDir, int thumbSize) : this(outputDir, thumbSize, new FileHandler()) {}

        public bool BackupImage(string imagePath) // check - should return error enum
        {
            DateTime date = GetImageDate(imagePath);
            string imageDest = outputFolder + "\\" + date.Year + "\\" + date.Month;
            string imageThumbDest = outputFolder + "\\Thumbnails\\" + date.Year + "\\" + date.Month;
            fileHandler.CreateDir(imageDest); // check - handle if failed
            fileHandler.CreateDir(imageThumbDest); // check - handle if failed
            Image thumbImage = fileHandler.CreateThumbnail(imagePath, thumbnailSize); // check - handle if failed
            fileHandler.SaveImage(imageThumbDest, thumbImage); // check - handle if failed
            fileHandler.MoveFile(imagePath, imageDest); // check - handle if failed
        }

        private DateTime GetImageDate(string imagePath)
        {
            Regex r = new Regex(":");
            using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false))
            {
                PropertyItem propItem = myImage.GetPropertyItem(36867);
                string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                return DateTime.Parse(dateTaken);
            }

        }


    }
}
