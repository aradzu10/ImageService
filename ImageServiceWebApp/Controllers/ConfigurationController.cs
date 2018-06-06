using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWebApp.Models
{
    public class ConfigurationController : Controller
    {
        private ConfigurationModel model;

        public ConfigurationController()
        {
            model = ConfigurationModel.Instance;

            model.notify += Notify;
        }

        public void Notify()
        {
            ConfigurationView();
        }

        public ActionResult ConfigurationView()
        {
            return View(model);
        }

        public ActionResult DeleteConfirm(string toDelete)
        {
            model.NotifyRemoveHandler(toDelete);

            return RedirectToAction("ConfigView");
        }

        public ActionResult DeleteCancel()
        {
            return RedirectToAction("ConfigView");
        }
    }
}