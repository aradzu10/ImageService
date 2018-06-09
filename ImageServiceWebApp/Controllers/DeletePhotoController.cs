using ImageServiceWebApp.Models;
using ImageServiceWebApp.Models.PhotosHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWebApp.Controllers
{
    public class DeletePhotoController : Controller
    {
        private static DeletePhotoModel model = new DeletePhotoModel();

        public DeletePhotoController() { }

        public ActionResult DeletePhotoView()
        {
            return View(model);
        }

        public ActionResult DeletePhoto(int selectedPhoto)
        {
            model.selectedPhoto = PhotoDB.GetPhoto(selectedPhoto);
            return RedirectToAction("DeletePhotoView");
        }

        public ActionResult Delete()
        {
            return RedirectToAction("DeleteConfirm", "Photos", new { toDelete = PhotoDB.GetIndex(model.selectedPhoto) });
        }

        public ActionResult Cancel()
        {
            return RedirectToAction("DeleteCancel", "Photos");
        }
    }
}