using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Globalization;
using WindowsFormsApp;

namespace WindowsFormsAppTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DistanceTest1()
        {
            Line l = new Line(new Point(1, 1), new Point(1, 3));
            double expected = 2;

            double actual = l.Distance();

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ArgbColorTest()
        {
            Line l = new Line();
            l.LineColor= Color.White;
            int expected = Int32.Parse("#FFFFFFFF".Replace("#", ""), NumberStyles.HexNumber);

            int actual = l.LineColor.ToArgb();

            Assert.AreEqual(expected, actual);
        }
    }
}
