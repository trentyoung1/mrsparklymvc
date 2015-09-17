using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MrSparklyMVC;
using MrSparklyMVC.Controllers;
using MrSparklyMVC.Models;

namespace MrSparklyMVC.Tests.Controllers
{
    [TestClass]
    public class RetailersControllerTest
    {
        [TestMethod]
        public void RetailersController_Index()
        {
            RetailersController controller = new RetailersController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void RetailersController_Details_isValid()
        {
            RetailersController controller = new RetailersController();

            ViewResult result = controller.Details(1) as ViewResult;
            Retailer RetailerResult = (Retailer)result.Model;

            Assert.AreEqual(1, RetailerResult.retailerID);
        }

        [TestMethod]
        public void RetailersController_Details_isNotValid()
        {
            RetailersController controller = new RetailersController();

            HttpNotFoundResult result = controller.Details(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }

        [TestMethod]
        public void RetailersController_Create_isValid()
        {
            Retailer testRetailer = new Retailer();
            testRetailer.retailerName = "testRetailer";
            testRetailer.retailerEmail = "testRetailer@test.gov";
            testRetailer.retailerContactName = "testContact";

            RetailersController controller = new RetailersController();

            var result = (RedirectToRouteResult)controller.Create(testRetailer);

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void RetailersController_Create_isNotValid()
        {
            Retailer testRetailer = new Retailer();
            testRetailer.retailerName = "invalidRetailer";
            RetailersController controller = new RetailersController();
            controller.ModelState.AddModelError("", "error message");

            var result = controller.Create(testRetailer) as ViewResult;
            Retailer resultRetailers = (Retailer)result.Model;

            Assert.AreEqual("invalidRetailer", resultRetailers.retailerName);
        }

        [TestMethod]
        public void RetailersController_Edit_GET_isValid()
        {
            RetailersController controller = new RetailersController();

            ViewResult result = controller.Edit(1) as ViewResult;
            Retailer RetailersResult = (Retailer)result.Model;

            Assert.AreEqual(1, RetailersResult.retailerID);
        }

        [TestMethod]
        public void RetailersController_Edit_GET_isNotValid()
        {
            RetailersController controller = new RetailersController();

            HttpNotFoundResult result = controller.Edit(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }

        [TestMethod]
        public void RetailersController_Delete_GET_isValid()
        {
            RetailersController controller = new RetailersController();

            ViewResult result = controller.Delete(1) as ViewResult;
            Retailer RetailersResult = (Retailer)result.Model;

            Assert.AreEqual(1, RetailersResult.retailerID);
        }

        [TestMethod]
        public void RetailersController_Delete_GET_isNotValid()
        {
            RetailersController controller = new RetailersController();

            HttpNotFoundResult result = controller.Delete(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }
    }
}
