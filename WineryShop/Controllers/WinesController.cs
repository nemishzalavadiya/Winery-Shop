using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WineryShop.Core.Models;
namespace WineryShop.Controllers
{
    public class WinesController : Controller
    {
        private ConModel11 db = new ConModel11();

        // GET: Wines
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                TempData["msg"] = " Please Login First !";
                return RedirectToAction("Index", "Home");
            }
            var wines = db.Wines.Include(w => w.Category);
            return View(wines.ToList());
        }

        // GET: Wines/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Admin"] == null)
            {
                TempData["msg"] = " Please Login First !";
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wine wine = db.Wines.Find(id);
            if (wine == null)
            {
                return HttpNotFound();
            }
            return View(wine);
        }

        // GET: Wines/Create
        public ActionResult Create()
        {

            if (Session["Admin"] == null)
            {
                TempData["msg"] = "Please Login First !";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Wines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Wine wine)
        {
            string fileName = Path.GetFileNameWithoutExtension(wine.ImageFile.FileName);
            string extension = Path.GetExtension(wine.ImageFile.FileName);

            if (ModelState.IsValid)
            {
               
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                wine.ImageUrl = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                wine.ImageFile.SaveAs(fileName);
                db.Wines.Add(wine);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", wine.CategoryId);
            return View(wine);
        }

        // GET: Wines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Admin"] == null)
            {
                TempData["msg"] = " Please Login First !";
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wine wine = db.Wines.Find(id);
            if (wine == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", wine.CategoryId);
            return View(wine);
        }

        // POST: Wines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Wine wine)
        {
            string fileName = Path.GetFileNameWithoutExtension(wine.ImageFile.FileName);
            string extension = Path.GetExtension(wine.ImageFile.FileName);

          
            if (ModelState.IsValid)
            {
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                wine.ImageUrl = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                wine.ImageFile.SaveAs(fileName);
                db.Entry(wine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", wine.CategoryId);
            return View(wine);
        }

        // GET: Wines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Admin"] == null)
            {
                TempData["msg"] = " Please Login First !";
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wine wine = db.Wines.Find(id);
            if (wine == null)
            {
                return HttpNotFound();
            }
            return View(wine);
        }

        // POST: Wines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wine wine = db.Wines.Find(id);
            db.Wines.Remove(wine);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
