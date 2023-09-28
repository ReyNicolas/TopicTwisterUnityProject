using System.Collections.Generic;

namespace DTOs
{
    public class MatchConfigurationDTO
    {
        public List<string> PlayersIDs;
        public int NumberOfRounds;
        public int CategoriesPerRound;
        public float TimePerTurn;

        public MatchConfigurationDTO(List<string> playersIDs, int numberOfRounds, int categoriesPerRound, float timePerTurn)
        {
            PlayersIDs = playersIDs;
            NumberOfRounds = numberOfRounds;
            CategoriesPerRound = categoriesPerRound;
            TimePerTurn = timePerTurn;
        }
    }
}