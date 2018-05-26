using ImageService.Enums;
using ImageService.Messages;
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
    /// <summary>
    /// current project image handler
    /// to do all with the same path
    /// and keep thumbnail size
    /// </summary>
    /// doucumented only what aint obvius
    public class ImageServiceFileHandler
    {
        private string outputFolder;
        private int thumbnailSize;  
        private IFileHandler fileHandler;

        public ImageServiceFileHandler(IFileHandler file_Handler, out ExitCode status)
        {
            Settings settings = Settings.Instance;
            outputFolder = settings.OutputPath;
            thumbnailSize = settings.ThumbSize;
            fileHandler = file_Handler;
            status = ExitCode.Success;
            if (!Directory.Exists(outputFolder))
            {
                try
                {
                    DirectoryInfo di = Directory.CreateDirectory(outputFolder);
                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                }
                catch (Exception)
                {
                    status = ExitCode.F_Create_Dir;
                }
            }
        }

        public ImageServiceFileHandler(out ExitCode status) :
            this(new FileHandler(), out status) { }

        /// <summary>
        /// backup currnt image
        /// </summary>
        /// <param name="imagePath">image</param>
        /// <returns>if success or the reson of failuer</returns>
        public ExitCode BackupImage(string imagePath)
        {
            // make sure file aint lock
            ExitCode status;
            while(IsFileLocked(imagePath))
            {
                System.Threading.Thread.Sleep(500);
            }

            DateTime date = GetImageDate(imagePath);

            string imageName = Path.GetFileName(imagePath);
            string imageDest = outputFolder + "\\" + date.Year + "\\" + date.Month;
            string imageThumbDest = outputFolder + "\\Thumbnails\\" + date.Year + "\\" + date.Month;

            status = fileHandler.CreateDir(imageDest);
            if (status != ExitCode.Success) return ExitCode.F_Create_Dir;
            status = fileHandler.CreateDir(imageThumbDest);
            if (status != ExitCode.Success) return ExitCode.F_Create_Dir;

            Image thumbImage = fileHandler.CreateThumbnail(imagePath, thumbnailSize, out status);
            if (status != ExitCode.Success) return ExitCode.F_Create_Thumb;
            status = fileHandler.SaveImage(imageThumbDest + "\\" + imageName, thumbImage);
            if (status != ExitCode.Success) return ExitCode.F_Create_Thumb;

            status = fileHandler.MoveFile(imagePath, imageDest);
            if (status != ExitCode.Success) return ExitCode.F_Move;

            return ExitCode.Success;
        }

        private DateTime GetImageDate(string imagePath)
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
                    date = DateTime.Parse(dateTaken);
                }
                return date;
            }
            catch (Exception)
            {
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
