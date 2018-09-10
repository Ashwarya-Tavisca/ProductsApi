using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductUI.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View("~/Views/Admin/Admin.cshtml");
        }
        public ActionResult Activity()
        {
            return RedirectToAction("Add", "Activity");
        }
        public ActionResult Air()
        {
            return RedirectToAction("Add", "Air");
        }
        public ActionResult Car()
        {
            return RedirectToAction("Add", "Car");
        }
        public ActionResult Hotel()
        {
            return RedirectToAction("Add", "Hotel");
        }
    }
}