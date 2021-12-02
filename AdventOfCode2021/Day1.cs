using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day1
    {
        private static readonly int[] Inputs = File.ReadAllLines(@"Inputs/Day1.txt").Select(int.Parse).ToArray();
        public static void Run()
        {
			PartOne();
            PartTwo();
        }

        private static void PartOne()
		{
            var totalIncreases = 0;

            for (var i = 1; i < Inputs.Length; i++)
			{
                if (Inputs[i - 1] < Inputs[i])
				{
                    totalIncreases++;
				}
			}

            Console.WriteLine(totalIncreases);
		}

        private static void PartTwo()
		{
            var totalIncreases = 0;

            var index = 0;

            while (true)
			{
                if (index + 3 >= Inputs.Length)
				{
                    break;
				}

                var oneOne = Inputs[index];
                var oneTwo = Inputs[index + 1];
                var oneThree = Inputs[index + 2];

                var twoOne = Inputs[index + 1];
                var twoTwo = Inputs[index + 2];
                var twoThree = Inputs[index + 3];

                var oneSum = oneOne + oneTwo + oneThree;
                var twoSum = twoOne + twoTwo + twoThree;

                if (oneSum < twoSum)
				{
                    totalIncreases++;
				}

                index++;
			}

            Console.WriteLine(totalIncreases);
		}
    }
}
