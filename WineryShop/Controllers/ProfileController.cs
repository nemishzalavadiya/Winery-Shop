using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WineryShop.Core.Models;

namespace WineryShop.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            
            if (Session["Username"] == null)
            {
                TempData["msg"] = "Please Login First !";
                return RedirectToAction("Index", "Home");
            }
            string user = Session["Username"].ToString();
            ConModel11 db = new ConModel11();
            var data = db.Logins.Where(x => x.Username == user).ToList();
            ViewBag.profileData = data;
            return View("~/Views/Profile.cshtml");
        }

    }
}