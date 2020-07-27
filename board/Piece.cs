
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

        public bool ThereArePossibleMoviments()
        {
            bool[,] possibleMovements = PossibleMovements();

            for (int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                    if (possibleMovements[i, j])
                        return true;
            }
            return false;
        }

        public bool CanMoveTo(Position position)
        {
            return PossibleMovements()[position.Line, position.Column];
        }

        public abstract bool[,] PossibleMovements();
    }
}
