using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class_library_laboratory_work_7
{
    [Comment("Pig")]
    public class Pig : Animal
    {
        public override eClassificationAnimal WhatAnimal { get => eClassificationAnimal.Omnivores; }

        public Pig(string country, string name, bool hideFromOtherAnimals = true) : base(country, name, hideFromOtherAnimals) { }
        public override void Deconstruct()
        {
            Console.WriteLine("The pig is destroyed");
        }

        public override eFavoriteFood GetFavouriteFood()
        {
            return eFavoriteFood.Everything;
        }

        public override void SayHello()
        {
            Console.WriteLine("Greetings from the pig");
        }
    }
}
