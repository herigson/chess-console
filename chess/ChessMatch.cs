﻿using System;
using board;
using System.Collections.Generic;
namespace chess
{
    public class ChessMatch
    {
        public Board Board { get; private set; }
        public int Shift { get; set; }
        public Color CurrentPlayer { get; set; }
        public bool Finished { get; private set; }
        private HashSet<Piece> Pieces;
        private HashSet<Piece> Captured;
        public bool Check { get; set; }
        public Piece VulnerableEnPassant { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Shift = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
            VulnerableEnPassant = null;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            InsertPieces();
        }

        public Piece ExecuteAMovement(Position origin, Position destiny)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncrementeNumberOfMovements();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.InsertPiece(piece, destiny);
            if (capturedPiece != null)
            {
                Captured.Add(capturedPiece);
            }
            //#SpecialPlay Castling short

            if (piece is King && destiny.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column + 3);
                Position rookDestiny = new Position(origin.Line, origin.Column + 1);
                Piece rook = Board.RemovePiece(rookOrigin);
                rook.IncrementeNumberOfMovements();
                Board.InsertPiece(rook, rookDestiny);
            }

            //#SpecialPlay Castling long

            if (piece is King && destiny.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column - 4);
                Position rookDestiny = new Position(origin.Line, origin.Column - 1);
                Piece rook = Board.RemovePiece(rookOrigin);
                rook.IncrementeNumberOfMovements();
                Board.InsertPiece(rook, rookDestiny);
            }

            //#SpecialPlay EnPassant
            if (piece is Pawn)
            {
                if (origin.Column != destiny.Column && capturedPiece == null)
                {
                    Position pawnPosition;
                    if (piece.Color == Color.White)
                        pawnPosition = new Position(destiny.Line + 1, destiny.Column);
                    else
                        pawnPosition = new Position(destiny.Line - 1, destiny.Column);

                    capturedPiece = Board.RemovePiece(pawnPosition);
                    Captured.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        public void UndoTheMovement(Position origin, Position destiny, Piece capturedPice)
        {
            Piece piece = Board.RemovePiece(destiny);
            piece.DecrementNumberOfMovements();
            if (capturedPice != null)
            {
                Board.InsertPiece(capturedPice, destiny);
                Captured.Remove(capturedPice);
            }
            Board.InsertPiece(piece, origin);

            //#SpecialPlay Castling short
            if (piece is King && destiny.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column + 3);
                Position rookDestiny = new Position(origin.Line, origin.Column + 1);
                Piece rook = Board.RemovePiece(rookDestiny);
                rook.DecrementNumberOfMovements();
                Board.InsertPiece(rook, rookOrigin);
            }

            //#SpecialPlay Castling long
            if (piece is King && destiny.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column - 4);
                Position rookDestiny = new Position(origin.Line, origin.Column - 1);
                Piece rook = Board.RemovePiece(rookDestiny);
                rook.DecrementNumberOfMovements();
                Board.InsertPiece(rook, rookOrigin);
            }

            //#SpecialPlay En Passant

            if (piece is Pawn)
            {
                if (origin.Column != destiny.Column && capturedPice == VulnerableEnPassant)
                {
                    Piece pawn = Board.RemovePiece(destiny);
                    Position pawnPosition;
                    if (piece.Color == Color.White)
                    {
                        pawnPosition = new Position(3, destiny.Column);
                    }
                    else
                    {
                        pawnPosition = new Position(4, destiny.Column);
                    }

                    Board.InsertPiece(pawn, pawnPosition);
                }
            }

        }

        public void MakeAPlay(Position origin, Position destiny)
        {
            Piece capturedPiece = ExecuteAMovement(origin, destiny);


            if (ItIsInCheck(CurrentPlayer))
            {
                UndoTheMovement(origin, destiny, capturedPiece);
                throw new BoardException("You cannot put yourself in check!");
            }

            Piece piece = Board.Piece(destiny);

            // #SpecialPlay Promotion

            if ((piece.Color == Color.White && destiny.Line == 0) || (piece.Color == Color.Black && destiny.Line == 7))
            {
                piece = Board.RemovePiece(destiny);
                Pieces.Remove(piece);

                Piece queen = new Queen(Board, piece.Color);
                Board.InsertPiece(queen, destiny);
                Pieces.Add(queen);
            }

            if (ItIsInCheck(Opponent(CurrentPlayer)))
            {
                Check = true;
            }
            else
                Check = false;

            if (TestCheckMate(Opponent(CurrentPlayer)))
            {
                Finished = true;
            }
            else
            {
                Shift++;
                ChangePlayer();
            }



            //#SpecialPlay EnPassant

            if (piece is Pawn && (destiny.Line == origin.Line - 2 || destiny.Line == origin.Line + 2))
            {
                VulnerableEnPassant = piece;
            }
            else
                VulnerableEnPassant = null;

        }

        public void ValidateOriginPosition(Position position)
        {
            if (Board.Piece(position) == null)
                throw new BoardException("There is no piece in the chosen origin position!");
            if (CurrentPlayer != Board.Piece(position).Color)
                throw new BoardException("The original piece chosen is not yours!");
            if (!Board.Piece(position).ThereArePossibleMoviments())
                throw new BoardException("There are no possible movements for the chosen piece of origin!");

        }

        public void ValidateDestinyPosition(Position origin, Position destiny)
        {
            if (!Board.Piece(origin).PossibleMovement(destiny))
                throw new BoardException("Destiny position invalid!");
        }

        private void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
                CurrentPlayer = Color.Black;
            else
                CurrentPlayer = Color.White;
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> capturedPiecesPerColor = new HashSet<Piece>();
            foreach (Piece piece in Captured)
            {
                if (piece.Color == color)
                    capturedPiecesPerColor.Add(piece);
            }
            return capturedPiecesPerColor;
        }

        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> piecesInGamePerColor = new HashSet<Piece>();

            foreach (Piece piece in Pieces)
            {
                if (piece.Color == color)
                    piecesInGamePerColor.Add(piece);
            }
            piecesInGamePerColor.ExceptWith(CapturedPieces(color));

            return piecesInGamePerColor;
        }

        private Color Opponent(Color color)
        {
            if (color == Color.White)
                return Color.Black;
            else
                return Color.White;
        }

        private Piece King(Color color)
        {
            foreach (Piece piece in PiecesInGame(color))
                if (piece is King)
                    return piece;
            return null;
        }

        public bool ItIsInCheck(Color color)
        {
            Piece king = King(color);
            if (king == null)
                throw new BoardException("There is no " + color + " king on the board!");
            foreach (Piece piece in PiecesInGame(Opponent(color)))
            {
                bool[,] mat = piece.PossibleMovements();
                if (mat[king.Position.Line, king.Position.Column])
                    return true;
            }

            return false;
        }

        public bool TestCheckMate(Color color)
        {
            if (!ItIsInCheck(color))
            {
                return false;
            }

            foreach (Piece piece in PiecesInGame(color))
            {
                bool[,] mat = piece.PossibleMovements();
                for (int i = 0; i < Board.Lines; i++)
                    for (int j = 0; j < Board.Lines; j++)
                        if (mat[i, j])
                        {
                            Position origin = piece.Position;
                            Position destiny = new Position(i, j);
                            Piece capturedPice = ExecuteAMovement(origin, destiny);
                            bool testCheck = ItIsInCheck(color);
                            UndoTheMovement(origin, destiny, capturedPice);
                            if (!testCheck)
                                return false;
                        }
            }
            return true;
        }

        public void InsertNewPiece(char column, int line, Piece piece)
        {
            Board.InsertPiece(piece, new ChessPosition(column, line).ToPosition());
            Pieces.Add(piece);
        }

        private void InsertPieces()
        {

            InsertNewPiece('a', 1, new Rook(Board, Color.White));
            InsertNewPiece('b', 1, new Knight(Board, Color.White));
            InsertNewPiece('c', 1, new Bishop(Board, Color.White));
            InsertNewPiece('d', 1, new Queen(Board, Color.White));
            InsertNewPiece('e', 1, new King(Board, Color.White, this));
            InsertNewPiece('f', 1, new Bishop(Board, Color.White));
            InsertNewPiece('g', 1, new Knight(Board, Color.White));
            InsertNewPiece('h', 1, new Rook(Board, Color.White));
            InsertNewPiece('a', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('b', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('c', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('d', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('e', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('f', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('g', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('h', 2, new Pawn(Board, Color.White, this));

            InsertNewPiece('a', 8, new Rook(Board, Color.Black));
            InsertNewPiece('b', 8, new Knight(Board, Color.Black));
            InsertNewPiece('c', 8, new Bishop(Board, Color.Black));
            InsertNewPiece('d', 8, new Queen(Board, Color.Black));
            InsertNewPiece('e', 8, new King(Board, Color.Black, this));
            InsertNewPiece('f', 8, new Bishop(Board, Color.Black));
            InsertNewPiece('g', 8, new Knight(Board, Color.Black));
            InsertNewPiece('h', 8, new Rook(Board, Color.Black));
            InsertNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('h', 7, new Pawn(Board, Color.Black, this));
        }
    }
}
