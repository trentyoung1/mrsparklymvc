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
    [Authorize(Roles = "Purchasing, Admin")]
    public class PurchaseOrdersController : Controller
    {
        private MrSparklyEntities db = new MrSparklyEntities();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        //
        // GET: /PurchaseOrders/

        public ActionResult Index()
        {
            var purchaseorders = db.PurchaseOrders.Include(p => p.Supplier);
            return View(purchaseorders.ToList());
        }

        //
        // GET: /PurchaseOrders/Details/5

        public ActionResult Details(int id = 0)
        {
            PurchaseOrder purchaseorder = db.PurchaseOrders.Find(id);
            if (purchaseorder == null)
            {
                logger.Error("Invalid ID (id={0})", id);
                return HttpNotFound();
            }

            decimal orderTotal = 0;

            foreach (var orderLine in purchaseorder.PurchaseOrderLines)
            {
                orderTotal += (decimal)orderLine.purchaseOrderLineSubtotal;
            }

            ViewBag.orderTotal = orderTotal;

            return View(purchaseorder);
        }

        //
        // GET: /PurchaseOrders/Create

        public ActionResult Create()
        {
            ViewBag.supplierID = new SelectList(db.Suppliers, "supplierID", "supplierName");
            return View();
        }

        //
        // POST: /PurchaseOrders/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PurchaseOrder purchaseorder)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseOrders.Add(purchaseorder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.supplierID = new SelectList(db.Suppliers, "supplierID", "supplierName", purchaseorder.supplierID);
            return View(purchaseorder);
        }

        //
        // GET: /PurchaseOrders/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PurchaseOrder purchaseorder = db.PurchaseOrders.Find(id);
            if (purchaseorder == null)
            {
                logger.Error("Invalid ID (id={0})", id);
                return HttpNotFound();
            }
            ViewBag.supplierID = new SelectList(db.Suppliers, "supplierID", "supplierName", purchaseorder.supplierID);
            return View(purchaseorder);
        }

        //
        // POST: /PurchaseOrders/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PurchaseOrder purchaseorder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseorder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.supplierID = new SelectList(db.Suppliers, "supplierID", "supplierName", purchaseorder.supplierID);
            return View(purchaseorder);
        }

        //
        // GET: /PurchaseOrders/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PurchaseOrder purchaseorder = db.PurchaseOrders.Find(id);
            if (purchaseorder == null)
            {
                logger.Error("Invalid ID (id={0})", id);
                return HttpNotFound();
            }
            return View(purchaseorder);
        }

        //
        // POST: /PurchaseOrders/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseOrder purchaseorder = db.PurchaseOrders.Find(id);
            db.PurchaseOrders.Remove(purchaseorder);
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