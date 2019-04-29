using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Addressbook.Web.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodSquare()
        {
            Square s1 = new Square(10);
            Square s2 = new Square(20);
            Square s3 = s1;

            Assert.AreEqual(s1.Size, 10);
            Assert.AreEqual(s2.Size, 20);
            Assert.AreEqual(s3.Size, 10);

            s3.Size = 15;
            Assert.AreEqual(s1.Size, 15);
            s2.Colored = true;
        }

        [TestMethod]
        public void TestMethodCircle()
        {
            Circle s1 = new Circle(10);
            Circle s2 = new Circle(20);
            Circle s3 = s1;

            Assert.AreEqual(s1.Radius, 10);
            Assert.AreEqual(s2.Radius, 20);
            Assert.AreEqual(s3.Radius, 10);

            s3.Radius = 15;
            Assert.AreEqual(s1.Radius, 15);
        
        }
    }
}
