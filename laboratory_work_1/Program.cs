using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratory_work_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region
            Console.WriteLine("\t\t\t\t\t\t\t\tTask 1");

            Console.WriteLine("\n\t\t\t\t\t\tЧисловые (целочисленные) типы:\n");

            Console.WriteLine($"Тип byte({typeof(byte).Name}): \t размер = {sizeof(byte)}, \t MIN = {byte.MinValue} | MAX = {byte.MaxValue}");

            Console.WriteLine($"Тип sbyte({typeof(sbyte).Name}): \t размер = {sizeof(sbyte)}, \t MIN = {sbyte.MinValue} | MAX = {sbyte.MaxValue}");

            Console.WriteLine($"Тип short({typeof(Int16).Name}): \t размер = {sizeof(Int16)}, \t MIN = {Int16.MinValue} | MAX = {Int16.MaxValue}");

            Console.WriteLine($"Тип ushort({typeof(UInt16).Name}): \t размер = {sizeof(UInt16)}, \t MIN = {UInt16.MinValue} | MAX = {UInt16.MaxValue}");

            Console.WriteLine($"Тип int({typeof(Int32).Name}): \t размер = {sizeof(Int32)}, \t MIN = {Int32.MinValue} | MAX = {Int32.MaxValue}");

            Console.WriteLine($"Тип uint({typeof(UInt32).Name}): \t размер = {sizeof(UInt32)}, \t MIN = {UInt32.MinValue} | MAX = {UInt32.MaxValue}");

            Console.WriteLine($"Тип long({typeof(Int64).Name}): \t размер = {sizeof(Int64)}, \t MIN = {Int64.MinValue} | MAX = {Int64.MaxValue}");

            Console.WriteLine($"Тип ulong({typeof(UInt64).Name}): \t размер = {sizeof(UInt64)}, \t MIN = {UInt64.MinValue} | MAX = {UInt64.MaxValue}");

            Console.WriteLine("\n\t\t\t\t\t\tЧисловые (с плавующей точкой) типы:\n");

            Console.WriteLine($"Тип float({typeof(float).Name}): \t размер = {sizeof(float)}, \t MIN = {float.MinValue} | MAX = {float.MaxValue}");

            Console.WriteLine($"Тип double({typeof(double).Name}): \t размер = {sizeof(double)}, \t MIN = {double.MinValue} | MAX = {double.MaxValue}");

            Console.WriteLine($"Тип decimal({typeof(decimal).Name}): \t размер = {sizeof(decimal)}, \t MIN = {decimal.MinValue} | MAX = {decimal.MaxValue}");

            Console.WriteLine("\n\t\t\t\t\t\tСимвольные типы:\n");

            Console.WriteLine($"Тип char({typeof(char).Name}): \t размер = {sizeof(char)}, \t MIN = {char.MinValue} | MAX = {char.MaxValue}");

            Console.WriteLine($"Тип string({typeof(string).Name}): \t размер = N/A, \t MIN = N/A | MAX = N/A");

            Console.WriteLine("\n\t\t\t\t\t\tЛогический тип:\n");

            Console.WriteLine($"Тип bool({typeof(bool).Name}): \t размер = {sizeof(bool)}, \t MIN = True | MAX = False");

            Console.WriteLine("\n\t\t\t\t\t\tОсобые типы:\n");

            Console.WriteLine($"Тип object({typeof(object).Name}): \t размер = N/A, \t MIN = N/A | MAX = N/A");

            Console.WriteLine($"Тип dynamic(N/A): \t размер = N/A, \t MIN = N/A | MAX = N/A");
            #endregion
        }
    }

    public class Rectangle
    {
        private double sideA, sideB;
        public double Area { get => CalculateArea(); }
        public double Perimeter { get => CalculatePerimeter(); }

        public Rectangle(double sideA, double sideB)
        {
            this.sideA = sideA;
            this.sideB = sideB;
        }

        private double CalculateArea()
        {
            return sideA * sideB;
        }

        private double CalculatePerimeter()
        {
            return 2 * (sideA + sideB);
        }
    }

    public class Point
    {
        private int _x, _y;
        public int x { get => _x; }
        public int y { get => _y; }
        public Point(int x, int y)
        {
            this._x = x;
            this._y = y;
        }
    }

    public class Figure
    {
        private Point point1, point2, point3, point4, point5;
        public string name { get; set; }

        public Figure(Point point1, Point point2, Point point3)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.point3 = point3;
            name = "triangle";
        }

        public Figure(Point point1, Point point2, Point point3, Point point4) : this(point1, point2, point3)
        {
            this.point4 = point4;
            name = "quadrilateral";
        }

        public Figure(Point point1, Point point2, Point point3, Point point4, Point point5) : this(point1, point2, point3, point4)
        {
            this.point5 = point5;
            name = "pentagon";
        }

        public double LengthSide(Point A, Point B)
        {
            return Math.Sqrt(Math.Pow(A.x - B.x, 2) + Math.Pow(A.y - B.y, 2));
        }

        public double PerimeterCalculator()
        {
            switch (name)
            {
                case "triangle":
                    return LengthSide(point1, point2) + LengthSide(point2, point3) + LengthSide(point3, point1);
                case "quadrilateral":
                    return LengthSide(point1, point2) + LengthSide(point2, point3) + LengthSide(point3, point4) + LengthSide(point4, point1);
                case "pentagon":
                    return LengthSide(point1, point2) + LengthSide(point2, point3) + LengthSide(point3, point4) + LengthSide(point4, point5) + LengthSide(point5, point1);
                default:
                    return 0;
            }
        }
    }
}