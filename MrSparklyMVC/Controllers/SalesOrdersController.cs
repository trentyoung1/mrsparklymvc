using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MrSparklyMVC.Models;
using NLog;
using PagedList;

namespace MrSparklyMVC.Controllers
{
    [Authorize(Roles = "Sales, Admin")]
    public class SalesOrdersController : Controller
    {
        private MrSparklyEntities db = new MrSparklyEntities();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        //
        // GET: /SalesOrders/

        public ActionResult Index(int? page)
        {
            var salesorders = db.SalesOrders.Include(s => s.Retailer);
            int pageSize = 5;
            salesorders = salesorders.OrderBy(s => s.salesOrderID);
            int pageNumber = (page ?? 1);

            return View(salesorders.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /SalesOrders/Details/5

        public ActionResult Details(int id = 0)
        {
            SalesOrder salesorder = db.SalesOrders.Find(id);
            if (salesorder == null)
            {
                return HttpNotFound();
            }
            return View(salesorder);
        }

        //
        // GET: /SalesOrders/Create

        public ActionResult Create()
        {
            ViewBag.retailerID = new SelectList(db.Retailers, "retailerID", "retailerName");
            return View();
        }

        //
        // POST: /SalesOrders/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SalesOrder salesorder)
        {
            if (ModelState.IsValid)
            {
                db.SalesOrders.Add(salesorder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.retailerID = new SelectList(db.Retailers, "retailerID", "retailerName", salesorder.retailerID);
            return View(salesorder);
        }

        //
        // GET: /SalesOrders/Edit/5

        public ActionResult Edit(int id = 0)
        {

            SalesOrder salesorder = db.SalesOrders.Find(id);
            if (salesorder == null)
            {
                return HttpNotFound();
            }
            ViewBag.retailerID = new SelectList(db.Retailers, "retailerID", "retailerName", salesorder.retailerID);
            return View(salesorder);
        }

        //
        // POST: /SalesOrders/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SalesOrder salesorder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesorder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.retailerID = new SelectList(db.Retailers, "retailerID", "retailerName", salesorder.retailerID);
            return View(salesorder);
        }

        //
        // GET: /SalesOrders/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SalesOrder salesorder = db.SalesOrders.Find(id);
            if (salesorder == null)
            {
                return HttpNotFound();
            }
            return View(salesorder);
        }

        //
        // POST: /SalesOrders/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesOrder salesorder = db.SalesOrders.Find(id);
            db.SalesOrders.Remove(salesorder);
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