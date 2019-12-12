using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WineryShop.Core.Models;
namespace WineryShop.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Checkout() {
            if (Session["Username"] == null)
            {
                TempData["msg"] = "Please Login First !";
                return RedirectToAction("Index", "Home");
            }
            ConModel11 db = new ConModel11();
            var user = Session["Username"].ToString();
            var address = db.Orders.Where(x => x.UserId == user).ToList();
            if (address != null)
            {
               foreach(var i in address)
                {
                    ViewBag.firstName = i.FirstName;
                    ViewBag.lastName = i.LastName;
                    ViewBag.phoneNumber = i.PhoneNumber;
                    ViewBag.email = i.Email;
                    ViewBag.addressLine1 = i.AddressLine1;
                    ViewBag.addressLine2 = i.AddressLine2;
                    ViewBag.city = i.City;
                    ViewBag.zip = i.ZipCode;
                    ViewBag.state = i.State;
                    ViewBag.country = i.Country;

                }
            }

            return View();
        }
        //string FirstName,string LastName,string PhoneNumber,string Email,string AddressLine1, string AddressLine2, string City, string State,string Country,string ZipCode
        public ActionResult CheckoutComplete(Order order)
        {
            if (Session["Username"] == null)
            {
                TempData["msg"] = "To Create Order Please Login First !";
                return RedirectToAction("Index", "Home");
            }
            
            ConModel11 db = new ConModel11();
          
            Order o = new Order();
            o.FirstName = order.FirstName;
            o.LastName = order.LastName;
            o.PhoneNumber = order.PhoneNumber;
            o.Email = order.Email;
            o.City = order.City;
            o.Country = order.Country;
            o.AddressLine1 = order.AddressLine1;
            o.AddressLine2 = order.AddressLine2;
            o.OrderPlacedTime = System.DateTime.Now;
            o.State = order.State;
            o.ZipCode = order.ZipCode;
            o.UserId = Session["Username"].ToString();
            var mod = db.ShoppingCartItems.ToList();
            var total=0;
            OrderDetail od = new OrderDetail();
            foreach (ShoppingCartItem s in mod)
            {
                total += (s.price*s.Qty);
                od.WineName = s.WineName;
                od.Qty = s.Qty;
                od.Price = s.price;
                od.UserId = Session["Username"].ToString();
                od.OrderPlacedTime = System.DateTime.Now;
                db.OrderDetails.Add(od);
                db.SaveChanges();
            }
           
            o.OrderTotal = total;
            db.Orders.Add(o);
            string ses = Session["Username"].ToString();
            var model = db.ShoppingCartItems.Where(x=>x.UserId.Equals(ses) );
            foreach (ShoppingCartItem s in model)
            {
                db.ShoppingCartItems.Remove(s);
            }

            db.SaveChanges();
            return View();
        }

        public ActionResult ShowMyOrder() {
            if (Session["Username"] == null)
            {
                TempData["msg"] = " Please Login First !";
                return RedirectToAction("Index", "Home");
            }
            ConModel11 db = new ConModel11();
            string ses = Session["Username"].ToString();
            List<OrderDetail> model = db.OrderDetails.Where(x=>x.UserId.Equals(ses)).ToList();
            return View(model);
        }


    }
}