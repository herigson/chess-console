using System;
using board;
namespace chess
{
    public class Knight : Piece
    {

        public Knight(Board board, Color color) : base(board, color)
        {

        }

        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;

        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position position = new Position(0, 0);

            //UpRight
            position.SetValues(Position.Line - 1, Position.Column + 2);
            if (Board.ValidPosition(position) && CanMove(position))
                mat[position.Line, position.Column] = true;
            position.SetValues(Position.Line - 2, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
                mat[position.Line, position.Column] = true;

            //UpLeft
            position.SetValues(Position.Line - 1, Position.Column - 2);
            if (Board.ValidPosition(position) && CanMove(position))
                mat[position.Line, position.Column] = true;
            position.SetValues(Position.Line - 2, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
                mat[position.Line, position.Column] = true;
                
            //DownRight
            position.SetValues(Position.Line + 2, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
                mat[position.Line, position.Column] = true;
            position.SetValues(Position.Line + 1, Position.Column + 2);
            if (Board.ValidPosition(position) && CanMove(position))
                mat[position.Line, position.Column] = true;

            //DownLeft
            position.SetValues(Position.Line + 2, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
                mat[position.Line, position.Column] = true;
            position.SetValues(Position.Line + 1, Position.Column - 2);
            if (Board.ValidPosition(position) && CanMove(position))
                mat[position.Line, position.Column] = true;

            return mat;
        }

        public override string ToString()
        {
            return "♞";
        }
    }
}
