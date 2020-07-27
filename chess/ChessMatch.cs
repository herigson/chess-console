using System;
using board;
namespace chess
{
    public class ChessMatch
    {
        public Board Board { get; private set; }
        public int Shift { get; set; }
        public Color CurrentPlayer { get; set; }
        public bool Finished { get; private set; }


        public ChessMatch()
        {
            Board = new Board(8, 8);
            Shift = 1;
            CurrentPlayer = Color.White;
            InsertPieces();
            Finished = false;
        }

        public void ExecuteAMovement(Position origin, Position destiny)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncrementeNumberOfMovements();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.InsertPiece(piece, destiny);
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
                throw new Exception("Destiny position invalid!");
        }

        private void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
                CurrentPlayer = Color.Black;
            else
                CurrentPlayer = Color.White;
        }

        private void InsertPieces()
        {
            Board.InsertPiece(new Rook(Board, Color.White), new ChessPosition('c',1).ToPosition());
            Board.InsertPiece(new Rook(Board, Color.White), new ChessPosition('c', 2).ToPosition());
            Board.InsertPiece(new Rook(Board, Color.White), new ChessPosition('d', 2).ToPosition());
            Board.InsertPiece(new Rook(Board, Color.White), new ChessPosition('e', 2).ToPosition());
            Board.InsertPiece(new Rook(Board, Color.White), new ChessPosition('e', 1).ToPosition());
            Board.InsertPiece(new King(Board, Color.White), new ChessPosition('d', 1).ToPosition());

            Board.InsertPiece(new Rook(Board, Color.Black), new ChessPosition('c', 7).ToPosition());
            Board.InsertPiece(new Rook(Board, Color.Black), new ChessPosition('c', 8).ToPosition());
            Board.InsertPiece(new Rook(Board, Color.Black), new ChessPosition('d', 7).ToPosition());
            Board.InsertPiece(new Rook(Board, Color.Black), new ChessPosition('e', 7).ToPosition());
            Board.InsertPiece(new Rook(Board, Color.Black), new ChessPosition('e', 8).ToPosition());
            Board.InsertPiece(new King(Board, Color.Black), new ChessPosition('d', 8).ToPosition());
        }
    }
}
