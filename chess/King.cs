using System;
using board;

namespace chess
{
    public class King : Piece
    {
        private ChessMatch Match;

        public King(Board board, Color color, ChessMatch match) : base(board, color)
        {
            Match = match;
        }

        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;

        }

        private bool TestRookToCastling(Position position)
        {
            Piece piece = Board.Piece(position);

            return piece != null && piece is Rook && piece.Color == Color && piece.NumberOfMovements == 0;
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


            //#Special play Castling
            if(NumberOfMovements == 0 && !Match.Check)
            {
                //#Special play castling short
                Position positionRook = new Position(Position.Line, Position.Column + 3);
                if (TestRookToCastling(positionRook))
                {
                    Position kingMoreOne = new Position(Position.Line, Position.Column + 1);
                    Position kingMoreTwo = new Position(Position.Line, Position.Column + 2);

                    if(Board.Piece(kingMoreOne) == null && Board.Piece(kingMoreTwo) == null)
                    {
                        mat[Position.Line, Position.Column + 2] = true;
                    }
                }

                //#Special play castling long
                Position positionRook2 = new Position(Position.Line, Position.Column + 3);
                if (TestRookToCastling(positionRook))
                {
                    Position kingOneLess = new Position(Position.Line, Position.Column - 1);
                    Position kingTwoLess = new Position(Position.Line, Position.Column - 2);
                    Position kingThreeLess = new Position(Position.Line, Position.Column - 3);

                    if (Board.Piece(kingOneLess) == null && Board.Piece(kingTwoLess) == null  && Board.Piece(kingThreeLess) == null)
                    {
                        mat[Position.Line, Position.Column - 2] = true;
                    }
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return "♔";
        }
    }
}
