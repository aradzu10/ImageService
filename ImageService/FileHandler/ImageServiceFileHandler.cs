﻿using ImageService.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ImageService.FileHandler
{
    public class ImageServiceFileHandler
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

        public ImageServiceFileHandler(string outputDir, int thumbSize) : this(outputDir, thumbSize, new FileHandler()) { }

        public ExitCode BackupImage(string imagePath) // check - should return error enum
        {
            MyLogger.MyLogger.Log("Backup: " + imagePath);
            ExitCode status;
            while(IsFileLocked(imagePath))
            {
                System.Threading.Thread.Sleep(500);
            }
            DateTime date = GetImageDate(imagePath, out status);
            if (status != ExitCode.Success) return status;
            string imageName = Path.GetFileName(imagePath);
            string imageDest = outputFolder + "\\" + date.Year + "\\" + date.Month;
            string imageThumbDest = outputFolder + "\\Thumbnails\\" + date.Year + "\\" + date.Month;
            status = fileHandler.CreateDir(imageDest); // check - handle if failed
            if (status != ExitCode.Success) return status;
            status = fileHandler.CreateDir(imageThumbDest); // check - handle if failed
            if (status != ExitCode.Success) return status;
            Image thumbImage = fileHandler.CreateThumbnail(imagePath, thumbnailSize, out status); // check - handle if failed
            if (status != ExitCode.Success) return status;
            status = fileHandler.SaveImage(imageThumbDest + "\\" + imageName, thumbImage); // check - handle if failed
            if (status != ExitCode.Success) return status;
            status = fileHandler.MoveFile(imagePath, imageDest); // check - handle if failed
            return status;
        }

        private DateTime GetImageDate(string imagePath, out ExitCode status)
        {
            DateTime date;
            try
            {
                Regex r = new Regex(":");
                using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                using (Image myImage = Image.FromStream(fs, false, false))
                {
                    PropertyItem propItem = myImage.GetPropertyItem(36867);
                    string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                    status = ExitCode.Success;
                    date = DateTime.Parse(dateTaken);
                }
                return date;
            }
            catch (Exception e)
            {
                status = ExitCode.Failed;
                return DateTime.Now;
            }

        }

        private bool IsFileLocked(string filePath)
        {
            FileStream stream = null;

            try
            {
                stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return false;
        }
    }
}
