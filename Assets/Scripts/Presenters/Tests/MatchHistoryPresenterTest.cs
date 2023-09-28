using System.Collections.Generic;
using Commands;
using DataApplicationsContainer;
using DTOs;
using Gateways.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Presenters.IViews;
using Providers;

namespace Presenters.Tests
{
    public class MatchHistoryPresenterTest
    {

        private IGatewayProvider gatewayProvider;
        private IMatchGateway matchGateway;
        private IRoundGateway roundGateway;
        private ITurnGateway turnGateway;
        private IPlayerGateway playerGateway;
        private IMatchHistoryView view;
        private ICommand goToEndMatchCommand;
        private MatchHistoryPresenter matchHistoryPresenter;
        private IDataApplicationContainer dataApplicationContainer;
        private string playerID;
        private string matchID;

        [SetUp]
        public void Setup()
        {
            view = Substitute.For<IMatchHistoryView>();
            gatewayProvider = Substitute.For<IGatewayProvider>();
            matchGateway = Substitute.For<IMatchGateway>();
            roundGateway = Substitute.For<IRoundGateway>();
            turnGateway = Substitute.For<ITurnGateway>();
            playerGateway = Substitute.For<IPlayerGateway>();
            gatewayProvider.ProvideMatchGateway().Returns(matchGateway);
            gatewayProvider.ProvidePlayerGateway().Returns(playerGateway);
            gatewayProvider.ProvideRoundGateway().Returns(roundGateway);
            gatewayProvider.ProvideTurnGateway().Returns(turnGateway);
            dataApplicationContainer = Substitute.For<IDataApplicationContainer>();
            playerID = "Player";
            matchID = "Match";
        }
        [Test]
        public void MatchHistoryPresenter_GetMatchesResultsData()
        {
            //Arrange
            DataApplication dataApplication = new DataApplication("", 0,0,
                0, playerID);
            MatchResultDTO matchResultData = new MatchResultDTO(matchID,playerID, "Rival", 2, 1, true, false);
            List<MatchResultDTO> matchesResultsDatas = new List<MatchResultDTO>() { matchResultData };
            dataApplicationContainer.LoadData().Returns(dataApplication);
            playerGateway.GetThisMatchHistory(playerID).Returns(matchesResultsDatas);
            //Act
            matchHistoryPresenter = new MatchHistoryPresenter(view, gatewayProvider, dataApplicationContainer,goToEndMatchCommand);
            //Assert
            playerGateway.Received(1).GetThisMatchHistory(playerID);
        }

        [Test]
        public void MatchHistoryPresenter_SendToViewAListOfMatchesResultsData()
        {
            //Arrange
            DataApplication dataApplication = new DataApplication("", 0,0,
                0, playerID);
            MatchResultDTO matchResultData = new MatchResultDTO(matchID,playerID, "Rival", 2, 1, true, false);
            List<MatchResultDTO> matchesResultsDatas = new List<MatchResultDTO>() { matchResultData };
            dataApplicationContainer.LoadData().Returns(dataApplication);
            playerGateway.GetThisMatchHistory(playerID).Returns(matchesResultsDatas);
            //Act
            matchHistoryPresenter = new MatchHistoryPresenter(view, gatewayProvider, dataApplicationContainer,goToEndMatchCommand);
            //Assert
            view.Received(1).SetMatchesResults(matchesResultsDatas);
        }
        
    }
}