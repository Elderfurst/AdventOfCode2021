using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day8
    {
        private static readonly string[] Inputs = File.ReadAllLines(@"Inputs/Day8.txt");
        public static void Run()
        {
            PartOne();
            PartTwo();
        }

        private static void PartOne()
        {
            var totalCount = 0;

            foreach (var input in Inputs)
			{
                var inputSplit = input.Split(" | ");

                var rightSplit = inputSplit[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                foreach (var value in rightSplit)
				{
                    if (value.Length == 2 || value.Length == 3 || value.Length == 4 || value.Length == 7)
					{
                        totalCount++;
					}
				}
			}

            Console.WriteLine(totalCount);
        }


        private static void PartTwo()
        {
            var total = 0;

            foreach (var input in Inputs)
            {
                var inputSplit = input.Split(" | ");

                var leftSplit = inputSplit[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var rightSplit = inputSplit[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                var entries = new Dictionary<int, string>();

                while (entries.Count < leftSplit.Length)
				{
                    foreach (var scramble in leftSplit)
					{
                        var sortedScramble = new string(scramble.OrderBy(x => x).ToArray());

                        if (!entries.ContainsKey(1) && scramble.Length == 2)
                        {
                            entries[1] = sortedScramble;
                        }
                        else if (!entries.ContainsKey(7) && scramble.Length == 3)
                        {
                            entries[7] = sortedScramble;
                        }
                        else if (!entries.ContainsKey(4) && scramble.Length == 4)
                        {
                            entries[4] = sortedScramble;
                        }
                        else if (!entries.ContainsKey(5)
                            && scramble.Length == 5
                            && entries.ContainsKey(6)
                            && entries[6].Except(scramble).Count() == 1)
						{
                            entries[5] = sortedScramble;
						}
                        else if (!entries.ContainsKey(3)
                            && scramble.Length == 5
                            && entries.ContainsKey(5)
                            && entries[5].Except(scramble).Count() == 1)
						{
                            entries[3] = sortedScramble;
						}
                        else if (!entries.ContainsKey(2)
                            && scramble.Length == 5
                            && entries.ContainsKey(5)
                            && entries[5].Except(scramble).Count() == 2)
						{
                            entries[2] = sortedScramble;
						}
                        else if (!entries.ContainsKey(0)
                            && scramble.Length == 6
                            && entries.ContainsKey(4)
                            && entries.ContainsKey(7)
                            && scramble.Except(entries[4]).Count() == 3
                            && scramble.Except(entries[7]).Count() == 3)
                        {
                            entries[0] = sortedScramble;
                        }
                        else if (!entries.ContainsKey(9)
                            && scramble.Length == 6
                            && entries.ContainsKey(4)
                            && scramble.Except(entries[4]).Count() == 2)
                        {
                            entries[9] = sortedScramble;
						}
                        else if (!entries.ContainsKey(6)
                            && scramble.Length == 6
                            && entries.ContainsKey(1)
                            && entries.ContainsKey(8)
                            && entries.ContainsKey(9)
                            && entries[1].Except(scramble).Count() == 1
                            && entries[8].Except(scramble).Count() == 1
                            && entries[9].Except(scramble).Any())
						{
                            entries[6] = sortedScramble;
						}
                        else if (!entries.ContainsKey(8) && scramble.Length == 7)
						{
                            entries[8] = sortedScramble;
						}
					}
				}

                var value = "";

                foreach (var output in rightSplit)
				{
                    var sortedOutput = new string(output.OrderBy(x => x).ToArray());

                    value += entries.First(x => x.Value == sortedOutput).Key;
				}

                var parsedValue = int.Parse(value);

                total += parsedValue;
            }

            Console.WriteLine(total);
        }
    }
}
