using ImageServiceWebApp.Models;
using ImageServiceWebApp.Models.PhotosHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWebApp.Controllers
{
    public class ViewPhotoController : Controller
    {
        private static ViewPhotoModel model = new ViewPhotoModel();

        public ViewPhotoController() { }

        public ActionResult ViewPhotoView()
        {
            return View(model);
        }

        public ActionResult ViewPhoto(int selectedPhoto)
        {
            model.selectedPhoto = PhotoDB.GetPhoto(selectedPhoto);
            return RedirectToAction("ViewPhotoView");
        }

        public ActionResult DeletePhoto()
        {
            return RedirectToAction("DeletePhoto", "DeletePhoto", new { selectedPhoto = PhotoDB.GetIndex(model.selectedPhoto) });
        }
    }
}