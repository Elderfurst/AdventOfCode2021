using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day9
    {
        public static void Run()
        {
            PartOne();
            PartTwo();
        }

        private static void PartOne()
        {
            var input = ParseInput();

            var riskSum = 0;

            for (var i = 0; i < input.Count; i++)
			{
                for (var j = 0; j < input[i].Count; j++)
				{
                    var current = input[i][j];

                    if (i - 1 >= 0)
					{
                        var up = input[i - 1][j];

                        if (up <= current)
						{
                            continue;
						}
					}
                    if (j - 1 >= 0)
					{
                        var left = input[i][j - 1];

                        if (left <= current)
						{
                            continue;
						}
					}
                    if (j + 1 < input[i].Count)
                    {
                        var right = input[i][j + 1];

                        if (right <= current)
						{
                            continue;
						}
                    }
                    if (i + 1 < input.Count)
                    {
                        var down = input[i + 1][j];

                        if (down <= current)
						{
                            continue;
						}
                    }

                    riskSum += (1 + current);
				}
			}

            Console.WriteLine(riskSum);
        }


        private static void PartTwo()
        {
            var input = ParseInput();

            var basins = new List<int>();
            for (var i = 0; i < input.Count; i++)
            {
                for (var j = 0; j < input[i].Count; j++)
                {
                    if (input[i][j] == 9)
                    {
                        continue;
                    }
                    basins.Add(FindBasinSize(input, i, j));
                }
            }

            var basinArray = basins.ToArray();
            Array.Sort(basinArray);
            Array.Reverse(basinArray);
            Console.WriteLine(basinArray[0] * basinArray[1] * basinArray[2]);
        }

        static int FindBasinSize(List<List<int>> map, int i, int j)
        {
            if (i < 0 || i >= map.Count || j < 0 || j >= map[0].Count || map[i][j] == 9)
            {
                return 0;
            }
            map[i][j] = 9;
            return 1 + FindBasinSize(map, i - 1, j) + FindBasinSize(map, i + 1, j) + FindBasinSize(map, i, j - 1) + FindBasinSize(map, i, j + 1);
        }

        private static List<List<int>> ParseInput()
		{
            var list = new List<List<int>>();

            var inputs = File.ReadAllLines(@"Inputs/Day9.txt");

            foreach (var input in inputs)
			{
                var numbers = input.ToArray().Select(x => int.Parse(x.ToString())).ToList();

                list.Add(numbers);
			}

            return list;
        }
    }
}
