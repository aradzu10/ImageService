using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ImageServiceWebApp.Models.PhotosHandler
{
    public class PhotoPackage
    {
        public string PhotoPath { get; set; }
        public string PhotoThumbnailPath { get; set; }
        public byte[] Photo { get; set; }
        public byte[] PhotoThumbnail { get; set; }

        public PhotoPackage() { }

        public PhotoPackage(string photoPath, string photoThumbnailPath, Image photo, Image photoThumb)
        {
            PhotoPath = photoPath;
            PhotoThumbnailPath = photoThumbnailPath;
            Photo = ImageToByteArray(photo);
            PhotoThumbnail = ImageToByteArray(photoThumb);
        }

        private byte[] ImageToByteArray(Image image)
        {
            var ms = new MemoryStream();
            image.Save(ms, image.RawFormat);
            return ms.ToArray();
        }

        public DateTime GetPhotoDate()
        {
            DateTime date;
            Regex r = new Regex(":");
            using (var ms = new MemoryStream(Photo))
            using (Image image = Image.FromStream(ms))
            {
                try
                {
                    PropertyItem propItem = image.GetPropertyItem(36867);
                    string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                    date = DateTime.Parse(dateTaken);
                } 
                catch (Exception)
                {
                    date = DateTime.Now;
                }
            }
            return date;
        }

        public string GetPhoto()
        {
            return Convert.ToBase64String(Photo);
        }

        public string GetPhotoThumbnail()
        {
            return Convert.ToBase64String(PhotoThumbnail);
        }

        public string GetPhotoName()
        {
            return Path.GetFileNameWithoutExtension(PhotoPath);
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static PhotoPackage Deserialize(string obj)
        {
            return JsonConvert.DeserializeObject<PhotoPackage>(obj);
        }
    }
}
