using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratory_work_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }

    struct Vector
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        public double Length { get; }

        public Vector(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
            Length = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
        }

        public static Vector operator +(Vector vector1, Vector vector2)
        {
            return new Vector(vector1.X + vector2.X, vector1.Y + vector2.Y, vector1.Z + vector2.Z);
        }

        public static Vector operator *(Vector vector1, Vector vector2)
        {
            return new Vector(vector1.Y * vector2.Z - vector1.Z * vector2.Y,
                vector1.Z * vector2.X - vector1.X * vector2.Z,
                vector1.X * vector2.Y - vector1.Y * vector2.X);

        }

        public static Vector operator *(Vector vector, int number)
        {
            return new Vector(vector.X * number, vector.Y * number, vector.Z * number);
        }

        public static Vector operator *(int number, Vector vector)
        {
            return vector * number;
        }

        public static bool operator >(Vector vector1, Vector vector2)
        {
            return vector1.Length > vector2.Length;
        }

        public static bool operator <(Vector vector1, Vector vector2)
        {
            return vector1.Length < vector2.Length;
        }

        public static bool operator ==(Vector vector1, Vector vector2)
        {
            return vector1.X == vector2.X && vector1.Y == vector2.Y && vector1.Z == vector2.Z;
        }

        public static bool operator !=(Vector vector1, Vector vector2)
        {
            return !(vector1 == vector2);
        }
    }

    class Car : IEquatable<Car>
    {
        public string Name { get; set; }
        public string Engine { get; set; }
        public int MaxSpeed { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(Car other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Name == other.Name && Engine == other.Engine && MaxSpeed == other.MaxSpeed;
        }

        public static bool operator ==(Car car1, Car car2)
        {

            return car1.MaxSpeed == car2.MaxSpeed && car1.Engine == car2.Engine && car1.Engine == car2.Engine;
        }
        public static bool operator !=(Car car1, Car car2)
        {
            return !(car1 == car2);
        }

    }

    class CarsCatalog
    {
        private List<Car> cars;

        public CarsCatalog(List<Car> cars)
        {
            this.cars = cars;
        }

        public string this[int index]
        {
            get { return $"Name: {cars[index].Name}\nEnigme: {cars[index].Engine}"; }
        }
    }

    class Currency
    {
        public float Value { get; private set; }

        public Currency(float value)
        {
            Value = value;
        }
    }

    class CurrencyUSD : Currency
    {
        public CurrencyUSD(float value) : base(value) { }

        public static explicit operator CurrencyEUR(CurrencyUSD item)
        {
            return new CurrencyEUR(item.Value * 1.11f);
        }

        public static explicit operator CurrencyRUB(CurrencyUSD item)
        {
            return new CurrencyRUB(item.Value * 91.14f);
        }
    }

    class CurrencyEUR : Currency
    {
        public CurrencyEUR(float value) : base(value) { }

        public static explicit operator CurrencyUSD(CurrencyEUR item)
        {
            return new CurrencyUSD(item.Value / 1.11f);
        }

        public static explicit operator CurrencyRUB(CurrencyEUR item)
        {
            return new CurrencyRUB(item.Value / 101.28f);
        }
    }

    class CurrencyRUB : Currency
    {
        public CurrencyRUB(float value) : base(value) { }

        public static explicit operator CurrencyUSD(CurrencyRUB item)
        {
            return new CurrencyUSD(item.Value / 91.14f);
        }

        public static explicit operator CurrencyEUR(CurrencyRUB item)
        {
            return new CurrencyEUR(item.Value / 101.28f);
        }
    }
}
