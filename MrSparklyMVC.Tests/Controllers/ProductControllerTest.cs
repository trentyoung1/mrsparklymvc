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
    public class ProductControllerTest
    {
        [TestMethod]
        public void ProductController_Index()
        {
            ProductController controller = new ProductController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void ProductController_Details_isValid()
        {
            ProductController controller = new ProductController();

            ViewResult result = controller.Details(1) as ViewResult;
            Product productResult = (Product)result.Model;
            
            Assert.AreEqual(1, productResult.productID);
        }

        [TestMethod]
        public void ProductController_Details_isNotValid()
        {
            ProductController controller = new ProductController();

            HttpNotFoundResult result = controller.Details(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }

        [TestMethod]
        public void ProductController_GetProductPrice_isValid()
        {
            ProductController controller = new ProductController();

            JsonResult result = controller.GetProductPrice(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductController_GetProductPrice_isNotValid()
        {
            ProductController controller = new ProductController();

            JsonResult result = controller.GetProductPrice(999999);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ProductController_Create_isValid()
        {
            Product testProduct = new Product();
            testProduct.productBrandName = "testBrand";
            testProduct.productCostPrice = 1;
            testProduct.productRetailPrice = 2;
            testProduct.productQty = 5;
            ProductController controller = new ProductController();

            var result = (RedirectToRouteResult)controller.Create(testProduct);

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void ProductController_Create_isNotValid()
        {
            Product testProduct = new Product();
            testProduct.productBrandName = "invalidTestBrand";
            ProductController controller = new ProductController();
            controller.ModelState.AddModelError("", "error message");

            var result = controller.Create(testProduct) as ViewResult;
            Product resultProduct = (Product)result.Model;

            Assert.AreEqual("invalidTestBrand", resultProduct.productBrandName);
        }
        
        [TestMethod]
        public void ProductController_Edit_GET_isValid()
        {
            ProductController controller = new ProductController();

            ViewResult result = controller.Edit(1) as ViewResult;
            Product productResult = (Product)result.Model;

            Assert.AreEqual(1, productResult.productID);
        }

        [TestMethod]
        public void ProductController_Edit_GET_isNotValid()
        {
            ProductController controller = new ProductController();

            HttpNotFoundResult result = controller.Edit(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }

        [TestMethod]
        public void ProductController_Delete_GET_isValid()
        {
            ProductController controller = new ProductController();

            ViewResult result = controller.Delete(1) as ViewResult;
            Product productResult = (Product)result.Model;

            Assert.AreEqual(1, productResult.productID);
        }

        [TestMethod]
        public void ProductController_Delete_GET_isNotValid()
        {
            ProductController controller = new ProductController();

            HttpNotFoundResult result = controller.Delete(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }
    }
}
