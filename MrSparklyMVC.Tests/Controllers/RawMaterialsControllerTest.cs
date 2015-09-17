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
    public class RawMaterialsControllerTest
    {
        [TestMethod]
        public void RawMaterialsController_Index()
        {
            RawMaterialsController controller = new RawMaterialsController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void RawMaterialsController_Details_isValid()
        {
            RawMaterialsController controller = new RawMaterialsController();

            ViewResult result = controller.Details(1) as ViewResult;
            RawMaterial rawMatResult = (RawMaterial)result.Model;

            Assert.AreEqual(1, rawMatResult.rawMaterialsID);
        }

        [TestMethod]
        public void RawMaterialsController_Details_isNotValid()
        {
            RawMaterialsController controller = new RawMaterialsController();

            HttpNotFoundResult result = controller.Details(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }

        [TestMethod]
        public void RawMaterialsController_Create_isValid()
        {
            RawMaterial testRawMaterials = new RawMaterial();
            testRawMaterials.rawMaterialsName = "testBrand";
            testRawMaterials.rawMaterialsPrice = 1;
            testRawMaterials.rawMaterialsQty = 5;
            RawMaterialsController controller = new RawMaterialsController();

            var result = (RedirectToRouteResult)controller.Create(testRawMaterials);

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void RawMaterialsController_Create_isNotValid()
        {
            RawMaterial testRawMaterials = new RawMaterial();
            testRawMaterials.rawMaterialsName = "invalidTestBrand";
            RawMaterialsController controller = new RawMaterialsController();
            controller.ModelState.AddModelError("", "error message");

            var result = controller.Create(testRawMaterials) as ViewResult;
            RawMaterial resultRawMaterials = (RawMaterial)result.Model;

            Assert.AreEqual("invalidTestBrand", resultRawMaterials.rawMaterialsName);
        }

        [TestMethod]
        public void RawMaterialsController_Edit_GET_isValid()
        {
            RawMaterialsController controller = new RawMaterialsController();

            ViewResult result = controller.Edit(1) as ViewResult;
            RawMaterial RawMaterialsResult = (RawMaterial)result.Model;

            Assert.AreEqual(1, RawMaterialsResult.rawMaterialsID);
        }

        [TestMethod]
        public void RawMaterialsController_Edit_GET_isNotValid()
        {
            RawMaterialsController controller = new RawMaterialsController();

            HttpNotFoundResult result = controller.Edit(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }

        [TestMethod]
        public void RawMaterialsController_Delete_GET_isValid()
        {
            RawMaterialsController controller = new RawMaterialsController();

            ViewResult result = controller.Delete(1) as ViewResult;
            RawMaterial RawMaterialsResult = (RawMaterial)result.Model;

            Assert.AreEqual(1, RawMaterialsResult.rawMaterialsID);
        }

        [TestMethod]
        public void RawMaterialsController_Delete_GET_isNotValid()
        {
            RawMaterialsController controller = new RawMaterialsController();

            HttpNotFoundResult result = controller.Delete(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }
    }
}
