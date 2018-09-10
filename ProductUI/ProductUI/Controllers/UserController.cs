using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductUI.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View("~/Views/User/User.cshtml");
        }
        public ActionResult Activity()
        {
            return RedirectToAction("Index", "Activity");
        }
        public ActionResult Air()
        {
            return RedirectToAction("Index", "Air");
        }
        public ActionResult Car()
        {
            return RedirectToAction("Index", "Car");
        }
        public ActionResult Hotel()
        {
            return RedirectToAction("Index", "Hotel");
        }
    }
}