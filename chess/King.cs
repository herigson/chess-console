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

            //Acima
            position.SetValues(position.Line - 1, position.Column);
            if (Board.ValidPosition(position) && CanMove(position))
                mat[position.Line, position.Column] = true;

            //Nordeste
            position.SetValues(position.Line - 1, position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
                mat[position.Line, position.Column] = true;
            //direita
            position.SetValues(position.Line, position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
                mat[position.Line, position.Column] = true;

            //Sudeste
            position.SetValues(position.Line + 1, position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
                mat[position.Line, position.Column] = true;

            //Abaixo
            position.SetValues(position.Line + 1, position.Column);
            if (Board.ValidPosition(position) && CanMove(position))
                mat[position.Line, position.Column] = true;

            //Sudoeste
            position.SetValues(position.Line + 1, position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
                mat[position.Line, position.Column] = true;

            //Esquerda
            position.SetValues(position.Line , position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
                mat[position.Line, position.Column] = true;

            //Noroeste
            position.SetValues(position.Line -1, position.Column - 1);
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
