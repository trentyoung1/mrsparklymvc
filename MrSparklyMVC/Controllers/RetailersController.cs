﻿using System;
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
    public class RetailersController : Controller
    {
        private MrSparklyEntities db = new MrSparklyEntities();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        //
        // GET: /Retailers/

        [MVCUrl("/Retailers")]
        public ActionResult Index()
        {
            var retailers = db.Retailers.Include(r => r.Employee).Include(r => r.Suburb);
            return View(retailers.ToList());
        }

        //
        // GET: /Retailers/Details/5

        [MVCUrl("/Retailers/Details")]
        public ActionResult Details(int id = 0)
        {
            Retailer retailer = db.Retailers.Find(id);
            if (retailer == null)
            {
                logger.Error("Invalid ID (id={0})", id);
                return HttpNotFound();
            }
            return View(retailer);
        }

        //
        // GET: /Retailers/Create

        [MVCUrl("/Retailers/Create")]
        public ActionResult Create()
        {
            ViewBag.employeeID = new SelectList(db.Employees, "employeeID", "employeeFirstName");
            ViewBag.suburbID = new SelectList(db.Suburbs, "suburbID", "suburb1");
            return View();
        }

        //
        // POST: /Retailers/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Retailer retailer)
        {
            if (ModelState.IsValid)
            {
                db.Retailers.Add(retailer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.employeeID = new SelectList(db.Employees, "employeeID", "employeeFirstName", retailer.employeeID);
            ViewBag.suburbID = new SelectList(db.Suburbs, "suburbID", "suburb1", retailer.suburbID);
            return View(retailer);
        }

        //
        // GET: /Retailers/Edit/5

        [MVCUrl("/Retailers/Edit")]
        public ActionResult Edit(int id = 0)
        {
            Retailer retailer = db.Retailers.Find(id);
            if (retailer == null)
            {
                logger.Error("Invalid ID (id={0})", id);
                return HttpNotFound();
            }
            ViewBag.employeeID = new SelectList(db.Employees, "employeeID", "employeeFirstName", retailer.employeeID);
            ViewBag.suburbID = new SelectList(db.Suburbs, "suburbID", "suburb1", retailer.suburbID);
            return View(retailer);
        }

        //
        // POST: /Retailers/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Retailer retailer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(retailer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.employeeID = new SelectList(db.Employees, "employeeID", "employeeFirstName", retailer.employeeID);
            ViewBag.suburbID = new SelectList(db.Suburbs, "suburbID", "suburb1", retailer.suburbID);
            return View(retailer);
        }

        //
        // GET: /Retailers/Delete/5

        [MVCUrl("/Retailers/Delete")]
        public ActionResult Delete(int id = 0)
        {
            Retailer retailer = db.Retailers.Find(id);
            if (retailer == null)
            {
                logger.Error("Invalid ID (id={0})", id);
                return HttpNotFound();
            }
            return View(retailer);
        }

        //
        // POST: /Retailers/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Retailer retailer = db.Retailers.Find(id);
            db.Retailers.Remove(retailer);
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