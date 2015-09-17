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
    public class EmployeesControllerTest
    {
        [TestMethod]
        public void EmployeesController_Index()
        {
            EmployeesController controller = new EmployeesController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void EmployeesController_Details_isValid()
        {
            EmployeesController controller = new EmployeesController();

            ViewResult result = controller.Details(1) as ViewResult;
            Employee employeeResult = (Employee)result.Model;

            Assert.AreEqual(1, employeeResult.employeeID);
        }

        [TestMethod]
        public void EmployeesController_Details_isNotValid()
        {
            EmployeesController controller = new EmployeesController();

            HttpNotFoundResult result = controller.Details(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }

        [TestMethod]
        public void EmployeesController_Create_isValid()
        {
            Employee testEmployee = new Employee();
            testEmployee.employeeDepartment = "Sales";
            testEmployee.employeeEmail = "testemployee@test.gov";
            testEmployee.employeeStreet = "test street";
            testEmployee.employeeFirstName = "TestEmp";
            testEmployee.employeeLastName = "TestLast";

            EmployeesController controller = new EmployeesController();

            var result = (RedirectToRouteResult)controller.Create(testEmployee);

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void EmployeesController_Create_isNotValid()
        {
            Employee testEmployee = new Employee();
            testEmployee.employeeFirstName = "invalidEmployee";
            EmployeesController controller = new EmployeesController();
            controller.ModelState.AddModelError("", "error message");

            var result = controller.Create(testEmployee) as ViewResult;
            Employee resultEmployees = (Employee)result.Model;

            Assert.AreEqual("invalidEmployee", resultEmployees.employeeFirstName);
        }

        [TestMethod]
        public void EmployeesController_Edit_GET_isValid()
        {
            EmployeesController controller = new EmployeesController();

            ViewResult result = controller.Edit(1) as ViewResult;
            Employee EmployeesResult = (Employee)result.Model;

            Assert.AreEqual(1, EmployeesResult.employeeID);
        }

        [TestMethod]
        public void EmployeesController_Edit_GET_isNotValid()
        {
            EmployeesController controller = new EmployeesController();

            HttpNotFoundResult result = controller.Edit(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }

        [TestMethod]
        public void EmployeesController_Delete_GET_isValid()
        {
            EmployeesController controller = new EmployeesController();

            ViewResult result = controller.Delete(1) as ViewResult;
            Employee EmployeesResult = (Employee)result.Model;

            Assert.AreEqual(1, EmployeesResult.employeeID);
        }

        [TestMethod]
        public void EmployeesController_Delete_GET_isNotValid()
        {
            EmployeesController controller = new EmployeesController();

            HttpNotFoundResult result = controller.Delete(9999999) as HttpNotFoundResult;
            var expectedResult = new HttpNotFoundResult().GetType();

            Assert.IsInstanceOfType(result, expectedResult);
        }
    }
}
