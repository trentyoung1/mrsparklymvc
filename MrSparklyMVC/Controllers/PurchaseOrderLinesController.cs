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
    public class PurchaseOrderLinesController : Controller
    {
        private MrSparklyEntities db = new MrSparklyEntities();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        //
        // GET: /PurchaseOrderLines/

        public ActionResult Index()
        {
            var purchaseorderlines = db.PurchaseOrderLines.Include(p => p.PurchaseOrder).Include(p => p.RawMaterial);
            return View(purchaseorderlines.ToList());
        }

        //
        // GET: /PurchaseOrderLines/Details/5

        public ActionResult Details(int id = 0)
        {
            PurchaseOrderLine purchaseorderline = db.PurchaseOrderLines.Find(id);
            if (purchaseorderline == null)
            {
                return HttpNotFound();
            }
            return View(purchaseorderline);
        }

        //
        // GET: /PurchaseOrderLines/Create

        public ActionResult Create(int id = 0)
        {
            ViewBag.purchaseOrderID = new SelectList(db.PurchaseOrders, "purchaseOrderID", "purchaseOrderNo");
            ViewBag.rawMaterialsID = new SelectList(db.RawMaterials, "rawMaterialsID", "rawMaterialsName");
            PurchaseOrderLine orderLine = new PurchaseOrderLine();
            orderLine.purchaseOrderID = id;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PurchaseOrderLinesCreate", orderLine);
            }

            return View();
        }

        //
        // POST: /PurchaseOrderLines/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PurchaseOrderLine purchaseorderline)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseOrderLines.Add(purchaseorderline);
                db.SaveChanges();
                return RedirectToAction("Details", "PurchaseOrders", new { id = purchaseorderline.purchaseOrderID });
            }

            ViewBag.purchaseOrderID = new SelectList(db.PurchaseOrders, "purchaseOrderID", "purchaseOrderNo", purchaseorderline.purchaseOrderID);
            ViewBag.rawMaterialsID = new SelectList(db.RawMaterials, "rawMaterialsID", "rawMaterialsName", purchaseorderline.rawMaterialsID);
            return View(purchaseorderline);
        }

        //
        // GET: /PurchaseOrderLines/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PurchaseOrderLine purchaseorderline = db.PurchaseOrderLines.Find(id);
            ViewBag.purchaseOrderID = new SelectList(db.PurchaseOrders, "purchaseOrderID", "purchaseOrderNo", purchaseorderline.purchaseOrderID);
            ViewBag.rawMaterialsID = new SelectList(db.RawMaterials, "rawMaterialsID", "rawMaterialsName", purchaseorderline.rawMaterialsID);
            if (purchaseorderline == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PurchaseOrderLinesEdit", purchaseorderline);
            }
            else
            {
                ViewBag.purchaseOrderID = new SelectList(db.PurchaseOrders, "purchaseOrderID", "purchaseOrderNo", purchaseorderline.purchaseOrderID);
                ViewBag.rawMaterialsID = new SelectList(db.RawMaterials, "rawMaterialsID", "rawMaterialsName", purchaseorderline.rawMaterialsID);
                return View(purchaseorderline);
            }
        }

        //
        // POST: /PurchaseOrderLines/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PurchaseOrderLine purchaseorderline)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseorderline).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "PurchaseOrders", new { id = purchaseorderline.purchaseOrderID });
            }
            ViewBag.purchaseOrderID = new SelectList(db.PurchaseOrders, "purchaseOrderID", "purchaseOrderNo", purchaseorderline.purchaseOrderID);
            ViewBag.rawMaterialsID = new SelectList(db.RawMaterials, "rawMaterialsID", "rawMaterialsName", purchaseorderline.rawMaterialsID);
            return PartialView("_PurchaseOrderLinesEdit", purchaseorderline);
        }

        //
        // GET: /PurchaseOrderLines/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PurchaseOrderLine purchaseorderline = db.PurchaseOrderLines.Find(id);
            if (purchaseorderline == null)
            {
                return HttpNotFound();
            }
            return View(purchaseorderline);
        }

        //
        // POST: /PurchaseOrderLines/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseOrderLine purchaseorderline = db.PurchaseOrderLines.Find(id);
            db.PurchaseOrderLines.Remove(purchaseorderline);
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