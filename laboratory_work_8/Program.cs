using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using class_library_laboratory_work_7;
#nullable enable

namespace laboratory_work_8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Animal animal = new Cow("Russia", "Burenka");
            Serialize(animal, "C:\\Users\\admin\\Downloads", "Cow");

            Animal? deserializeAnimal = Deserialize("C:\\Users\\admin\\Downloads", "Cow");
            if (deserializeAnimal is not null) Console.WriteLine(deserializeAnimal);
            else { Console.WriteLine("Animal is null"); }
        }
        public static void Serialize(Animal animal, string path = "", string name = "Animal")
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Animal));
            try
            {
                using (FileStream fileStream = new FileStream($"{path}\\{name}.xml", FileMode.OpenOrCreate))
                {
                    serializer.Serialize(fileStream, animal);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static Animal Deserialize(string path = "", string name = "Animal")
        {
            Animal? deserializeAnimal = null;
            XmlSerializer serializer = new XmlSerializer(typeof(Animal));
            try
            {
                using (FileStream fileStream = new FileStream($"{path}\\{name}.xml", FileMode.Open))
                {
                    deserializeAnimal = serializer.Deserialize(fileStream) as Animal;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return deserializeAnimal;
        }
    }

}
