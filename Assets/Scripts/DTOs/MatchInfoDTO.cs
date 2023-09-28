namespace DTOs
{
    public class MatchInfoDTO
    {
        public string MatchID { get; set; }
        public string RivalID { get; set; }
        public int PlayerRoundsWon { get; set; }
        public int RivalRoundsWon { get; set; }
        public int ActualRound { get; set; }
        public bool IsPlayerTurn { get; set; }

        public MatchInfoDTO(string matchId, string rivalId, int playerRoundsWon, int rivalRoundsWon, int actualRound, bool isPlayerTurn)
        {
            MatchID = matchId;
            RivalID = rivalId;
            PlayerRoundsWon = playerRoundsWon;
            RivalRoundsWon = rivalRoundsWon;
            ActualRound = actualRound;
            IsPlayerTurn = isPlayerTurn;
        }
    }
}