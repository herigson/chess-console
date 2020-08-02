using System;
using board;
namespace chess
{
    public class Pawn : Piece
    {
        private ChessMatch Match;

        public Pawn(Board board, Color color, ChessMatch match) : base(board, color)
        {
            Match = match;
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
                if (Board.ValidPosition(position) && Free(position))
                    mat[position.Line, position.Column] = true;

                position.SetValues(Position.Line - 2, Position.Column);
                if (Board.ValidPosition(position) && Free(position) && NumberOfMovements == 0)
                    mat[position.Line, position.Column] = true;

                position.SetValues(Position.Line - 1, Position.Column - 1);
                if (Board.ValidPosition(position) && ThereIsOponente(position))
                    mat[position.Line, position.Column] = true;

                position.SetValues(Position.Line - 1, Position.Column + 1);
                if (Board.ValidPosition(position) && ThereIsOponente(position))
                    mat[position.Line, position.Column] = true;
                    
                //#SpecialPlay EnPassant
                if(Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);

                    if (Board.ValidPosition(left) && ThereIsOponente(left) && Board.Piece(left) == Match.VulnerableEnPassant)
                    {
                        mat[left.Line - 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Line, Position.Column + 1);

                    if (Board.ValidPosition(right) && ThereIsOponente(right) && Board.Piece(right) == Match.VulnerableEnPassant)
                    {
                        mat[right.Line - 1, right.Column] = true;
                    }
                }
            }
            else
            {
                position.SetValues(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(position) && Free(position))
                    mat[position.Line, position.Column] = true;

                position.SetValues(Position.Line + 2, Position.Column);
                if (Board.ValidPosition(position) && Free(position) && NumberOfMovements == 0)
                    mat[position.Line, position.Column] = true;

                position.SetValues(Position.Line + 1, Position.Column - 1);
                if (Board.ValidPosition(position) && ThereIsOponente(position))
                    mat[position.Line, position.Column] = true;

                position.SetValues(Position.Line + 1, Position.Column + 1);
                if (Board.ValidPosition(position) && ThereIsOponente(position))
                    mat[position.Line, position.Column] = true;

                //#SpecialPlay EnPassant
                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);

                    if (Board.ValidPosition(left) && ThereIsOponente(left) && Board.Piece(left) == Match.VulnerableEnPassant)
                    {
                        mat[left.Line + 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Line, Position.Column + 1);

                    if (Board.ValidPosition(right) && ThereIsOponente(right) && Board.Piece(right) == Match.VulnerableEnPassant)
                    {
                        mat[right.Line + 1, right.Column] = true;
                    }
                }
            }
            return mat;
        }

        public override string ToString()
        {
            return "♙";
        }
    }
}
