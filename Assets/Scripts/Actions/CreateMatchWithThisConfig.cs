using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;
using Gateways.Interfaces;

namespace Actions
{
    public class CreateMatchWithThisConfig
    {
        private IMatchGateway matchGateway;

        public CreateMatchWithThisConfig(IMatchGateway matchGateway)
        {
            this.matchGateway = matchGateway;
        }

        public async Task<string> Execute(List<string> playersIDs, int numberOfRounds, int categoriesPerRound, float timePerTurn)
        {
            MatchConfigurationDTO matchConfig =
                new MatchConfigurationDTO(playersIDs, numberOfRounds, categoriesPerRound, timePerTurn);
            return await matchGateway.CreateMatch(matchConfig);
        }
    }
}