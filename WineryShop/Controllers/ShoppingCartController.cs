using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WineryShop.Core.Models;
namespace WineryShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddToShoppingCart(int id)
        {
            if (Session["Username"] == null)
            {
                TempData["msg"] = "To Create Order Please Login First !";
                return RedirectToAction("Index","Home");
            }
            ConModel11 db = new ConModel11();
            ShoppingCartItem s = new ShoppingCartItem();
            s.Qty = 1;
            s.WineId = id;
            s.UserId = Session["Username"].ToString();
            s.WineName = db.Wines.First(x=>x.Id==id).Name;
            s.price = db.Wines.First(x => x.Id == id).Price;
            db.ShoppingCartItems.Add(s);
            db.SaveChanges();
        
            
            return RedirectToAction("show");
        }
        public ActionResult show() {
            if (Session["Username"] == null)
            {
                TempData["msg"] = "Please Login First";
                return RedirectToAction("Index", "Home");
            }
            ConModel11 db = new ConModel11();
            var temp = Session["Username"].ToString();
            var model = db.ShoppingCartItems.Where(x=>x.UserId.Equals(temp) ).ToList();
            return View(model);
        }
        public ActionResult RemoveFromShoppingCart(int id) {
            if (Session["Username"] == null)
            {
                TempData["msg"] = "To Create Order Please Login First !";
                return RedirectToAction("Index", "Home");
            }
            ConModel11 db = new ConModel11();
            var s = db.ShoppingCartItems.First(x=>x.Id==id);
            s.Qty = s.Qty - 1;
            if (s.Qty == 0)
            {
                db.ShoppingCartItems.Remove(s);
            }
            db.SaveChanges();
            return RedirectToAction("show");
        }
        public ActionResult AddToAgainShoppingCart(int id)
        {
            if (Session["Username"] == null)
            {
                TempData["msg"] = "To Create Order Please Login First !";
                return RedirectToAction("Index", "Home");
            }
            ConModel11 db = new ConModel11();
            var s = db.ShoppingCartItems.First(x => x.Id == id);
            s.Qty = s.Qty + 1;
            db.SaveChanges();
            return RedirectToAction("show");
        }

        public ActionResult RemoveAllCart() {
            if (Session["Username"] == null)
            {
                TempData["msg"] = "Please Login First !";
                return RedirectToAction("Index", "Home");
            }
            ConModel11 db = new ConModel11();
            var rows = from o in db.ShoppingCartItems
                       select o;
            foreach (var row in rows)
            {
                db.ShoppingCartItems.Remove(row);
            }
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
    }
}