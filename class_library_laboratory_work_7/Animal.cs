using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace class_library_laboratory_work_7
{
    [Comment("abstract class Animal")]
    [XmlInclude(typeof(Cow)), XmlInclude(typeof(Lion)), XmlInclude(typeof(Pig))]
    public abstract class Animal
    {
        public string Country { get; set; }
        public bool HideFromOtherAnimals { get; set; }
        public string Name { get; set; }

        public abstract eClassificationAnimal WhatAnimal { get; }

        public Animal() { }

        public Animal(string country, string name, bool hideFromOtherAnimals = true)
        {
            Country = country;
            Name = name;
            HideFromOtherAnimals = hideFromOtherAnimals;
        }

        public abstract void Deconstruct();
        public eClassificationAnimal GetClassificationAnimal()
        {
            return WhatAnimal;
        }
        public abstract eFavoriteFood GetFavouriteFood();
        public abstract void SayHello();

        public override string ToString()
        {
            return $"Country: {Country}, Name: {Name}, HideFromOtherAnimals: {HideFromOtherAnimals}, WhatAnimal: {WhatAnimal}";
        }
    }
}
