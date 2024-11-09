using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class_library_laboratory_work_7
{
    [Comment("Lion")]
    public class Lion : Animal
    {
        public override eClassificationAnimal WhatAnimal { get => eClassificationAnimal.Carnivores; }

        public Lion(string country, string name, bool hideFromOtherAnimals = false) : base(country, name, hideFromOtherAnimals) { }
        public override void Deconstruct()
        {
            Console.WriteLine("The lion is destroyed");
        }

        public override eFavoriteFood GetFavouriteFood()
        {
            return eFavoriteFood.Meat;
        }

        public override void SayHello()
        {
            Console.WriteLine("Greetings from the lion");
        }
    }
}
