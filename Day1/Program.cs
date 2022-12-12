using System;
using System.IO;
using System.Collections.Generic;

namespace dev
{
    public class Food
    {
        public int Calories { get; set; }
    }

    public class Elf
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

    public class TopThreeTracker
    {
        private const int NUM_FAT_ELVES = 3;

        Elf[] FattestElves {get;set;} = new Elf[NUM_FAT_ELVES];

        public bool TryAddElf(Elf elf)
        {
            int index = GetIndexForElf(elf);
            if (index != -1)
            {
                AddElf(elf, index);
                return true;
            }

            return false;
        }

        public int GetIndexForElf(Elf elf)
        {
            for(int i = 0; i < NUM_FAT_ELVES; i++)
            {
                if(FattestElves[i] == null || elf.TotalCalories > FattestElves[i].TotalCalories)
                {
                    return i;
                }
            }
            return -1;
        }

        public void AddElf(Elf elfToAdd, int indexToAdd)
        {
            for(int i = indexToAdd; i < NUM_FAT_ELVES; i++)
            {
                var curr = FattestElves[i];
                FattestElves[i] = elfToAdd;
                elfToAdd = curr;
            }
        }

        public int GetTopThreeTotal()
        {
            return FattestElves[0].TotalCalories + FattestElves[1].TotalCalories + FattestElves[2].TotalCalories;
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

            //CalculatePartOne(elves);

            CalculatePartTwo(elves);

        }

        #region Part One

        static void CalculatePartOne(List<Elf> elves)
        {
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

        #endregion
    
        #region Part Two

        static void CalculatePartTwo(List<Elf> elves)
        {
            var calculator = new TopThreeTracker();

            foreach(var elf in elves)
            {
                calculator.TryAddElf(elf);
            }

            var total = calculator.GetTopThreeTotal();
            Console.WriteLine($"Total calories for top three fattest elves(!!): {total}");
        }

        #endregion
    } 
}
