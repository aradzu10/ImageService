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
        private HomeModel model;

        public HomeController()
        {
            model = new HomeModel();

            model.NotifyEvent += ImageWeb;
        }

        // GET: ImageView
        //public ActionResult ImageWeb(object sender, EventArgs e)
        public void ImageWeb(object sender, EventArgs e)
        {
            ViewBag.NumOfPics = model.NumOfPics;
            ViewBag.IsConnected = model.IsConnected;

            //return View(ImageViewInfoObj);
        }
    }
}