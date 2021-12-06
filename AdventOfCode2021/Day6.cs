using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day6
    {
        private static readonly int[] Inputs = File.ReadAllText(@"Inputs/Day6.txt").Split(",").Select(int.Parse).ToArray();
        public static void Run()
        {
            PartOne();
            PartTwo();
        }

        private static void PartOne()
        {
            var lanternfish = new List<int>();

            foreach (var input in Inputs)
			{
                lanternfish.Add(input);
			}

            for (var i = 0; i < 80; i++)
			{
                var newLanternfish = new List<int>();

                for (var j = 0; j < lanternfish.Count; j++)
				{
                    var currentFish = lanternfish[j];

                    if (currentFish > 0)
					{
                        currentFish--;
                        newLanternfish.Add(currentFish);
					}
                    else if (currentFish == 0)
					{
                        currentFish = 6;
                        newLanternfish.Add(currentFish);
                        newLanternfish.Add(8);
					}
				}

                lanternfish = newLanternfish;
			}

            Console.WriteLine(lanternfish.Count);
        }


        private static void PartTwo()
		{
            var fish = new long[9];

            for (var i = 0; i < Inputs.Length; i++)
			{
                fish[Inputs[i]]++;
            }                

            for (var i = 0; i < 256; i++)
            {
                var oldFish = fish[0];
                fish[0] = fish[1];
                fish[1] = fish[2];
                fish[2] = fish[3];
                fish[3] = fish[4];
                fish[4] = fish[5];
                fish[5] = fish[6];
                fish[6] = fish[7] + oldFish;
                fish[7] = fish[8];
                fish[8] = oldFish;
            }

            long answer = 0;
            for (var i = 0; i < 9; i++)
            {
                answer += fish[i];
            }

            Console.WriteLine(answer);
        }

        private static void PartTwoSlow()
        {
            long totalFish = Inputs.Length;

            foreach (var input in Inputs)
            {
                totalFish += CalculateFish(256, input);
            }

            Console.WriteLine(totalFish);
        }

        private static int CalculateFish(int daysLeft, int value)
		{
            var output = 0;

            var firstFishDay = value + 1;

            if (firstFishDay <= daysLeft)
			{
                output++;
			}

            var remainingOutput = (daysLeft - firstFishDay) / 7;

            output += remainingOutput;

            if (output <= 0)
			{
                return 0;
			}
            else
			{
                var fishOutput = output;

                var newFish = value + 1;

                while (newFish < daysLeft)
				{
                    fishOutput += CalculateFish(daysLeft - newFish, 8);

                    newFish += 7;
				}

                return fishOutput;
			}
		}
    }
}
