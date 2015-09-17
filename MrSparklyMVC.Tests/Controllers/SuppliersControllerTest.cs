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
    public class SuppliersControllerTest
    {
        [TestMethod]
        public void SuppliersController_Index()
        {
            SuppliersController controller = new SuppliersController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void SuppliersController_Details_isValid()
        {
            SuppliersController controller = new SuppliersController();

            ViewResult result = controller.Details(1) as ViewResult;
            Supplier SupplierResult = (Supplier)result.Model;

            Assert.AreEqual(1, SupplierResult.supplierID);
        }

        [TestMethod]
        public void SuppliersController_Details_isNotValid()
        {
            SuppliersController controller = new SuppliersController();

            HttpNotFoundResult result = controller.Details(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }

        [TestMethod]
        public void SuppliersController_Create_isValid()
        {
            Supplier testSupplier = new Supplier();
            testSupplier.supplierName = "testSupplier";
            testSupplier.supplierEmail = "testSupplier@test.gov";
            testSupplier.contactName = "testContact";

            SuppliersController controller = new SuppliersController();

            var result = (RedirectToRouteResult)controller.Create(testSupplier);

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void SuppliersController_Create_isNotValid()
        {
            Supplier testSupplier = new Supplier();
            testSupplier.supplierName = "invalidSupplier";
            SuppliersController controller = new SuppliersController();
            controller.ModelState.AddModelError("", "error message");

            var result = controller.Create(testSupplier) as ViewResult;
            Supplier resultSuppliers = (Supplier)result.Model;

            Assert.AreEqual("invalidSupplier", resultSuppliers.supplierName);
        }

        [TestMethod]
        public void SuppliersController_Edit_GET_isValid()
        {
            SuppliersController controller = new SuppliersController();

            ViewResult result = controller.Edit(1) as ViewResult;
            Supplier SuppliersResult = (Supplier)result.Model;

            Assert.AreEqual(1, SuppliersResult.supplierID);
        }

        [TestMethod]
        public void SuppliersController_Edit_GET_isNotValid()
        {
            SuppliersController controller = new SuppliersController();

            HttpNotFoundResult result = controller.Edit(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }

        [TestMethod]
        public void SuppliersController_Delete_GET_isValid()
        {
            SuppliersController controller = new SuppliersController();

            ViewResult result = controller.Delete(1) as ViewResult;
            Supplier SuppliersResult = (Supplier)result.Model;

            Assert.AreEqual(1, SuppliersResult.supplierID);
        }

        [TestMethod]
        public void SuppliersController_Delete_GET_isNotValid()
        {
            SuppliersController controller = new SuppliersController();

            HttpNotFoundResult result = controller.Delete(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }
    }
}
