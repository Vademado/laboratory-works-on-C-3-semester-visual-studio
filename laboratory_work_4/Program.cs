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
            Console.WriteLine("Введите размерность матрицы");
            Console.Write("Количество строк: ");
            uint m = Convert.ToUInt32(Console.ReadLine());
            Console.Write("Количество столбцов: ");
            uint n = Convert.ToUInt32(Console.ReadLine());
            Console.WriteLine("Введите диапозон элементов матрицы");
            Console.Write("Минимальное значение: ");
            int minValue = Convert.ToInt32(Console.ReadLine());
            Console.Write("Максимальное значение: ");
            int maxValue = Convert.ToInt32(Console.ReadLine());
            MyMatrix matrix = new MyMatrix(m, n, minValue, maxValue);
            Console.WriteLine(matrix);
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
    }

    class CarComparer : IComparer<Car>
    {

    }
}
