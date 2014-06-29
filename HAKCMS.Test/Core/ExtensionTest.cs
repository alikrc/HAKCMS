using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HAKCMS.Model;
using System.Collections.Generic;
using System.Collections;
using HAKCMS.Core.Extension;

namespace HAKCMS.Test
{
    [TestClass]
    public class ExtensionTest
    {
        [TestMethod]
        public void IsDeleted_Records_Excluded()
        {
            //Arrange
            var o = new TBL_Page() { Name = "Test", IsDeleted = true };
            var list = new List<TBL_Page>();
            list.Add(o);

            //Act
            list = (List<TBL_Page>)list.ExtractDeletedRecords<TBL_Page>();

            //Assert
            Assert.IsTrue(list.Count == 0);
        }
    }
}
