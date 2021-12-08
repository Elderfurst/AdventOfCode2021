using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day7
    {
        private static readonly int[] Inputs = File.ReadAllText(@"Inputs/Day7.txt").Split(",").Select(int.Parse).ToArray();
        public static void Run()
        {
            PartOne();
            PartTwo();
        }

        private static void PartOne()
        {
            var max = 0;

            foreach (var input in Inputs)
			{
                if (input > max)
				{
                    max = input;
				}
			}

            var minFuelCost = int.MaxValue;

            for (var i = 0; i <= max; i++)
			{
                var totalFuelCost = 0;

                foreach (var input in Inputs)
				{
                    var difference = Math.Abs(input - i);

                    totalFuelCost += difference;
				}

                if (totalFuelCost < minFuelCost)
				{
                    minFuelCost = totalFuelCost;
				}
			}

            Console.WriteLine(minFuelCost);
        }


        private static void PartTwo()
        {
            var max = 0;

            foreach (var input in Inputs)
            {
                if (input > max)
                {
                    max = input;
                }
            }

            var minFuelCost = int.MaxValue;

            for (var i = 0; i <= max; i++)
            {
                var totalFuelCost = 0;

                foreach (var input in Inputs)
                {
                    var difference = Math.Abs(input - i);

                    var increasingFuelCost = (difference * (difference + 1)) / 2;

                    totalFuelCost += increasingFuelCost;
                }

                if (totalFuelCost < minFuelCost)
                {
                    minFuelCost = totalFuelCost;
                }
            }

            Console.WriteLine(minFuelCost);
        }
    }
}
