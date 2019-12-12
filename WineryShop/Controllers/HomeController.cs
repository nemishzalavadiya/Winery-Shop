using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WineryShop.Core.Models;
namespace WineryShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ConModel11 db = new ConModel11();
            var model = db.Wines.ToList();
            Session["Categories"] = db.Categories.ToList();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult wines(int id) {
            ConModel11 db = new ConModel11();
            if (id == -1)
            {
                return View(db.Wines.ToList());
            }
                       
            var model = db.Wines.Where(x=>x.CategoryId == id);
            return View(model);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Developer Details";

            return View();
        }
    }
}