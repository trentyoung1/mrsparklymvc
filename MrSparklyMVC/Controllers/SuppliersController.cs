using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MrSparklyMVC.Models;
using NLog;

namespace MrSparklyMVC.Controllers
{
    public class SuppliersController : Controller
    {
        private MrSparklyEntities db = new MrSparklyEntities();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        //
        // GET: /Suppliers/

        public ActionResult Index()
        {
            var suppliers = db.Suppliers.Include(s => s.Suburb);
            return View(suppliers.ToList());
        }

        //
        // GET: /Suppliers/Details/5

        public ActionResult Details(int id = 0)
        {
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        //
        // GET: /Suppliers/Create

        public ActionResult Create()
        {
            ViewBag.suburbID = new SelectList(db.Suburbs, "suburbID", "suburb1");
            return View();
        }

        //
        // POST: /Suppliers/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(supplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.suburbID = new SelectList(db.Suburbs, "suburbID", "suburb1", supplier.suburbID);
            return View(supplier);
        }

        //
        // GET: /Suppliers/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            ViewBag.suburbID = new SelectList(db.Suburbs, "suburbID", "suburb1", supplier.suburbID);
            return View(supplier);
        }

        //
        // POST: /Suppliers/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.suburbID = new SelectList(db.Suburbs, "suburbID", "suburb1", supplier.suburbID);
            return View(supplier);
        }

        //
        // GET: /Suppliers/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        //
        // POST: /Suppliers/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            db.Suppliers.Remove(supplier);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}