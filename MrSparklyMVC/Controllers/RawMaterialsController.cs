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
    [Authorize(Roles = "Purchasing, Admin")]
    public class RawMaterialsController : Controller
    {
        private MrSparklyEntities db = new MrSparklyEntities();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        //
        // GET: /RawMaterials/
        [MVCUrl("/RawMaterials")]
        public ActionResult Index()
        {
            return View(db.RawMaterials.ToList());
        }

        //
        // GET: /RawMaterials/Details/5

        [MVCUrl("/RawMaterials/Details")]
        public ActionResult Details(int id = 0)
        {
            RawMaterial rawmaterial = db.RawMaterials.Find(id);
            if (rawmaterial == null)
            {
                return HttpNotFound();
            }
            return View(rawmaterial);
        }

        //
        // GET: /RawMaterials/GetRawMaterialPrice/5

        public JsonResult GetRawMaterialPrice(int id = 0)
        {
            RawMaterial rawMat = db.RawMaterials.Find(id);

            if (!(rawMat == null))
            {
                return Json(rawMat.rawMaterialsPrice, JsonRequestBehavior.AllowGet);
            }
            else
            {
                logger.Error("Invalid Material ID (id={0})", id);
                return null;
            }
        }

        //
        // GET: /RawMaterials/Create
        [MVCUrl("/RawMaterials/Create")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /RawMaterials/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RawMaterial rawmaterial)
        {
            if (ModelState.IsValid)
            {
                db.RawMaterials.Add(rawmaterial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rawmaterial);
        }

        //
        // GET: /RawMaterials/Edit/5

        [MVCUrl("/RawMaterials/Edit")]
        public ActionResult Edit(int id = 0)
        {
            RawMaterial rawmaterial = db.RawMaterials.Find(id);
            if (rawmaterial == null)
            {
                return HttpNotFound();
            }
            return View(rawmaterial);
        }

        //
        // POST: /RawMaterials/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RawMaterial rawmaterial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rawmaterial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rawmaterial);
        }

        //
        // GET: /RawMaterials/Delete/5

        [MVCUrl("/RawMaterials/Delete")]
        public ActionResult Delete(int id = 0)
        {
            RawMaterial rawmaterial = db.RawMaterials.Find(id);
            if (rawmaterial == null)
            {
                return HttpNotFound();
            }
            return View(rawmaterial);
        }

        //
        // POST: /RawMaterials/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RawMaterial rawmaterial = db.RawMaterials.Find(id);
            db.RawMaterials.Remove(rawmaterial);
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