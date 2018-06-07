using ImageServiceWebApp.Models;
using ImageServiceWebApp.Models.PhotosHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWebApp.Controllers
{
    public class PhotosController : Controller
    {
        private static PhotosModel model = PhotosModel.Instance;

        public PhotosController()
        {
            model.notify += Notify;
        }

        public void Notify()
        {
            PhotosView();
        }

        public ActionResult PhotosView()
        {
            return View(model);
        }

        public ActionResult DeleteConfirm(string toDelete)
        {
            model.NotifyDeletePhoto(toDelete);

            return RedirectToAction("PhotosView");
        }

        public ActionResult DeleteCancel()
        {
            return RedirectToAction("PhotosView");
        }
    }
}