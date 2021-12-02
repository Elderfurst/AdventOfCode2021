using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day2
    {
        private static readonly string[] Inputs = File.ReadAllLines(@"Inputs/Day2.txt").ToArray();
        public static void Run()
        {
            var instructions = ParseInput();
            PartOne(instructions);
            PartTwo(instructions);
        }

        private static void PartOne(IEnumerable<Instruction> instructions)
        {
            var horizontal = 0;
            var depth = 0;

            foreach (var instruction in instructions)
			{
                switch (instruction.Direction)
                {
                    case "forward":
                        horizontal += instruction.Distance;
                        break;
                    case "down":
                        depth += instruction.Distance;
                        break;
                    case "up":
                        depth -= instruction.Distance;
                        break;
                }                    
			}

            Console.WriteLine(horizontal * depth);
        }

        private static void PartTwo(IEnumerable<Instruction> instructions)
        {
            var horizontal = 0;
            var depth = 0;
            var aim = 0;

            foreach (var instruction in instructions)
            {
                switch (instruction.Direction)
                {
                    case "forward":
                        horizontal += instruction.Distance;
                        depth += aim * instruction.Distance;
                        break;
                    case "down":
                        aim += instruction.Distance;
                        break;
                    case "up":
                        aim -= instruction.Distance;
                        break;
                }
            }

            Console.WriteLine(horizontal * depth);
        }

        private static IEnumerable<Instruction> ParseInput()
		{
            var instructions = new List<Instruction>();

            foreach (var input in Inputs)
			{
                var splitString = input.Split(" ");

                var direction = splitString[0];
                var distance = int.Parse(splitString[1]);

                var instruction = new Instruction(direction, distance);

                instructions.Add(instruction);
			}

            return instructions;
		}
    }

    class Instruction
	{
        public string Direction;
        public int Distance;

        public Instruction(string direction, int distance)
		{
            Direction = direction;
            Distance = distance;
		}
	}
}
