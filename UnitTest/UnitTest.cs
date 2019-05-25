using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    [TestCategory("MyUnitTest")]
    public class UnitTest
    {
        [TestMethod]
        [TestCategory("MyUnitTest")]
        public void TestMethod()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        [TestCategory("MyUnitTest")]
        public void AddNumberTest()
        {
            //正确数据，最好是旧版本已经过校验的数据
            int i = 5, j = 6;
            int result = 11;

            Assert.AreEqual(result, i + j);
        }
    }
}
