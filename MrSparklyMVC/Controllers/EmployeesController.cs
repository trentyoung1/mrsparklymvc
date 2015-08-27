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
    public class EmployeesController : Controller
    {
        private MrSparklyEntities db = new MrSparklyEntities();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        //
        // GET: /Employees/

        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Suburb);
            return View(employees.ToList());
        }

        //
        // GET: /Employees/Details/5

        public ActionResult Details(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                logger.Error("Invalid ID (id={0})", id);
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // GET: /Employees/Create

        public ActionResult Create()
        {
            ViewBag.suburbID = new SelectList(db.Suburbs, "suburbID", "suburb1");
            return View();
        }

        //
        // POST: /Employees/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.suburbID = new SelectList(db.Suburbs, "suburbID", "suburb1", employee.suburbID);
            return View(employee);
        }

        //
        // GET: /Employees/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                logger.Error("Invalid ID (id={0})", id);
                return HttpNotFound();
            }
            ViewBag.suburbID = new SelectList(db.Suburbs, "suburbID", "suburb1", employee.suburbID);
            return View(employee);
        }

        //
        // POST: /Employees/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.suburbID = new SelectList(db.Suburbs, "suburbID", "suburb1", employee.suburbID);
            return View(employee);
        }

        //
        // GET: /Employees/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                logger.Error("Invalid ID (id={0})", id);
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // POST: /Employees/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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