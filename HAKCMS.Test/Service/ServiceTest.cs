using HAKCMS.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAKCMS.Test.Service
{
    [TestClass]
    public class ServiceTest
    {
        [TestMethod]
        public void PageService_GetAllData()
        {
            //Arrange
            var o = new TBL_Page() { Name = "Test", IsDeleted = true };
            var list = new List<TBL_Page>();
            list.Add(o);

            //Act


            //Assert
            Assert.IsTrue(list.Count == 0);
        }
    }
}
