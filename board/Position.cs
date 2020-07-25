using System;
namespace chess_console.board
{
    public class Position
    {
        public int Line { get; set; }
        public int Column { get; set; }

        public Position(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public override string ToString()
        {
            return Line
                   + ", "
                   + Column;
        }
    }
}
