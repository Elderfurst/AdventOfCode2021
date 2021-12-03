using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day3
    {
        private static readonly string[] Inputs = File.ReadAllLines(@"Inputs/Day3.txt").ToArray();
        public static void Run()
        {
            PartOne();
            PartTwo();
        }

        private static void PartOne()
        {
            var counts = GetCounts(Inputs, '1');

            var totalInput = Inputs.Length;

            var gamma = "";
            var epsilon = "";

            foreach (var count in counts)
			{
                if (count > (totalInput / 2))
				{
                    gamma += "1";
                    epsilon += "0";
				}
                else
				{
                    gamma += "0";
                    epsilon += "1";
				}
			}

            var gammaNumber = Convert.ToInt32(gamma, 2);
            var epsilonNumber = Convert.ToInt32(epsilon, 2);

            Console.WriteLine(gammaNumber * epsilonNumber);
        }

        private static void PartTwo()
        {
            var oxygenStrings = new List<string>();
            var scrubberStrings = new List<string>();

            foreach (var input in Inputs)
            {
                oxygenStrings.Add(input);
                scrubberStrings.Add(input);
            }

            var oxygenCounter = 0;

            while (oxygenStrings.Count > 1)
			{
                var newOxygenStrings = new List<string>();

                var counts = GetCounts(oxygenStrings.ToArray(), '1');
                var half = oxygenStrings.Count / 2.0;

                var currentCount = counts[oxygenCounter];
                var expectedLetter = (currentCount >= half) ? '1' : '0';

                foreach (var oxygenString in oxygenStrings)
				{
                    var currentLetter = oxygenString[oxygenCounter];                    

                    if (currentLetter == expectedLetter)
					{
                        newOxygenStrings.Add(oxygenString);
					}
				}

                oxygenCounter++;
                oxygenStrings = newOxygenStrings;                
			}

            var scrubberCounter = 0;

            while (scrubberStrings.Count > 1)
            {
                var newScrubberStrings = new List<string>();

                var counts = GetCounts(scrubberStrings.ToArray(), '0');
                var half = scrubberStrings.Count / 2.0;

                var currentCount = counts[scrubberCounter];
                var expectedLetter = (currentCount <= half) ? '0' : '1';

                foreach (var scrubberString in scrubberStrings)
                {
                    var currentLetter = scrubberString[scrubberCounter];

                    if (currentLetter == expectedLetter)
                    {
                        newScrubberStrings.Add(scrubberString);
                    }
                }

                scrubberCounter++;
                scrubberStrings = newScrubberStrings;
            }

            var oxygenNumber = Convert.ToInt32(oxygenStrings[0], 2);
            var scrubberNumber = Convert.ToInt32(scrubberStrings[0], 2);

            Console.WriteLine(oxygenNumber * scrubberNumber);
        }

        private static int[] GetCounts(string[] inputs, char needle)
		{
            var inputLength = inputs[0].Length;

            var counts = new int[inputLength];

            foreach (var input in inputs)
            {
                for (var i = 0; i < input.Length; i++)
                {
                    var letter = input[i];

                    if (letter == needle)
					{
                        counts[i]++;
					}
                }
            }

            return counts;
        }
    }
}
