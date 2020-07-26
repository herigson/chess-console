
namespace board
{
    public abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int NumberOfMovements { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Board board, Color color)
        {
            Position = null;
            Color = color;
            NumberOfMovements = 0;
            Board = board;
        }

        public void IncrementeNumberOfMovements()
        {
            NumberOfMovements++;
        }

        public abstract bool[,] PossibleMovements();
    }
}
