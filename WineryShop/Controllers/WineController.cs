using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WineryShop.Core.Models;
namespace WineryShop.Controllers
{
    public class WineController : Controller
    {
        // GET: Wine
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            ConModel11 db = new ConModel11();
            var model = db.Wines.First(x => x.Id == id);
            return View(model);
        }
    }
}