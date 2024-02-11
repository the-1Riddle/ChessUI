namespace ChessLog
{
    public class EndResult
    {
        public Player Winner { get; }
        public EndReason Reason { get; }

        public EndResult(Player winner, EndReason reason)
        {
            Winner = winner;
            Reason = reason;
        }
        public static EndResult Win(Player winner)
        {
            return new EndResult(winner, EndReason.Checkmate);
        }
        public static EndResult Draw(EndReason reason)
        {
            return new EndResult(Player.none, reason);
        }
    }
}
