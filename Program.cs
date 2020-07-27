using System;
using board;
using chess;

namespace chess_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch match = new ChessMatch();

                while (!match.Finished)
                {
                    try
                    {
                        Console.Clear();
                        Screen.PrintBoard(match.Board);
                        Console.WriteLine();
                        Console.WriteLine("Shift: " + match.Shift); 
                        Console.WriteLine("Awaiting move: " + match.CurrentPlayer);

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        match.ValidateOriginPosition(origin);

                        bool[,] possiblePositions = match.Board.Piece(origin).PossibleMovements();

                        Console.Clear();
                        Screen.PrintBoard(match.Board, possiblePositions);

                        Console.WriteLine();
                        Console.Write("Destiny: ");
                        Position destiny = Screen.ReadChessPosition().ToPosition();
                        match.ValidateDestinyPosition(origin, destiny);

                        match.MakeAPlay(origin, destiny);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
