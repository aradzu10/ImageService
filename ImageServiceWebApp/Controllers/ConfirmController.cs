using ImageServiceWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWebApp.Controllers
{
    public class ConfirmController : Controller
    {
        private ConfirmModel model;

        public ConfirmController()
        {
            model = new ConfirmModel();
        }

        public ActionResult ConfirmView()
        {
            return View(model);
        }

        public ActionResult DeleteHandler(string selectedHandler)
        {
            model.SelectedHandler = selectedHandler;
            return RedirectToAction("ConfirmView");
        }

        public ActionResult Delete()
        {
            return RedirectToAction("DeleteConfirm", "Configuration", new { toDelete = model.SelectedHandler });
        }

        public ActionResult Cancel()
        {
            return RedirectToAction("DeleteCancel", "Configuration");
        }
    }
}