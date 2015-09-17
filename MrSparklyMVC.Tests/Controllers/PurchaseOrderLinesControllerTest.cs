using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MrSparklyMVC;
using MrSparklyMVC.Controllers;
using MrSparklyMVC.Models;
using Moq;

namespace MrSparklyMVC.Tests.Controllers
{
    [TestClass]
    public class PurchaseOrderLinesControllerTest
    {
        [TestMethod]
        public void PurchaseOrderLinesController_Index()
        {
            PurchaseOrderLinesController controller = new PurchaseOrderLinesController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void PurchaseOrderLinesController_Details_isValid()
        {
            PurchaseOrderLinesController controller = new PurchaseOrderLinesController();

            ViewResult result = controller.Details(1) as ViewResult;
            PurchaseOrderLine PurchaseOrderLineResult = (PurchaseOrderLine)result.Model;

            Assert.AreEqual(1, PurchaseOrderLineResult.purchaseOrderLinesID);
        }

        [TestMethod]
        public void PurchaseOrderLinesController_Details_isNotValid()
        {
            PurchaseOrderLinesController controller = new PurchaseOrderLinesController();

            HttpNotFoundResult result = controller.Details(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }

        [TestMethod]
        public void PurchaseOrderLinesController_Create_isValid()
        {
            PurchaseOrderLine testPurchaseOrderLine = new PurchaseOrderLine();
            testPurchaseOrderLine.purchaseOrderID = 1;
            testPurchaseOrderLine.purchaseOrderItemPrice = 1;
            testPurchaseOrderLine.rawMaterialsID = 1;
            testPurchaseOrderLine.purchaseOrderItemQty = 1;
            testPurchaseOrderLine.purchaseOrderLineSubtotal = 1;

            PurchaseOrderLinesController controller = new PurchaseOrderLinesController();

            var result = (RedirectToRouteResult)controller.Create(testPurchaseOrderLine);

            Assert.AreEqual("Details", result.RouteValues["action"]);
        }

        [TestMethod]
        public void PurchaseOrderLinesController_Create_isNotValid()
        {
            PurchaseOrderLine testPurchaseOrderLine = new PurchaseOrderLine();
            testPurchaseOrderLine.purchaseOrderItemQty = 99;
            PurchaseOrderLinesController controller = new PurchaseOrderLinesController();
            controller.ModelState.AddModelError("", "error message");

            var result = controller.Create(testPurchaseOrderLine) as ViewResult;
            PurchaseOrderLine resultPurchaseOrderLines = (PurchaseOrderLine)result.Model;

            Assert.AreEqual((short)99, resultPurchaseOrderLines.purchaseOrderItemQty);
        }

        [TestMethod]
        public void PurchaseOrderLinesController_Edit_GET_isValid()
        {
            PurchaseOrderLinesController controller = new PurchaseOrderLinesController();
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers).Returns(new System.Net.WebHeaderCollection { { "X-Requested-With", "XMLHttpRequest" } });
            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            PartialViewResult result = controller.Edit(1) as PartialViewResult;
            PurchaseOrderLine PurchaseOrderLinesResult = (PurchaseOrderLine)result.Model;

            Assert.AreEqual(1, PurchaseOrderLinesResult.purchaseOrderLinesID);
        }

        [TestMethod]
        public void PurchaseOrderLinesController_Edit_GET_isNotValid()
        {
            PurchaseOrderLinesController controller = new PurchaseOrderLinesController();
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers).Returns(new System.Net.WebHeaderCollection { { "X-Requested-With", "XMLHttpRequest" } });
            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            HttpNotFoundResult result = controller.Edit(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }

        [TestMethod]
        public void PurchaseOrderLinesController_Delete_GET_isValid()
        {
            PurchaseOrderLinesController controller = new PurchaseOrderLinesController();

            ViewResult result = controller.Delete(1) as ViewResult;
            PurchaseOrderLine PurchaseOrderLinesResult = (PurchaseOrderLine)result.Model;

            Assert.AreEqual(1, PurchaseOrderLinesResult.purchaseOrderLinesID);
        }

        [TestMethod]
        public void PurchaseOrderLinesController_Delete_GET_isNotValid()
        {
            PurchaseOrderLinesController controller = new PurchaseOrderLinesController();

            HttpNotFoundResult result = controller.Delete(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }
    }
}
