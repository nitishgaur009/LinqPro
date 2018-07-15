using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Linq.Business;

namespace Linq.Test
{
    [TestClass]
    public class StudentsTest
    {
        [TestMethod]
        public void CheckHelloWorld()
        {
            string name = "nitish2";

            Assert.AreEqual(StudentsBLL.GetHello(name), "Hello nitish");
        }
    }
}
