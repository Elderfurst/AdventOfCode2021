using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day5
    {
        private static readonly string[] Inputs = File.ReadAllLines(@"Inputs/Day5.txt");
        public static void Run()
        {
            PartOne();
            PartTwo();
        }

        private static void PartOne()
        {
            var instructions = ParseInput(false);

            var map = new int[instructions.MaxX, instructions.MaxY];

            foreach (var line in instructions.Lines)
			{
                if (line.StartingPoint.X == line.EndingPoint.X)
				{
                    if (line.StartingPoint.Y < line.EndingPoint.Y)
					{
                        for (var i = line.StartingPoint.Y; i <= line.EndingPoint.Y; i++)
                        {
                            map[line.StartingPoint.X, i]++;
                        }
                    }
                    else
					{
                        for (var i = line.EndingPoint.Y; i <= line.StartingPoint.Y; i++)
                        {
                            map[line.StartingPoint.X, i]++;
                        }
                    }
				}
                else
				{
                    if (line.StartingPoint.X < line.EndingPoint.X)
					{
                        for (var i = line.StartingPoint.X; i <= line.EndingPoint.X; i++)
                        {
                            map[i, line.StartingPoint.Y]++;
                        }
                    }
                    else
					{
                        for (var i = line.EndingPoint.X; i <= line.StartingPoint.X; i++)
                        {
                            map[i, line.StartingPoint.Y]++;
                        }
                    }
                }
			}

            var totalOverlap = 0;

            for (var i = 0; i < instructions.MaxX; i++)
			{
                for (var j = 0; j < instructions.MaxY; j++)
				{
                    if (map[i,j] > 1)
					{
                        totalOverlap++;
					}
				}
			}

            Console.WriteLine(totalOverlap);
        }

        private static void PartTwo()
        {
            var instructions = ParseInput(true);

            var map = new int[instructions.MaxX, instructions.MaxY];

            foreach (var line in instructions.Lines)
            {
                if (line.StartingPoint.X == line.EndingPoint.X)
                {
                    if (line.StartingPoint.Y < line.EndingPoint.Y)
                    {
                        for (var i = line.StartingPoint.Y; i <= line.EndingPoint.Y; i++)
                        {
                            map[line.StartingPoint.X, i]++;
                        }
                    }
                    else
                    {
                        for (var i = line.EndingPoint.Y; i <= line.StartingPoint.Y; i++)
                        {
                            map[line.StartingPoint.X, i]++;
                        }
                    }
                }
                else if(line.StartingPoint.Y == line.EndingPoint.Y)
                {
                    if (line.StartingPoint.X < line.EndingPoint.X)
                    {
                        for (var i = line.StartingPoint.X; i <= line.EndingPoint.X; i++)
                        {
                            map[i, line.StartingPoint.Y]++;
                        }
                    }
                    else
                    {
                        for (var i = line.EndingPoint.X; i <= line.StartingPoint.X; i++)
                        {
                            map[i, line.StartingPoint.Y]++;
                        }
                    }
                }
                else
				{
                    if (line.StartingPoint.X < line.EndingPoint.X)
					{
                        if (line.StartingPoint.Y < line.EndingPoint.Y)
						{
                            var difference = line.EndingPoint.X - line.StartingPoint.X;

                            for (var i = 0; i <= difference; i++)
							{
                                map[line.StartingPoint.X + i, line.StartingPoint.Y + i]++;
							}
						}
                        else
						{
                            var difference = line.EndingPoint.X - line.StartingPoint.X;

                            for (var i = 0; i <= difference; i++)
                            {
                                map[line.StartingPoint.X + i, line.StartingPoint.Y - i]++;
                            }
                        }
					}
                    else
					{
                        if (line.StartingPoint.Y < line.EndingPoint.Y)
                        {
                            var difference = line.StartingPoint.X - line.EndingPoint.X;

                            for (var i = 0; i <= difference; i++)
                            {
                                map[line.StartingPoint.X - i, line.StartingPoint.Y + i]++;
                            }
                        }
                        else
                        {
                            var difference = line.StartingPoint.X - line.EndingPoint.X;

                            for (var i = 0; i <= difference; i++)
                            {
                                map[line.StartingPoint.X - i, line.StartingPoint.Y - i]++;
                            }
                        }
                    }
				}
            }

            var totalOverlap = 0;

            for (var i = 0; i < instructions.MaxX; i++)
            {
                for (var j = 0; j < instructions.MaxY; j++)
                {
                    if (map[i, j] > 1)
                    {
                        totalOverlap++;
                    }
                }
            }

            Console.WriteLine(totalOverlap);
        }

        private static Instructions ParseInput(bool includeDiagonals)
		{
            var lines = new List<Line>();

            var maxX = 0;
            var maxY = 0;

            foreach (var input in Inputs)
			{
                var split = input.Split(" -> ");

                var rawStart = split[0];
                var splitStart = rawStart.Split(",").Select(int.Parse).ToArray();

                var start = new Point
                {
                    X = splitStart[0],
                    Y = splitStart[1],
                };

                if (start.X > maxX)
				{
                    maxX = start.X;
				}
                if (start.Y > maxY)
				{
                    maxY = start.Y;
				}

                var rawEnd = split[1];
                var splitEnd = rawEnd.Split(",").Select(int.Parse).ToArray();

                var end = new Point
                {
                    X = splitEnd[0],
                    Y = splitEnd[1],
                };

                if (end.X > maxX)
				{
                    maxX = end.X;
				}
                if (end.Y > maxY)
				{
                    maxY = end.Y;
				}

                var line = new Line
                {
                    StartingPoint = start,
                    EndingPoint = end,
                };

                if (includeDiagonals)
				{
                    lines.Add(line);
                }
                else if (start.X == end.X || start.Y == end.Y)
				{
                    lines.Add(line);
				}
			}

            var instructions = new Instructions
            {
                MaxX = maxX + 1,
                MaxY = maxY + 1,
                Lines = lines,
            };

            return instructions;
		}

        class Instructions
		{
            public int MaxX { get; set; }
            public int MaxY { get; set; }
            public List<Line> Lines { get; set; }
		}

        class Line
		{
            public Point StartingPoint { get; set; }
            public Point EndingPoint { get; set; }
		}

        class Point
		{
            public int X { get; set; }
            public int Y { get; set; }
		}
    }
}
