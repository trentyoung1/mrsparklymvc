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
    public class SalesOrdersControllerTest
    {
        [TestMethod]
        public void SalesOrdersController_Index()
        {
            SalesOrdersController controller = new SalesOrdersController();

            ViewResult result = controller.Index(1) as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void SalesOrdersController_Details_isValid()
        {
            SalesOrdersController controller = new SalesOrdersController();

            ViewResult result = controller.Details(1) as ViewResult;
            SalesOrder SalesOrderResult = (SalesOrder)result.Model;

            Assert.AreEqual(1, SalesOrderResult.salesOrderID);
        }

        [TestMethod]
        public void SalesOrdersController_Details_isNotValid()
        {
            SalesOrdersController controller = new SalesOrdersController();

            HttpNotFoundResult result = controller.Details(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }

        [TestMethod]
        public void SalesOrdersController_Create_isValid()
        {
            SalesOrder testSalesOrder = new SalesOrder();
            testSalesOrder.salesOrderNo = "9999999";
            testSalesOrder.salesOrderDate = DateTime.Now;

            SalesOrdersController controller = new SalesOrdersController();

            var result = (RedirectToRouteResult)controller.Create(testSalesOrder);

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void SalesOrdersController_Create_isNotValid()
        {
            SalesOrder testSalesOrder = new SalesOrder();
            testSalesOrder.salesOrderNo = "invalid";
            SalesOrdersController controller = new SalesOrdersController();
            controller.ModelState.AddModelError("", "error message");

            var result = controller.Create(testSalesOrder) as ViewResult;
            SalesOrder resultSalesOrders = (SalesOrder)result.Model;

            Assert.AreEqual("invalid", resultSalesOrders.salesOrderNo);
        }

        [TestMethod]
        public void SalesOrdersController_Edit_GET_isValid()
        {
            SalesOrdersController controller = new SalesOrdersController();

            ViewResult result = controller.Edit(1) as ViewResult;
            SalesOrder SalesOrdersResult = (SalesOrder)result.Model;

            Assert.AreEqual(1, SalesOrdersResult.salesOrderID);
        }

        [TestMethod]
        public void SalesOrdersController_Edit_GET_isNotValid()
        {
            SalesOrdersController controller = new SalesOrdersController();

            HttpNotFoundResult result = controller.Edit(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }

        [TestMethod]
        public void SalesOrdersController_Delete_GET_isValid()
        {
            SalesOrdersController controller = new SalesOrdersController();

            ViewResult result = controller.Delete(1) as ViewResult;
            SalesOrder SalesOrdersResult = (SalesOrder)result.Model;

            Assert.AreEqual(1, SalesOrdersResult.salesOrderID);
        }

        [TestMethod]
        public void SalesOrdersController_Delete_GET_isNotValid()
        {
            SalesOrdersController controller = new SalesOrdersController();

            HttpNotFoundResult result = controller.Delete(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }
    }
}
