using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MrSparklyMVC.Models;
using NLog;
using System.IO;
using System.Diagnostics;

namespace MrSparklyMVC.Controllers
{
    [Authorize(Roles = "Sales, Admin")]
    public class ProductController : Controller
    {
        private MrSparklyEntities db = new MrSparklyEntities();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                logger.Error("Invalid Product ID (id={0})", id);
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // GET: /Product/GetProductPrice/5

        public JsonResult GetProductPrice(int id = 0)
        {
            Product product = db.Products.Find(id);

            if (!(product == null))
            {
                return Json(product.productRetailPrice, JsonRequestBehavior.AllowGet);
            }
            else
            {
                logger.Error("Invalid Product ID (id={0})", id);
                return null;
            }
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        //
        // POST: /Product/CreateFromFile

        [HttpPost]
        public ActionResult CreateFromFile()
        {
            HttpPostedFileBase file = Request.Files[0];

            // if the file is not the products.csv file, warn that its not the demo file
            Debug.Assert(file.FileName == "products.csv", "Not using the demo file");

            //ensure file is not empty
            if (file != null && file.ContentLength > 0)
            {

                Debug.Print("Reading in the contents of the file");

                //ensure file is csv
                if (file.ContentType == "text/csv" || file.ContentType == "application/vnd.ms-excel")
                {
                    //read in the first line of the file
                    StreamReader sr = new StreamReader(file.InputStream);
                    string productLine = sr.ReadLine();
                    int count = 0;
                    //read in each line of the file
                    while (productLine != null)
                    {
                        //populate a new product with values
                        string[] pArray = productLine.Split(',');
                        Product newProd = new Product();
                        newProd.productBrandName = pArray[0];
                        newProd.productCostPrice = decimal.Parse(pArray[1]);
                        newProd.productRetailPrice = decimal.Parse(pArray[2]);
                        newProd.productQty = short.Parse(pArray[3]);

                        //add the product to the db
                        db.Products.Add(newProd);
                        db.SaveChanges();
                        count++;
                        productLine = sr.ReadLine();
                    }
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                logger.Error("Invalid Product ID (id={0})", id);
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                logger.Error("Invalid Product ID (id={0})", id);
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Product/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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