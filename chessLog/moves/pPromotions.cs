namespace ChessLog
{
    public class PawnPromotion : Move
    {
        public override MoveType Type => MoveType.pawnPromotion;
        public override Position FromPos { get; }
        public override Position ToPos { get; }
        private readonly PieceType newType;

        public PawnPromotion(Position from, Position to, PieceType newType)
        {
            FromPos = from;
            ToPos = to;
            this.newType = newType;
        }
        private Piece CreatePromoPiece(Player color)
        {
            return newType switch
            {
                PieceType.Knight => new Knight(color),
                PieceType.Bishop => new Bishop(color),
                PieceType.Rook => new Rook(color),
                _ => new Queen(color)
            };
        }
        public override void Execute(Board board)
        {
            Piece pawn = board[FromPos];
            board[FromPos] = null;

            Piece promoPiece = CreatePromoPiece(pawn.Color);
            promoPiece.HasMoved = true;
            board[ToPos] = promoPiece;
        }
    }
}