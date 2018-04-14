using ImageService.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ImageService.FileHandler
{
    public class FileHandler : IFileHandler
    {
        public ExitCode CreateDir(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
                return ExitCode.Success;
            }
            catch (Exception)
            {
                return ExitCode.Failed;
            }
        }

        public Image CreateThumbnail(string imagePath, int size, out ExitCode status)
        {
            Image thumb = null, image = null;
            try
            {
                image = Image.FromFile(imagePath);
                thumb = new Bitmap(image, size, size);
                status = ExitCode.Success;
                return thumb;
            }
            catch (Exception)
            {
                thumb?.Dispose();
                status = ExitCode.Failed;
                return null;
            }
            finally
            {
                image?.Dispose();
            }
        }

        public ExitCode MoveFile(string from, string to)
        {
            CreateDir(to);
            int counter = 1;
            string filenameNoPath = Path.GetFileNameWithoutExtension(from);
            string extension = Path.GetExtension(from);
            string destPath = to + "\\" + filenameNoPath + extension;

            if (!File.Exists(from))
            {
                return ExitCode.F_Invalid_Input;
            }

            try
            {
                if (!File.Exists(destPath))
                {
                    File.Move(from, destPath);
                }
                else
                {
                    do
                    {
                        counter++;
                        destPath = to + "\\" + filenameNoPath + " (" + counter.ToString() + ")" + extension; 
                    } while (File.Exists(destPath));

                    File.Move(from, destPath);
                }
            }
            catch (Exception)
            {
                return ExitCode.Failed;
            }
            return ExitCode.Success;
        }

        public ExitCode SaveImage(string path, Image image)
        {
            int counter = 1;
            string filenameNoPath = Path.GetFileNameWithoutExtension(path);
            string temppath = Path.GetDirectoryName(path);
            string extension = Path.GetExtension(path);

            try
            {
                if (!File.Exists(path))
                {
                    image.Save(path);
                }
                else
                {
                    do
                    {
                        counter++;
                        path = temppath + "\\" + filenameNoPath + " (" + counter.ToString() + ")" + extension;
                    } while (File.Exists(path));
                    image.Save(path);
                }
                return ExitCode.Success;
            }
            catch (Exception)
            {
                return ExitCode.Failed;
            }
            finally
            {
                image?.Dispose();
            }
        }
    }
}
