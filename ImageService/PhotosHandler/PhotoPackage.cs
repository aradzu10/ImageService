using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.PhotosHandler
{
    class PhotoPackage
    {
        public string PhotoPath { get; set; }

        public string PhotoThumbnailPath { get; set; }

        [JsonConverter(typeof(PhotoConverter))]
        public Image Photo { get; set; }

        [JsonConverter(typeof(PhotoConverter))]
        public Image PhotoThumbnail { get; set; }

        public PhotoPackage(string photoPath, string photoThumbnailPath, Image photo, Image photoThumb)
        {
            PhotoPath = photoPath;
            PhotoThumbnailPath = photoThumbnailPath;
            Photo = photo;
            PhotoThumbnail = photoThumb;
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
