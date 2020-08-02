using System;
using board;
namespace chess
{
    public class Pawn : Piece
    {

        public Pawn(Board board, Color color) : base(board, color)
        {

        }

        private bool ThereIsOponente(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece.Color != Color;

        }

        private bool Free(Position position)
        {
            return Board.Piece(position) == null;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position position = new Position(0, 0);

            if (Color == Color.White)
            {

                position.SetValues(Position.Line - 1, Position.Column);
                if (Board.ValidPosition(position) && Free(Position))
                    mat[position.Line, position.Column] = true;

                position.SetValues(Position.Line - 2, Position.Column);
                if (Board.ValidPosition(position) && Free(Position) && NumberOfMovements == 0)
                    mat[position.Line, position.Column] = true;

                position.SetValues(Position.Line - 1, Position.Column - 1);
                if (Board.ValidPosition(position) && ThereIsOponente(position))
                    mat[position.Line, position.Column] = true;

                position.SetValues(Position.Line - 1, Position.Column + 1);
                if (Board.ValidPosition(position) && ThereIsOponente(position))
                    mat[position.Line, position.Column] = true;
            }
            else
            {
                position.SetValues(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(position) && Free(Position))
                    mat[position.Line, position.Column] = true;

                position.SetValues(Position.Line + 2, Position.Column);
                if (Board.ValidPosition(position) && Free(Position) && NumberOfMovements == 0)
                    mat[position.Line, position.Column] = true;

                position.SetValues(Position.Line + 1, Position.Column - 1);
                if (Board.ValidPosition(position) && ThereIsOponente(position))
                    mat[position.Line, position.Column] = true;

                position.SetValues(Position.Line + 1, Position.Column + 1);
                if (Board.ValidPosition(position) && ThereIsOponente(position))
                    mat[position.Line, position.Column] = true;
            }
            return mat;
        }

        public override string ToString()
        {
            return "♙";
        }
    }
}
