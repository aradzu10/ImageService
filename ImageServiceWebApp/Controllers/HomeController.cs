using ImageServiceWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWebApp.Controllers
{
    public class HomeController : Controller
    {
        private static HomeModel model = new HomeModel();

        public HomeController()
        {
            model.notify += Notify;
        }

        public void Notify()
        {
            HomeView();
        }

        public ActionResult HomeView()
        {
            return View(model);
        }
    }
}