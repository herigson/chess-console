using System;
using board;

namespace chess
{
    public class King : Piece
    {
        public King(Board board, Color color) : base(board, color)
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

            //Up
            position.SetValues(Position.Line - 1, Position.Column);
            if (Board.ValidPosition(position) && CanMove(position))
                mat[position.Line, position.Column] = true;

            //Northeast
            position.SetValues(Position.Line - 1, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
                mat[position.Line, position.Column] = true;
            //Right
            position.SetValues(Position.Line, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
                mat[position.Line, position.Column] = true;

            //Southeast
            position.SetValues(Position.Line + 1, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
                mat[position.Line, position.Column] = true;

            //Down
            position.SetValues(Position.Line + 1, Position.Column);
            if (Board.ValidPosition(position) && CanMove(position))
                mat[position.Line, position.Column] = true;

            //South-west
            position.SetValues(Position.Line + 1, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
                mat[position.Line, position.Column] = true;

            //Left
            position.SetValues(Position.Line , Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
                mat[position.Line, position.Column] = true;

            //Northwest
            position.SetValues(Position.Line -1, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
                mat[position.Line, position.Column] = true;

            return mat;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
