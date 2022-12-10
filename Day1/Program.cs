using System;
using System.IO;
using System.Collections.Generic;

namespace dev
{
    class Food
    {
        public int Calories { get; set; }
    }

    class Elf
    {
        public int TotalCalories { get; set; }

        public List<Food> FoodList {get;set;} = new List<Food>();
        public Elf()
        {
            TotalCalories = 0;
        }

        public void AddToCalories(int caloriesToAdd)
        {
            FoodList.Add(new Food(){Calories = caloriesToAdd});
            TotalCalories += caloriesToAdd;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Elf> elves = new List<Elf>();
            var currentElf = new Elf();

            // take user input
            var filename = "input.txt";
            foreach(var calory in File.ReadLines(filename))
            {
                if(string.IsNullOrWhiteSpace(calory))
                {
                    elves.Add(currentElf);
                    currentElf = new Elf();
                }
                else
                {
                    var caloryAsInt = Convert.ToInt32(calory);
                    currentElf.AddToCalories(caloryAsInt);
                }
            }

            Console.WriteLine($"There are {elves.Count} elves!");

            var highestCalories = 0;
            var fattestElf = new Elf();
            // find answer
            foreach(var elf in elves)
            {
                if (elf.TotalCalories > highestCalories)
                {
                    highestCalories = elf.TotalCalories;
                    fattestElf = elf;
                }
            }
            Console.WriteLine($"The fattest elf has {fattestElf.FoodList.Count} foods.");
            Console.WriteLine($"Highest calories: {highestCalories}");
        }
    }
}
