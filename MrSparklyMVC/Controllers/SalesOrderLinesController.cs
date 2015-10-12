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
    [Authorize(Roles = "Sales, Admin")]
    public class SalesOrderLinesController : Controller
    {
        private MrSparklyEntities db = new MrSparklyEntities();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        //
        // GET: /SalesOrderLines/
        [MVCUrl("/SalesOrderLines")]
        public ActionResult Index()
        {
            var salesorderlines = db.SalesOrderLines.Include(s => s.Product).Include(s => s.SalesOrder);
            return View(salesorderlines.ToList());
        }

        //
        // GET: /SalesOrderLines/Details/5
        [MVCUrl("/SalesOrderLines/Details")]
        public ActionResult Details(int id = 0)
        {
            SalesOrderLine salesorderline = db.SalesOrderLines.Find(id);
            if (salesorderline == null)
            {
                return HttpNotFound();
            }
            return View(salesorderline);
        }

        //
        // GET: /SalesOrderLines/Create
        [MVCUrl("/SalesOrderLines/Create")]
        public ActionResult Create(int id = 0)
        {
            ViewBag.productID = new SelectList(db.Products, "productID", "productBrandName");
            ViewBag.salesOrderID = new SelectList(db.SalesOrders, "salesOrderID", "salesOrderNo");
            SalesOrderLine orderLine = new SalesOrderLine();
            orderLine.salesOrderID = id;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_SalesOrderLinesCreate", orderLine);
            }
            
            return View();
        }

        //
        // POST: /SalesOrderLines/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SalesOrderLine orderline)
        {
            if (ModelState.IsValid)
            {
                db.SalesOrderLines.Add(orderline);
                db.SaveChanges();
                return RedirectToAction("Details", "SalesOrders", new { id = orderline.salesOrderID });
            }

            ViewBag.productID = new SelectList(db.Products, "productID", "productBrandName", orderline.productID);
            ViewBag.salesOrderID = new SelectList(db.SalesOrders, "salesOrderID", "salesOrderNo", orderline.salesOrderID);
            return View(orderline);
        }

        //
        // GET: /SalesOrderLines/Edit/5
        [MVCUrl("/SalesOrderLines/Edit")]
        public ActionResult Edit(int id = 0)
        {
            SalesOrderLine salesorderline = db.SalesOrderLines.Find(id);
            
            if (salesorderline == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_SalesOrderLinesEdit", salesorderline);
            }
            else
            {
                ViewBag.productID = new SelectList(db.Products, "productID", "productBrandName", salesorderline.productID);
                ViewBag.salesOrderID = new SelectList(db.SalesOrders, "salesOrderID", "salesOrderNo", salesorderline.salesOrderID);
                return View(salesorderline);
            }
        }

        public ActionResult EditLine(int id = 0)
        {
            SalesOrderLine salesorderline = db.SalesOrderLines.Find(id);
            ViewBag.productID = new SelectList(db.Products, "productID", "productBrandName", salesorderline.productID);
            ViewBag.salesOrderID = new SelectList(db.SalesOrders, "salesOrderID", "salesOrderNo", salesorderline.salesOrderID);
            //ViewBag.salesOrderID = salesorderline.salesOrderID;

            return PartialView("_SalesOrderLinesEdit", salesorderline);
        }

        //
        // POST: /SalesOrderLines/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SalesOrderLine salesorderline)
        {

            if (ModelState.IsValid)
            {
                db.Entry(salesorderline).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "SalesOrders", new { id = salesorderline.salesOrderID });
            }
            ViewBag.productID = new SelectList(db.Products, "productID", "productBrandName", salesorderline.productID);
            ViewBag.salesOrderID = new SelectList(db.SalesOrders, "salesOrderID", "salesOrderNo", salesorderline.salesOrderID);
            return View(salesorderline);
        }

        //
        // GET: /SalesOrderLines/Delete/5
        [MVCUrl("/SalesOrderLines/Delete")]
        public ActionResult Delete(int id = 0)
        {
            SalesOrderLine salesorderline = db.SalesOrderLines.Find(id);
            if (salesorderline == null)
            {
                return HttpNotFound();
            }
            return View(salesorderline);
        }

        //
        // POST: /SalesOrderLines/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesOrderLine salesorderline = db.SalesOrderLines.Find(id);
            var orderID = new { id = salesorderline.salesOrderID };
            db.SalesOrderLines.Remove(salesorderline);
            db.SaveChanges();
            return RedirectToAction("Details", "SalesOrders", orderID);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}