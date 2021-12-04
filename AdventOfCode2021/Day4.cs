using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day4
    {
        public static void Run()
        {
            PartOne();
            PartTwo();
        }

        private static void PartOne()
        {
            var bingoGame = ParseInput();

            var winningBoard = new BingoBoard();
            var winningIndex = 500;

            foreach (var bingoBoard in bingoGame.Boards)
			{
                for (var i = 0; i < bingoGame.DrawnNumbers.Count(); i++)
				{
                    var currentNumber = bingoGame.DrawnNumbers.ElementAt(i);

                    MarkBoard(bingoBoard, currentNumber);

                    if (CheckBoard(bingoBoard))
					{
                        if (i < winningIndex)
						{
                            winningIndex = i;
                            winningBoard = bingoBoard;
                        }
                        break;
					}
                }
			}

            var winningScore = 0;

            for (var i = 0; i < winningBoard.Board.GetLength(0); i++)
			{
                for (var j = 0; j < winningBoard.Board.GetLength(1); j++)
				{
                    var currentSquare = winningBoard.Board[i, j];

                    if (!currentSquare.Marked)
					{
                        winningScore += currentSquare.Number;
					}
				}
			}

            var winningNumber = bingoGame.DrawnNumbers.ElementAt(winningIndex);

            Console.WriteLine(winningScore * winningNumber);
        }

        private static void PartTwo()
        {
            var bingoGame = ParseInput();

            var winningBoard = new BingoBoard();
            var winningIndex = -1;

            foreach (var bingoBoard in bingoGame.Boards)
            {
                for (var i = 0; i < bingoGame.DrawnNumbers.Count(); i++)
                {
                    var currentNumber = bingoGame.DrawnNumbers.ElementAt(i);

                    MarkBoard(bingoBoard, currentNumber);

                    if (CheckBoard(bingoBoard))
                    {
                        if (i > winningIndex)
						{
                            winningIndex = i;
                            winningBoard = bingoBoard;
                        }
                        break;
                    }
                }
            }

            var winningScore = 0;

            for (var i = 0; i < winningBoard.Board.GetLength(0); i++)
            {
                for (var j = 0; j < winningBoard.Board.GetLength(1); j++)
                {
                    var currentSquare = winningBoard.Board[i, j];

                    if (!currentSquare.Marked)
                    {
                        winningScore += currentSquare.Number;
                    }
                }
            }

            var winningNumber = bingoGame.DrawnNumbers.ElementAt(winningIndex);

            Console.WriteLine(winningScore * winningNumber);
        }

        private static BingoGame ParseInput()
		{
            var bingoGame = new BingoGame();

            var rawInput = File.ReadAllLines(@"Inputs/Day4.txt");

            var drawnNumbers = rawInput[0].Split(",").Select(int.Parse);

            bingoGame.DrawnNumbers = drawnNumbers;

            var currentBoard = new BingoBoard();
            var currentBoardRowCounter = 0;

            for (var i = 2; i < rawInput.Length; i++)
			{
                var currentInput = rawInput[i];

                if (currentInput == "")
				{
                    bingoGame.Boards.Add(currentBoard);
                    currentBoard = new BingoBoard();
                    currentBoardRowCounter = 0;
				}
                else
				{
                    var numberRow = currentInput.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                    
                    for (var j = 0; j < numberRow.Length; j++)
					{
                        var bingoSquare = new BingoSquare
                        {
                            Number = numberRow[j],
                            Marked = false,
                        };

                        currentBoard.Board[currentBoardRowCounter, j] = bingoSquare;
					}

                    currentBoardRowCounter++;
				}
			}

            // Add the last board because it doesn't get added in the for loop, because there's no last empty row
            bingoGame.Boards.Add(currentBoard);

            return bingoGame;
		}

        private static void MarkBoard(BingoBoard board, int number)
		{
            for (var j = 0; j < board.Board.GetLength(0); j++)
            {
                for (var k = 0; k < board.Board.GetLength(1); k++)
                {
                    var currentSquare = board.Board[j, k];

                    if (currentSquare.Number == number)
                    {
                        currentSquare.Marked = true;

                        return;
                    }
                }
            }
        }

        private static bool CheckBoard(BingoBoard board)
		{
            var length = board.Board.GetLength(0);

            for (var i = 0; i < length; i++)
			{
                var row = GetRow(board, i);
                var column = GetColumn(board, i);

                var rowCount = GetRow(board, i).Where(x => x.Marked).Count();
                var columnCount = GetColumn(board, i).Where(x => x.Marked).Count();

                if (rowCount == length || columnCount == length)
				{
                    return true;
				}
			}

            return false;
		}

        private static BingoSquare[] GetRow(BingoBoard board, int rowNumber)
		{
            return Enumerable.Range(0, board.Board.GetLength(1)).Select(x => board.Board[rowNumber, x]).ToArray();
		}

        private static BingoSquare[] GetColumn(BingoBoard board, int columnNumber)
		{
            return Enumerable.Range(0, board.Board.GetLength(0)).Select(x => board.Board[x, columnNumber]).ToArray();
        }

        class BingoSquare
		{
            public int Number { get; set; }
            public bool Marked { get; set; }
		}

        class BingoBoard
        {
            public BingoSquare[,] Board { get; set; } = new BingoSquare[5,5];
        }

        class BingoGame
		{
            public IEnumerable<int> DrawnNumbers { get; set; }
            public List<BingoBoard> Boards { get; set; } = new List<BingoBoard>();
		}
    }
}
