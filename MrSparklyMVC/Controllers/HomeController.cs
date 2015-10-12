using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using System.Reflection;
using MrSparklyMVC.Models;
using System.Xml;
using System.Xml.Linq;

namespace MrSparklyMVC.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private MrSparklyEntities db = new MrSparklyEntities();

        [MVCUrl("/Index")]
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to the Mr. Sparkly ASP Web Application.";

            return View();
        }

        /*
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        */

        /// <summary>
        /// dynamically generates an xml sitemap using reflection
        /// </summary>
        /// <returns></returns>
        [MVCUrl("/Home/SiteMap")]
        public ActionResult SiteMap()
        {
            //a list to hold all the urls
            List<string> allPageUrls = new List<string>();

            //get all controllers
            var allControllers = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(Controller)));

            foreach (var controllerType in allControllers)
            {
                //get all public methods on this controller
                var allPublicMethodsOnController = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance);

                foreach (var publicMethod in allPublicMethodsOnController)
                {
                    //get the MvcUrlAttribute for this method
                    var mvcurlattr = publicMethod.GetCustomAttributes(true).OfType<MVCUrlAttribute>().FirstOrDefault();

                    if (mvcurlattr != null)
                    {
                        //add the url to the list
                        allPageUrls.Add(mvcurlattr.Url);

                        //holds the ids for item urls
                        List<int> ids = new List<int>();

                        //get the ids to be appended to the "Details", "Edit" and "Delete" urls
                        if (publicMethod.Name == "Details" || publicMethod.Name == "Edit" || publicMethod.Name == "Delete")
                        {
                            switch (controllerType.Name)
                            {
                                case "EmployeesController":
                                    var employees = db.Employees.ToList();

                                    foreach (var employee in employees)
                                    {
                                        ids.Add(employee.employeeID);
                                    }
                                    break;
                                case "ProductController":
                                    var products = db.Products.ToList();

                                    foreach (var product in products)
                                    {
                                        ids.Add(product.productID);
                                    }
                                    break;
                                case "PurchaseOrdersController":
                                    var purchaseOrders = db.PurchaseOrders.ToList();

                                    foreach (var order in purchaseOrders)
                                    {
                                        ids.Add(order.purchaseOrderID);
                                    }
                                    break;
                                case "PurchaseOrderLinesController":
                                    var orderLines = db.PurchaseOrderLines.ToList();

                                    foreach (var orderline in orderLines)
                                    {
                                        ids.Add(orderline.purchaseOrderLinesID);
                                    }
                                    break;
                                case "RawMaterialsController":
                                    var rawMaterials = db.RawMaterials.ToList();

                                    foreach (var material in rawMaterials)
                                    {
                                        ids.Add(material.rawMaterialsID);
                                    }
                                    break;
                                case "RetailersController":
                                    var retailers = db.Retailers.ToList();

                                    foreach (var retailer in retailers)
                                    {
                                        ids.Add(retailer.retailerID);
                                    }
                                    break;
                                case "SalesOrdersController":
                                    var salesOrders = db.SalesOrders.ToList();

                                    foreach (var order in salesOrders)
                                    {
                                        ids.Add(order.salesOrderID);
                                    }
                                    break;
                                case "SalesOrderLinesController":
                                    var salesOrderLines = db.SalesOrderLines.ToList();

                                    foreach (var orderline in salesOrderLines)
                                    {
                                        ids.Add(orderline.salesOrderLinesID);
                                    }
                                    break;
                                case "SuppliersController":
                                    var suppliers = db.Suppliers.ToList();

                                    foreach (var supplier in suppliers)
                                    {
                                        ids.Add(supplier.supplierID);
                                    }
                                    break;
                            }

                            //create a url for each of the ids
                            foreach (var id in ids)
                            {
                                var idUrl = mvcurlattr.Url + "/" + id.ToString();
                                allPageUrls.Add(idUrl);
                            }
                        }
                        
                    }
                }

            }
            
            //create the xml sitemap
            SiteMapCreator creator = new SiteMapCreator();
            string fqdn = HttpContext.Request.Url.GetLeftPart(UriPartial.Authority);
            XDocument siteMap = creator.createSiteMap(allPageUrls, fqdn);

            //return the site map as an xml document
            return Content("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + siteMap.ToString(), "text/xml");
        }
    }
}
