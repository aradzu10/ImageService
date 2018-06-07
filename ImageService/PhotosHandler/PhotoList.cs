using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.PhotosHandler
{
    public class PhotoList
    {
        public List<PhotoPackage> Photos { get; set; }

        public PhotoList()
        {
            Photos = new List<PhotoPackage>();
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static PhotoList Deserialize(string obj)
        {
            return JsonConvert.DeserializeObject<PhotoList>(obj);
        }
    }
}
