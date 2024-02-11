namespace ChessLog
{
    public class Rook : Piece
    {
        public override PieceType Type => PieceType.Rook;
        public override Player Color { get; }

        private static readonly Direction[] dirs = new Direction[]
        {
            Direction.west,
            Direction.east,
            Direction.south,
            Direction.north
        };
        public Rook(Player color)
        {
            Color = color;
        }
        public override Piece Copy()
        {
            Rook copy = new Rook(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return MovePosInDir(from, board, dirs).Select(to => new NormalMove(from, to));
        }
    }
}