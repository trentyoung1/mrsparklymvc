using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MrSparklyMVC.Models;

namespace MrSparklyMVC.Controllers
{
    public class RawMaterialsController : Controller
    {
        private MrSparklyEntities db = new MrSparklyEntities();

        //
        // GET: /RawMaterials/

        public ActionResult Index()
        {
            return View(db.RawMaterials.ToList());
        }

        //
        // GET: /RawMaterials/Details/5

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
        // GET: /RawMaterials/Create

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