using ImageService.Infrastructure;
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

namespace ImageService.Modal
{
    public class FileHandler : IFileHandler
    {
        public bool CheckIfFileExist(string path)
        {
            return File.Exists(path);
        }

        public void CreateDir(string path)
        {
            Directory.CreateDirectory(path);
        }

        public Image CreateThumbnail(string imagePath, int size)
        {
            throw new NotImplementedException();
        }

        public bool MoveFile(string from, string to)
        {
            CreateDir(to);
            string filenameNoPath = Path.GetFileNameWithoutExtension(from);
            string temppath = Path.GetDirectoryName(from);
            string extension = Path.GetExtension(from);
            string destPath = to + "\\" + filenameNoPath + extension;
            int counter = 0;

            if (!File.Exists(from))
            {
                return false;
            }

            try
            {
                if (!File.Exists(destPath))
                {
                    File.WriteAllBytes(destPath, File.ReadAllBytes(from));
                }
                else
                {
                    do
                    {
                        counter++;
                        destPath = to + "\\" + filenameNoPath + " (" + counter.ToString() + ")" + extension; // check - beginning with dot
                    } while (File.Exists(destPath));

                    File.WriteAllBytes(destPath, File.ReadAllBytes(from));
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool SaveImage(string path, Image image)
        {
            try
            {
                image.Save(path);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
