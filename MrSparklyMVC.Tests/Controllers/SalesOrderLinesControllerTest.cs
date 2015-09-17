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
    public class SalesOrderLinesControllerTest
    {
        [TestMethod]
        public void SalesOrderLinesController_Index()
        {
            SalesOrderLinesController controller = new SalesOrderLinesController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void SalesOrderLinesController_Details_isValid()
        {
            SalesOrderLinesController controller = new SalesOrderLinesController();

            ViewResult result = controller.Details(2) as ViewResult;
            SalesOrderLine SalesOrderLineResult = (SalesOrderLine)result.Model;

            Assert.AreEqual(2, SalesOrderLineResult.salesOrderLinesID);
        }

        [TestMethod]
        public void SalesOrderLinesController_Details_isNotValid()
        {
            SalesOrderLinesController controller = new SalesOrderLinesController();

            HttpNotFoundResult result = controller.Details(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }

        [TestMethod]
        public void SalesOrderLinesController_Create_isValid()
        {
            SalesOrderLine testSalesOrderLine = new SalesOrderLine();
            testSalesOrderLine.salesOrderID = 1;
            testSalesOrderLine.salesOrderItemPrice = 1;
            testSalesOrderLine.productID = 1;
            testSalesOrderLine.salesOrderItemQty = 1;
            testSalesOrderLine.salesOrderLinesSubtotal = 1;

            SalesOrderLinesController controller = new SalesOrderLinesController();

            var result = (RedirectToRouteResult)controller.Create(testSalesOrderLine);

            Assert.AreEqual("Details", result.RouteValues["action"]);
        }

        [TestMethod]
        public void SalesOrderLinesController_Create_isNotValid()
        {
            SalesOrderLine testSalesOrderLine = new SalesOrderLine();
            testSalesOrderLine.salesOrderItemQty = 99;
            SalesOrderLinesController controller = new SalesOrderLinesController();
            controller.ModelState.AddModelError("", "error message");

            var result = controller.Create(testSalesOrderLine) as ViewResult;
            SalesOrderLine resultSalesOrderLines = (SalesOrderLine)result.Model;

            Assert.AreEqual((short)99, resultSalesOrderLines.salesOrderItemQty);
        }

        [TestMethod]
        public void SalesOrderLinesController_Edit_GET_isValid()
        {
            SalesOrderLinesController controller = new SalesOrderLinesController();
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers).Returns(new System.Net.WebHeaderCollection { { "X-Requested-With", "XMLHttpRequest" } });
            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            PartialViewResult result = controller.Edit(2) as PartialViewResult;
            SalesOrderLine SalesOrderLinesResult = (SalesOrderLine)result.Model;

            Assert.AreEqual(2, SalesOrderLinesResult.salesOrderLinesID);
        }

        [TestMethod]
        public void SalesOrderLinesController_Edit_GET_isNotValid()
        {
            SalesOrderLinesController controller = new SalesOrderLinesController();
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
        public void SalesOrderLinesController_Delete_GET_isValid()
        {
            SalesOrderLinesController controller = new SalesOrderLinesController();

            ViewResult result = controller.Delete(2) as ViewResult;
            SalesOrderLine SalesOrderLinesResult = (SalesOrderLine)result.Model;

            Assert.AreEqual(2, SalesOrderLinesResult.salesOrderLinesID);
        }

        [TestMethod]
        public void SalesOrderLinesController_Delete_GET_isNotValid()
        {
            SalesOrderLinesController controller = new SalesOrderLinesController();

            HttpNotFoundResult result = controller.Delete(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }
    }
}
