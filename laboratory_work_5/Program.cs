using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace laboratory_work_5
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
            matrix.Show();
        }
    }

    class MyMatrix
    {
        private double[,] matrix;
        private double minValue = double.MaxValue, maxValue = double.MinValue;
        private static Random random = new Random();
        public double MinValue { get => minValue; set { if (value < minValue) minValue = value; } }
        public double MaxValue { get => maxValue; set { if (value > maxValue) maxValue = value; } }
        public uint Rows { get; set; }
        public uint Columns { get; set; }

        public double this[uint row, uint column]
        {
            get => matrix[row, column];
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
            Fill();
        }

        private void Fill()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    matrix[i, j] = random.Next((int)minValue, (int)maxValue + 1);
                }
            }
        }

        public MyMatrix ChangeSize(uint rows, uint columns)
        {
            MyMatrix resultingMatrix = new MyMatrix(rows, columns, minValue, maxValue);
            for (uint i = 0; i < Math.Min(Rows, rows); i++)
            {
                for (uint j = 0; j < Math.Min(Columns, columns); j++)
                {
                    resultingMatrix[i, j] = matrix[i, j];
                }
                if (columns > Columns)
                {
                    for (uint j = Columns; j < columns; j++)
                    {
                        resultingMatrix[i, j] = random.Next((int)minValue, (int)maxValue + 1);
                    }
                }
            }
            if (rows > Rows)
            {
                for (uint i = Rows; i < rows; i++)
                {
                    for (uint j = 0; j < Math.Min(Columns, columns); j++)
                    {
                        resultingMatrix[i, j] = random.Next((int)minValue, (int)maxValue + 1);
                    }
                    if (columns > Columns)
                    {
                        for (uint j = Columns; j < columns; j++)
                        {
                            resultingMatrix[i, j] = random.Next((int)minValue, (int)maxValue + 1);
                        }
                    }
                }
            }
            return resultingMatrix;
        }

        public void ShowPartialy(uint startRow, uint startColumn, uint endRow, uint endColumn)
        {
            for (uint i = startRow - 1; i < endRow; i++)
            {
                for (uint j = startColumn - 1; j < endColumn; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.Write("\n");
            }
        }

        public void Show()
        {
            for (uint i = 0; i < Rows; i++)
            {
                for (uint j = 0; j < Columns; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.Write("\n");
            }
        }
    }

    class MyList<T> : IEnumerable<T>
    {
        private T[] list;
        public int Length { get; private set; } = 0;

        public T this[int index]
        {
            get => list[index];
            set => list[index] = value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Length; i++) yield return list[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public MyList()
        {
            list = new T[Length];
        }

        public MyList(T[] array)
        {
            list = array;
            Length = array.Length;
        }

        public MyList(int length)
        {
            list = new T[length];
        }
        public void Add(T item)
        {
            T[] resultingList = new T[Length + 1];
            for (int i = 0; i < Length; i++) resultingList[i] = list[i];
            resultingList[Length] = item;
            list = resultingList;
            Length++;
        }
    }

    class MyKeyValuePair<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public MyKeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        class MyDictionary<TKey, TValue> : IEnumerable<MyKeyValuePair<TKey, TValue>>
        {
            private MyKeyValuePair<TKey, TValue>[] dictionary;
            public int Count { get; private set; } = 0;

            public TValue this[TKey key]
            {
                get
                {
                    for (int i = 0; i < Count; i++)
                    {
                        if (dictionary[i].Key.Equals(key)) return dictionary[i].Value;
                    }
                    throw new ArgumentException();
                }
                set
                {
                    bool found = false;
                    for (int i = 0; i < Count; i++)
                    {
                        if (dictionary[i].Key.Equals(key))
                        {
                            dictionary[i].Value = value;
                            found = true;
                            break;
                        }
                    }
                    if (!found) Add(key, value);
                }
            }

            public IEnumerator<MyKeyValuePair<TKey, TValue>> GetEnumerator()
            {
                for (int i = 0; i < Count; i++)
                    yield return dictionary[i];
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
            public MyDictionary()
            {
                dictionary = new MyKeyValuePair<TKey, TValue>[0];
            }

            public MyDictionary(MyKeyValuePair<TKey, TValue>[] item)
            {
                MyKeyValuePair<TKey, TValue>[] resultingDictionary = new MyKeyValuePair<TKey, TValue>[item.Length];
                for (int i = 0; i < resultingDictionary.Length; i++)
                {
                    resultingDictionary[i] = item[i];
                }
                dictionary = resultingDictionary;
                Count = resultingDictionary.Length;
            }

            public MyDictionary(MyKeyValuePair<TKey, TValue> item)
            {
                MyKeyValuePair<TKey, TValue>[] resultingDictionary = new MyKeyValuePair<TKey, TValue>[1];
                MyKeyValuePair<TKey, TValue> resultingItem = new MyKeyValuePair<TKey, TValue>(item.Key, item.Value);
                resultingDictionary[0] = resultingItem;
                dictionary = resultingDictionary;
                Count = resultingDictionary.Length;
            }

            public void Add(TKey key, TValue value)
            {
                MyKeyValuePair<TKey, TValue>[] resultingDictionary = new MyKeyValuePair<TKey, TValue>[Count + 1];
                MyKeyValuePair<TKey, TValue> resultingItem = new MyKeyValuePair<TKey, TValue>(key, value);
                for (int i = 0; i < Count; i++)
                {
                    resultingDictionary[i] = dictionary[i];
                }
                resultingDictionary[Count] = resultingItem;
                dictionary = resultingDictionary;
                Count = resultingDictionary.Length;
            }
        }
    }
}
