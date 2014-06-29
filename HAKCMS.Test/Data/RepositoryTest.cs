using HAKCMS.Data.Infrastructure;
using HAKCMS.Data.Repository;
using HAKCMS.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAKCMS.Test.Data
{

    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void PageRepository_Get_Data()
        {
            //Arrange
            //var dbFactory = new DatabaseFactory();
            //var repository = new PageRepository(dbFactory);
            var repositoryMocK = new Mock<IPageRepository>();

            var pages = new List<TBL_Page>();
            pages.Add(new TBL_Page { Name = "Home", Content = "content" });

            repositoryMocK.Setup(w => w.GetAll()).Returns(pages.AsEnumerable());

            //Act
            var list = repositoryMocK.Object.GetAll().ToList();


            //Assert
            Assert.IsTrue(list.Count != 0);




        }
    }
}
