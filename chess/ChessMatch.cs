using System;
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


        public ChessMatch()
        {
            Board = new Board(8, 8);
            Shift = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            InsertPieces();


        }

        public void ExecuteAMovement(Position origin, Position destiny)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncrementeNumberOfMovements();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.InsertPiece(piece, destiny);
            if(capturedPiece != null)
            {
                Captured.Add(capturedPiece);
            }
        }

        public void MakeAPlay(Position origin,Position destiny)
        {
            ExecuteAMovement(origin, destiny);
            Shift++;
            ChangePlayer();
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
            if (!Board.Piece(origin).CanMoveTo(destiny))
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
            foreach(Piece piece in Captured)
            {
                if(piece.Color == color)
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

        public void InsertNewPiece(char column, int line, Piece piece)
        {
            Board.InsertPiece(piece, new ChessPosition(column, line).ToPosition());
            Pieces.Add(piece);
        }

        private void InsertPieces()
        {
            InsertNewPiece('c', 1, new Rook(Board, Color.White));
            InsertNewPiece('c', 2, new Rook(Board, Color.White));
            InsertNewPiece('d', 2, new Rook(Board, Color.White));
            InsertNewPiece('e', 2, new Rook(Board, Color.White));
            InsertNewPiece('e', 1, new Rook(Board, Color.White));
            InsertNewPiece('d', 1, new King(Board, Color.White));

            InsertNewPiece('c', 7, new Rook(Board, Color.Black));
            InsertNewPiece('c', 8, new Rook(Board, Color.Black));
            InsertNewPiece('d', 7, new Rook(Board, Color.Black));
            InsertNewPiece('e', 7, new Rook(Board, Color.Black));
            InsertNewPiece('e', 8, new Rook(Board, Color.Black));
            InsertNewPiece('d', 8, new King(Board, Color.Black));
        }
    }
}
