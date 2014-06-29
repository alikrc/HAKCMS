using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HAKCMS.Data.Identity;
using HAKCMS.Service.Identity;
using HAKCMS.Web.Areas.Admin.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HAKCMS.Web.Tests.AdminControllers
{
    [TestClass]
    public class UserControllerTest
    {
        public UserControllerTest()
        {

        }

        // how to mock owin?
        [TestMethod]
        public void Index()
        {
            // Arrange
            var controller = new UserController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
