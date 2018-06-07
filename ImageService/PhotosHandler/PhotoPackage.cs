using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.PhotosHandler
{
    public class PhotoPackage
    {
        public string PhotoPath { get; set; }
        public string PhotoThumbnailPath { get; set; }
        public byte[] Photo { get; set; }
        public byte[] PhotoThumbnail { get; set; }

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
