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

            #region
            Console.WriteLine("\t\t\t\t\t\t\t\tTask 2");
            #endregion
        }
    }
}