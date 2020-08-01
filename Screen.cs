using System;
using board;
using chess;
using System.Collections.Generic;

namespace chess_console
{
    public class Screen
    {

        public static void PrintMatch(ChessMatch match)
        {
            SetConsoleForegroundColorToWhitePieces();
            PrintBoard(match.Board);
            PrintCapturedPieces(match);

            Console.WriteLine();
            Console.WriteLine("Shift: " + match.Shift);
            Console.WriteLine("Awaiting move: " + match.CurrentPlayer);

        }

        public static void PrintCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured pieces:");
            Console.Write("White: ");
            PrintSet(match.CapturedPieces(Color.White));
            Console.WriteLine();
            Console.Write("Black: ");
            SetConsoleForegroundColorToBlackPieces();
            PrintSet(match.CapturedPieces(Color.Black));
            SetConsoleForegroundColorToWhitePieces();
            Console.WriteLine();
        }

        public static void PrintSet(HashSet<Piece> set)
        {
            Console.Write("[");
            foreach(Piece piece in set)
                Console.Write(piece + " ");
            Console.Write("]");
        }

        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");

                for (int j = 0; j < board.Columns; j++)
                    PrintPiece(board.Piece(i, j));

                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void SetConsoleForegroundColorToBlackPieces()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        public static void SetConsoleForegroundColorToWhitePieces()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintBoard(Board board, bool[,] PossiblePositions)
        {
            ConsoleColor defaultBackgroundColor = Console.BackgroundColor;
            ConsoleColor backgroundColorChanged = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (PossiblePositions[i, j])
                        Console.BackgroundColor = backgroundColorChanged;
                    else
                        Console.BackgroundColor = defaultBackgroundColor;

                    PrintPiece(board.Piece(i, j));
                    Console.BackgroundColor = defaultBackgroundColor;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = defaultBackgroundColor;
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor defaultColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = defaultColor;
                }
                Console.Write(" ");
            }
        }

        public static ChessPosition ReadChessPosition()
        {
            string inputUser = Console.ReadLine().ToLower();
            int line = int.Parse(inputUser[1] + "");
            char column = inputUser[0];

            return new ChessPosition(column, line);
        }
    }
}
