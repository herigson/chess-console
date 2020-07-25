using System;
using board;
using chess;

namespace chess_console
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessPosition pos = new ChessPosition('a', 1);

            Console.WriteLine(pos);
            Console.WriteLine(pos.ToPosition());

        }
    }
}
