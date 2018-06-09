using ImageServiceWebApp.Models;
using ImageServiceWebApp.Models.PhotosHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWebApp.Controllers
{
    public class PhotosController : Controller
    {
        private static PhotosModel model = PhotosModel.Instance;

        public PhotosController() { }

        public ActionResult PhotosView()
        {
            return View(model);
        }

        public ActionResult DeleteConfirm(int toDelete)
        {
            model.NotifyDeletePhoto(toDelete);

            Thread.Sleep(1000);

            return RedirectToAction("PhotosView");
        }

        public ActionResult DeleteCancel()
        {
            return RedirectToAction("PhotosView");
        }
    }
}