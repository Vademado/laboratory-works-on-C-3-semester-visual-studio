using laboratory_work_1;

namespace test_laboratory_work_1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Rectangle obj = new Rectangle(3, 4);
            double programmAnswer = obj.Area;

            Assert.AreEqual(programmAnswer, 12.0);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Rectangle obj = new Rectangle(4, 5);
            double programmAnswer = obj.Perimeter;

            Assert.AreEqual(programmAnswer, 18.0);
        }

        [TestMethod]
        public void TestMethod3() 
        { 
            Point point1 = new Point(0, 0);
            Point point2 = new Point(0, 3);
            Point point3 = new Point(4, 0);

            Figure obj = new Figure(point1, point2, point3);
            (string, double, double, double, double) programmAnswer = (obj.Name, obj.LengthSide(point1, point2), obj.LengthSide(point2, point3), obj.LengthSide(point3, point1), obj.PerimeterCalculator());

            Assert.AreEqual(("triangle", 3.0, 5.0, 4.0, 12.0), programmAnswer);
        }

        [TestMethod]
        public void TestMethod4()
        {
            Point point1 = new Point(-4, 0);
            Point point2 = new Point(0, 3);
            Point point3 = new Point(4, 0);
            Point point4 = new Point(0, -3);

            Figure obj = new Figure(point1, point2, point3, point4);
            (string, double, double, double, double, double) programmAnswer = (obj.Name, obj.LengthSide(point1, point2), obj.LengthSide(point2, point3), obj.LengthSide(point3, point4), obj.LengthSide(point4, point1), obj.PerimeterCalculator());

            Assert.AreEqual(("quadrilateral", 5.0, 5.0, 5.0, 5.0, 20.0), programmAnswer);
        }

        [TestMethod]
        public void TestMethod5()
        {
            Point point1 = new Point(-4, 0);
            Point point2 = new Point(0, 3);
            Point point3 = new Point(4, 0);
            Point point4 = new Point(4, -5);
            Point point5 = new Point(-4, -5);

            Figure obj = new Figure(point1, point2, point3, point4, point5);
            (string, double, double, double, double, double, double) programmAnswer = (obj.Name, obj.LengthSide(point1, point2), obj.LengthSide(point2, point3), obj.LengthSide(point3, point4), obj.LengthSide(point4, point5), obj.LengthSide(point5, point1), obj.PerimeterCalculator());

            Assert.AreEqual(("pentagon", 5.0, 5.0, 5.0, 8.0, 5.0, 28.0), programmAnswer);
        }
    }
}