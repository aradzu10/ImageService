using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ImageServiceWebApp.Models.PhotosHandler;

namespace ImageServiceWebApp.Models
{
    public class ViewPhotoModel
    {
        public PhotoPackage selectedPhoto { get; set; }

        public ViewPhotoModel() {}
    }
}