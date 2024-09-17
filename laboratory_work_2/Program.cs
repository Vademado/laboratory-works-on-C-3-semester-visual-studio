using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratory_work_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите ключ доступа: ");
            string key = Console.ReadLine();
            DocumentWorker worker = DocumentWorker.Access(key);
        }
    }

    abstract class Pupil
    {
        public string Name { get; }
        public string Surname { get; }
        public string Patronymic { get; }

        public Pupil(string name, string surname, string patronymic)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
        }

        public abstract void Study();

        public abstract void Read();

        public abstract void Write();

        public abstract void Relax();

        public string GetFullName()
        {
            return $"{Name} {Patronymic} {Surname}";
        }
    }

    class ExcelentPupil : Pupil
    {
        public ExcelentPupil(string name, string surname, string patronymic) : base(name, surname, patronymic) { }

        public override void Study()
        {
            Console.WriteLine("Ура! Снова учиться!");
        }

        public override void Read()
        {
            Console.WriteLine("Люблю читать Пушкина!");
        }

        public override void Write()
        {
            Console.WriteLine("Люблю писать сочинения!");
        }

        public override void Relax()
        {
            Console.WriteLine("Какой отдых? Пойду готовиться к следующим урокам.");
        }
    }

    class GoodPupil : Pupil
    {
        public GoodPupil(string name, string surname, string patronymic) : base(name, surname, patronymic) { }

        public override void Study()
        {
            Console.WriteLine("Время учиться.");
        }

        public override void Read()
        {
            Console.WriteLine("С какого момента мне читать?");
        }

        public override void Write()
        {
            Console.WriteLine("А на сколько слов писать сочинение?");
        }

        public override void Relax()
        {
            Console.WriteLine("Время прогуляться.");
        }
    }

    class BadPupil : Pupil
    {
        public BadPupil(string name, string surname, string patronymic) : base(name, surname, patronymic) { }

        public override void Study()
        {
            Console.WriteLine("Снова на эту учёбу...");
        }

        public override void Read()
        {
            Console.WriteLine("Кому сдалась эта классика?");
        }

        public override void Write()
        {
            Console.WriteLine("Снова сочинение писать...");
        }

        public override void Relax()
        {
            Console.WriteLine("Так не хочется идти на этот урок...");
        }
    }

    class ClassRoom
    {

        private Pupil[] pupils;
        public Pupil[] Pupils
        {
            get
            {
                Pupil[] GetPupils = new Pupil[PupilsInClass];
                for (int i = 0; i < PupilsInClass; i++)
                {
                    GetPupils[i] = pupils[i];
                }
                return GetPupils;
            }
        }
        public int PupilsInClass { get; }        

        public ClassRoom(params Pupil[] pupils)
        {
            this.pupils = pupils;
            PupilsInClass = pupils.Length;
        }

        public void Lesson()
        {
            Console.WriteLine("Начнём урок!");
            foreach (Pupil pupil in pupils)
            {
                Console.WriteLine($"{pupil.Name}, прозвенел звонок. Пора в класс.");
                pupil.Study();
                Console.WriteLine($"{pupil.Name}, читай пожалуйста.");
                pupil.Read();
                Console.WriteLine($"А теперь, {pupil.Name}, нужно написать сочинение по произведению.");
                pupil.Write();
                Console.WriteLine($"{pupil.Name}, урок окончен. Можешь отдыхать.");
                pupil.Relax();
                Console.Write("\n\n");
            }
        }
    }


    class Vehicle
    {
        protected string vehicleType;
        public (int, int) Coordinates { get; set; } = (0, 0);
        public int Price { get; set; }
        public int Speed { get; set; } = 0;
        public int YearOfRelease { get; }

        public Vehicle(int price, int yearOfRelease)
        {
            Price = price;
            YearOfRelease = yearOfRelease;
        }

        public virtual void VehicleInformation()
        {
            Console.WriteLine($"Вид транспортного средства: {vehicleType}");
            Console.WriteLine($"Цена: {Price}");
            Console.WriteLine($"Год выпуска: {YearOfRelease}");
            Console.WriteLine($"Координаты: ({Coordinates.Item1}, {Coordinates.Item2})");
            Console.WriteLine($"Скорость: {Speed}");
        }
    }

    class Plane : Vehicle
    {
        public int Height { get; set; } = 0;
        public int NumberOfPassengers { get; } = 0;

        public Plane(int price, int yearOfRelease, int speed = 0, int height = 0, int numberOfPassengers = 0) : base(price, yearOfRelease)
        {
            vehicleType = "Airplane";
            Speed = speed;
            Height = height;
            NumberOfPassengers = numberOfPassengers;
        }

        public override void VehicleInformation()
        {
            base.VehicleInformation();
            Console.WriteLine($"Высота полёта: {Height}");
            Console.WriteLine($"Пассажиров на борту: {NumberOfPassengers}");
        }
    }

    class Car : Vehicle
    {
        public int NumberOfPassengers { get; } = 0;
        public Car(int price, int yearOfRelease, int speed = 0, int numberOfPassengers = 0) : base(price, yearOfRelease)
        {
            vehicleType = "Car";
            Speed = speed;
            NumberOfPassengers = numberOfPassengers;
        }

        public override void VehicleInformation()
        {
            base.VehicleInformation();
            Console.WriteLine($"Пассажиров в автомобиле: {NumberOfPassengers}");
        }
    }

    class Ship : Vehicle
    {
        public int NumberOfPassengers { get; } = 0;
        public string HomePort { get; }

        public Ship(int price, int yearOfRelease, int speed = 0, string homePort = "Nan", int numberOfPassengers = 0) : base(price, yearOfRelease)
        {
            vehicleType = "Ship";
            Speed = speed;
            HomePort = homePort;
            NumberOfPassengers = numberOfPassengers;
        }

        public override void VehicleInformation()
        {
            base.VehicleInformation();
            Console.WriteLine($"Пассажиров на корабле: {NumberOfPassengers}");
            Console.WriteLine($"Порт приписки: {HomePort}");
        }
    }


    class DocumentWorker
    {
        public void OpenDocument()
        {
            Console.WriteLine("Документ открыт");
        }

        public virtual void EditDocument()
        {
            Console.WriteLine("Редактирование документа доступно в версии Pro");
        }

        public virtual void SaveDocument()
        {
            Console.WriteLine("Сохранение документа доступно в версии Pro");
        }

        public static DocumentWorker Access(string key)
        {
            if (key == "ExpertAccess")
            {
                return new ExpertDocumentWorker();
            } else if (key == "ProAccess") 
            { 
                return new ProDocumentWorker();
            }
            return new DocumentWorker();
        }
    }

    class ProDocumentWorker : DocumentWorker
    {
        public override void EditDocument()
        {
            Console.WriteLine("Документ отредактирован");
        }

        public override void SaveDocument()
        {
            Console.WriteLine("Документ сохранен в старом формате, сохранение в остальных форматах доступно в версии Expert");
        }
    }

    class ExpertDocumentWorker : ProDocumentWorker
    {
        public override void SaveDocument()
        {
            Console.WriteLine("Документ сохранен в новом формате");
        }
    }
}
