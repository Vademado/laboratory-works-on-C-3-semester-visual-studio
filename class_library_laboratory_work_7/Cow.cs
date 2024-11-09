using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class_library_laboratory_work_7
{
    [Comment("Cow")]
    public class Cow : Animal
    {
        public override eClassificationAnimal WhatAnimal { get => eClassificationAnimal.Herbivores; }

        public Cow(string country, string name, bool hideFromOtherAnimals = true) : base(country, name, hideFromOtherAnimals) { }
        public override void Deconstruct()
        {
            Console.WriteLine("The cow is destroyed");
        }

        public override eFavoriteFood GetFavouriteFood()
        {
            return eFavoriteFood.Plants;
        }

        public override void SayHello()
        {
            Console.WriteLine("Greetings from the cow");
        }
    }
}
