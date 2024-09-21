using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratory_work_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Введите размерность матрицы");
            //Console.Write("Количество строк: ");
            //uint m = Convert.ToUInt32(Console.ReadLine());
            //Console.Write("Количество столбцов: ");
            //uint n = Convert.ToUInt32(Console.ReadLine());
            //Console.WriteLine("Введите диапозон элементов матрицы");
            //Console.Write("Минимальное значение: ");
            //int minValue = Convert.ToInt32(Console.ReadLine());
            //Console.Write("Максимальное значение: ");
            //int maxValue = Convert.ToInt32(Console.ReadLine());
            //MyMatrix matrix = new MyMatrix(m, n, minValue, maxValue);
            //Console.WriteLine(matrix);

            Car[] cars =
            {
                new Car("Mercedes-Benz CLK GTR", 1999, 320),
                new Car("Ferrari F40", 1992, 324),
                new Car("Porsche 911 GT1", 1998, 330)
            };
            Console.WriteLine("Sorting by name");
            Array.Sort(cars, new CarComparer(CarComparer.CompareBy.Name));
            foreach (Car car in cars)
            {
                Console.WriteLine(car);
            }
            Console.Write("\n\n");
            
            Console.WriteLine("Sorting by production year");
            Array.Sort(cars, new CarComparer(CarComparer.CompareBy.ProductionYear));
            foreach (Car car in cars)
            {
                Console.WriteLine(car);
            }
            Console.Write("\n\n");
            
            Console.WriteLine("Sorting by max speed");
            Array.Sort(cars, new CarComparer(CarComparer.CompareBy.MaxSpeed));
            foreach (Car car in cars)
            {
                Console.WriteLine(car);
            }
            Console.Write("\n\n");
        }
    }

    class MyMatrix
    {
        private double[,] matrix;
        private double minValue = double.MaxValue, maxValue = double.MinValue;
        private static Random random = new Random();
        public double MinValue { get => minValue; set { if (value < minValue) minValue = value; } }
        public double MaxValue { get => maxValue; set { if (value > maxValue) maxValue = value; } }
        public uint Rows { get; }
        public uint Columns { get; }

        public double this[uint row, uint column]
        {
            get { return matrix[row, column]; }
            set
            {
                if (value < minValue) minValue = value;
                if (value > maxValue) maxValue = value;
                matrix[row, column] = value;
            }
        }

        public MyMatrix(uint rows, uint columns, double minValue = double.MaxValue, double maxValue = double.MinValue)
        {
            Rows = rows;
            Columns = columns;
            MinValue = minValue;
            MaxValue = maxValue;
            matrix = new double[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = random.Next((int)minValue, (int)maxValue + 1);
                }
            }
        }

        public static MyMatrix operator +(MyMatrix matrix1, MyMatrix matrix2)
        {
            if (matrix1.Rows != matrix2.Rows || matrix1.Columns != matrix2.Columns) return null;
            MyMatrix resultingMatrix = new MyMatrix(matrix1.Rows, matrix1.Columns);
            for (uint i = 0; i < resultingMatrix.Rows; i++)
            {
                for (uint j = 0; j < resultingMatrix.Columns; j++)
                {
                    resultingMatrix[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }
            return resultingMatrix;
        }

        public static MyMatrix operator -(MyMatrix matrix1, MyMatrix matrix2)
        {
            if (matrix1.Rows != matrix2.Rows || matrix1.Columns != matrix2.Columns) return null;
            MyMatrix resultingMatrix = new MyMatrix(matrix1.Rows, matrix1.Columns);
            for (uint i = 0; i < resultingMatrix.Rows; i++)
            {
                for (uint j = 0; j < resultingMatrix.Columns; j++)
                {
                    resultingMatrix[i, j] = matrix1[i, j] - matrix2[i, j];
                }
            }
            return resultingMatrix;
        }

        public static MyMatrix operator *(MyMatrix matrix1, MyMatrix matrix2)
        {
            if (matrix1.Columns != matrix2.Rows) return null;
            MyMatrix resultingMatrix = new MyMatrix(matrix1.Rows, matrix2.Columns);
            for (uint i = 0; i < resultingMatrix.Rows; i++)
            {
                for (uint j = 0; j < resultingMatrix.Columns; j++)
                {
                    resultingMatrix[i, j] = 0;
                    for (uint k = 0; k < resultingMatrix.Columns; k++)
                    {
                        resultingMatrix[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }
            return resultingMatrix;
        }

        public static MyMatrix operator *(MyMatrix matrix, double number)
        {
            MyMatrix resultingMatrix = new MyMatrix(matrix.Rows, matrix.Columns);
            for (uint i = 0; i < resultingMatrix.Rows; i++)
            {
                for (uint j = 0; j < resultingMatrix.Columns; j++)
                {
                    resultingMatrix[i, j] = matrix[i, j] * number;
                }
            }
            return resultingMatrix;
        }

        public static MyMatrix operator *(double number, MyMatrix matrix)
        {
            return matrix * number;
        }

        public static MyMatrix operator /(MyMatrix matrix, double number)
        {
            MyMatrix resultingMatrix = new MyMatrix(matrix.Rows, matrix.Columns);
            for (uint i = 0; i < resultingMatrix.Rows; i++)
            {
                for (uint j = 0; j < resultingMatrix.Columns; j++)
                {
                    resultingMatrix[i, j] = matrix[i, j] / number;
                }
            }
            return resultingMatrix;
        }

        public static MyMatrix operator /(double number, MyMatrix matrix)
        {
            return matrix / number;
        }

        public override string ToString()
        {
            string stringMatrix = "";
            for (uint i = 0; i < Rows; i++)
            {
                for (uint j = 0; j < Columns; j++)
                {
                    stringMatrix += $"{this[i, j]} ";
                }
                stringMatrix += "\n";
            }
            return stringMatrix;
        }
    }

    class Car
    {
        public string Name { get; set; }
        public int ProductionYear { get; set; }
        public int MaxSpeed { get; set; }

        public Car(string name, int productionYear, int maxSpeed)
        {
            Name = name;
            ProductionYear = productionYear;
            MaxSpeed = maxSpeed;
        }
        public override string ToString()
        {
            return $"{Name}  production year: {ProductionYear}  max speed: {MaxSpeed}";
        }
    }

    class CarComparer : IComparer<Car>
    {
        private CompareBy compareBy;
        public enum CompareBy : byte
        {
            Name,
            ProductionYear,
            MaxSpeed
        }

        public CarComparer(CompareBy compareBy)
        {
            this.compareBy = compareBy;
        }

        public int Compare(Car x, Car y)
        {
            if (x is null || y is null)
                throw new ArgumentException("Invalid parameter value");
            switch (compareBy)
            {
                case CompareBy.Name:
                    return x.Name.CompareTo(y.Name);
                case CompareBy.ProductionYear:
                    return x.ProductionYear.CompareTo(y.ProductionYear);
                case CompareBy.MaxSpeed:
                    return x.MaxSpeed.CompareTo(y.MaxSpeed);
                default:
                    return x.MaxSpeed.CompareTo(y.MaxSpeed);
            }
        }
    }
}
