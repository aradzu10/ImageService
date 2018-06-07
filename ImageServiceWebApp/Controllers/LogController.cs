using ImageServiceWebApp.Models;
using ImageServiceWebApp.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWebApp.Controllers
{
    public class LogController : Controller
    {
        private static LogModel model = LogModel.Instance;

        public LogController()
        {
            model.notify += Notify;
        }

        public void Notify()
        {
            LogMessageUpdate(model.lastType);
            LogView();
        }

        public ActionResult LogView()
        {
            return View(model);
        }

        public void LogMessageUpdate(string type)
        {
            if (type != "")
            {
                model.SelectedLogMessages.Clear();
                foreach (LogMessage item in model.logMessages)
                {
                    if (item.Type == type)
                    {
                        model.AddToSelected(item);
                    }
                }
            }
            else
            {
                model.SelectedLogMessages.Clear();
            }
        }

        [HttpPost]
        public ActionResult LogChangeFilter(FormCollection form)
        {
            string type = form["typeFilter"].ToString();
            model.lastType = type;
            LogMessageUpdate(type);

            return RedirectToAction("LogView");
        }
    }
}