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
    public class PurchaseOrdersControllerTest
    {
        [TestMethod]
        public void PurchaseOrdersController_Index()
        {
            PurchaseOrdersController controller = new PurchaseOrdersController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void PurchaseOrdersController_Details_isValid()
        {
            PurchaseOrdersController controller = new PurchaseOrdersController();

            ViewResult result = controller.Details(1) as ViewResult;
            PurchaseOrder PurchaseOrderResult = (PurchaseOrder)result.Model;

            Assert.AreEqual(1, PurchaseOrderResult.purchaseOrderID);
        }

        [TestMethod]
        public void PurchaseOrdersController_Details_isNotValid()
        {
            PurchaseOrdersController controller = new PurchaseOrdersController();

            HttpNotFoundResult result = controller.Details(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }

        [TestMethod]
        public void PurchaseOrdersController_Create_isValid()
        {
            PurchaseOrder testPurchaseOrder = new PurchaseOrder();
            testPurchaseOrder.purchaseOrderNo = "9999999";
            testPurchaseOrder.purchaseOrderDate = DateTime.Now;
            testPurchaseOrder.deliveryAddress = "123 test street";

            PurchaseOrdersController controller = new PurchaseOrdersController();

            var result = (RedirectToRouteResult)controller.Create(testPurchaseOrder);

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void PurchaseOrdersController_Create_isNotValid()
        {
            PurchaseOrder testPurchaseOrder = new PurchaseOrder();
            testPurchaseOrder.purchaseOrderNo = "invalid";
            PurchaseOrdersController controller = new PurchaseOrdersController();
            controller.ModelState.AddModelError("", "error message");

            var result = controller.Create(testPurchaseOrder) as ViewResult;
            PurchaseOrder resultPurchaseOrders = (PurchaseOrder)result.Model;

            Assert.AreEqual("invalid", resultPurchaseOrders.purchaseOrderNo);
        }

        [TestMethod]
        public void PurchaseOrdersController_Edit_GET_isValid()
        {
            PurchaseOrdersController controller = new PurchaseOrdersController();

            ViewResult result = controller.Edit(1) as ViewResult;
            PurchaseOrder PurchaseOrdersResult = (PurchaseOrder)result.Model;

            Assert.AreEqual(1, PurchaseOrdersResult.purchaseOrderID);
        }

        [TestMethod]
        public void PurchaseOrdersController_Edit_GET_isNotValid()
        {
            PurchaseOrdersController controller = new PurchaseOrdersController();

            HttpNotFoundResult result = controller.Edit(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }

        [TestMethod]
        public void PurchaseOrdersController_Delete_GET_isValid()
        {
            PurchaseOrdersController controller = new PurchaseOrdersController();

            ViewResult result = controller.Delete(1) as ViewResult;
            PurchaseOrder PurchaseOrdersResult = (PurchaseOrder)result.Model;

            Assert.AreEqual(1, PurchaseOrdersResult.purchaseOrderID);
        }

        [TestMethod]
        public void PurchaseOrdersController_Delete_GET_isNotValid()
        {
            PurchaseOrdersController controller = new PurchaseOrdersController();

            HttpNotFoundResult result = controller.Delete(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }
    }
}
