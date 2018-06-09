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

        public HomeController() { }

        public ActionResult HomeView()
        {
            return View(model);
        }
    }
}