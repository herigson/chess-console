using System;
using board;
using chess;

namespace chess_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try { 
            Board board = new Board(8, 8);

            board.InsertPiece(new Rook(board, Color.Black), new Position(0, 0));
            board.InsertPiece(new Rook(board, Color.Black), new Position(1, 3));
            board.InsertPiece(new King(board, Color.Black), new Position(2, 4));
            board.InsertPiece(new King(board, Color.White), new Position(0, 5));
            Screen.Print(board);
            }
            catch(BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
