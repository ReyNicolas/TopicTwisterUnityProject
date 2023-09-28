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
    public class EndMatchPresenterTest
    {
        private IGatewayProvider gatewayProvider;
        private IMatchGateway matchGateway;
        private IRoundGateway roundGateway;
        private ITurnGateway turnGateway;
        private IPlayerGateway playerGateway;
        private IEndMatchView view;
        private EndMatchPresenter endMatchPresenter;
        private IDataApplicationContainer dataApplicationContainer;
        private ICommand changeToMainSceneCommand;
        private string playerID;
        private string rivalID;
        private string matchID;
        private int roundID;
        private int turnID;
        private int numberOfRounds;

        [SetUp]
        public void Setup()
        {
            view = Substitute.For<IEndMatchView>();
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
            changeToMainSceneCommand = Substitute.For<ICommand>();
            endMatchPresenter = new EndMatchPresenter();
            playerID = "Player";
            rivalID = "Rival";
            matchID = "Match";
            roundID = 3;
            turnID = 2;
            numberOfRounds = 3;
        }

        [Test]
        public void Initialize_EndTheActualMatch()
        {
            //Arrange
            DataApplication dataApplication = new DataApplication(matchID, roundID,turnID,
                numberOfRounds, playerID);
            MatchResultDTO matchResultData = new MatchResultDTO(matchID,playerID, rivalID, 2, 1, true,false);
            dataApplicationContainer.LoadData().Returns(dataApplication);
            matchGateway.EndThisMatch(matchID).Returns(true);
            matchGateway.GetMatchResultForPlayer(matchID, playerID).Returns(matchResultData);
            //Act
            endMatchPresenter.Initialize(view,gatewayProvider,dataApplicationContainer,changeToMainSceneCommand);
            //Assert
            matchGateway.Received(1).EndThisMatch(matchID);
        }
        [Test]
        public void Initialize_GetTheActualMatchResult()
        {
            //Arrange
            DataApplication dataApplication = new DataApplication(matchID, roundID,turnID,
                numberOfRounds, playerID);
            MatchResultDTO matchResultData = new MatchResultDTO(matchID,playerID, rivalID, 2, 1, true,false);
            dataApplicationContainer.LoadData().Returns(dataApplication);
            matchGateway.EndThisMatch(matchID).Returns(true);
            matchGateway.GetMatchResultForPlayer(matchID, playerID).Returns(matchResultData);
            //Act
            endMatchPresenter.Initialize(view,gatewayProvider,dataApplicationContainer,changeToMainSceneCommand);
            //Assert
            matchGateway.Received(1).GetMatchResultForPlayer(matchID,playerID);
        }

        [Test]
        public void Initialize_SendToTheViewTheMatchResultData()
        {
            //Arrange
            DataApplication dataApplication = new DataApplication(matchID, roundID,turnID,
                numberOfRounds, playerID);
            MatchResultDTO matchResultData = new MatchResultDTO(matchID,playerID, rivalID, 2, 1, true,false);
            dataApplicationContainer.LoadData().Returns(dataApplication);
            matchGateway.EndThisMatch(matchID).Returns(true);
            matchGateway.GetMatchResultForPlayer(matchID, playerID).Returns(matchResultData);
            //Act
            endMatchPresenter.Initialize(view,gatewayProvider,dataApplicationContainer,changeToMainSceneCommand);
            //Assert
            view.Received(1).ShowResult(matchResultData);
        }
        
    }
}