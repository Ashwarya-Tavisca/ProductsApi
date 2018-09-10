using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Admin()
        {
            return RedirectToAction("Index", "Admin");
        }

        // GET: Default/Create
        public ActionResult User()
        {
            return RedirectToAction("Index", "User");
        }
    }
}