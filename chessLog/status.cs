﻿namespace ChessLog
{
    public class GmStatus
    {
        public Board Board { get; }
        public Player CurrentPlayer { get; private set; }
        public EndResult EndResult { get; private set; } = null;

        public GmStatus(Player player, Board board)
        {
            CurrentPlayer = player;
            Board = board;
        }
        public IEnumerable<Move> LegalMovesForPiece(Position pos)
        {
            if (Board.IsEmpty(pos) || Board[pos].Color != CurrentPlayer)
            {
                return Enumerable.Empty<Move>();
            }

            Piece piece = Board[pos];
            IEnumerable<Move> moveCandidates = piece.GetMoves(pos, Board);
            return moveCandidates.Where(move => move.IsLegal(Board));/*return Enumerable.Empty<Move>();*/
        }
        public void MakeMove(Move move)
        {
            Board.SetPawnSkipPos(CurrentPlayer, null);
            move.Execute(Board);
            CurrentPlayer = CurrentPlayer.Opponent();
            CheckForGameOver();
        }
        public IEnumerable<Move> AllLegalMovesFor(Player player)
        {
            IEnumerable<Move> moveCandidates = Board.PiecePositionFor(player).SelectMany(pos =>
            {
                Piece piece = Board[pos];
                return piece.GetMoves(pos, Board);
            });
            return moveCandidates.Where(move => move.IsLegal(Board));
        }
        private void CheckForGameOver()
        {
            if (!AllLegalMovesFor(CurrentPlayer).Any())
            {
                if (Board.IsCheck(CurrentPlayer))
                {
                    EndResult = EndResult.Win(CurrentPlayer.Opponent());
                }
                else
                {
                    EndResult = EndResult.Draw(EndReason.Stalemate);
                }
            }
            else if (Board.InsufMaterial())
            {
                EndResult = EndResult.Draw(EndReason.InsufMaterial);
            }
        }
        public bool IsGameOver()
        {
            return EndResult != null;
        }
    }
}