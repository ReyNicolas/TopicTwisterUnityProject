using System.Collections.Generic;
using DataApplicationsContainer;
using DTOs;
using Gateways.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Providers;


namespace Presenters.Tests
{
    public class StartOfTurnPresenterTest
    {
        private IGatewayProvider gatewayProvider;
        private IMatchGateway matchGateway;
        private IRoundGateway roundGateway;
        private ITurnGateway turnGateway;
        private IPlayerGateway playerGateway;
        private IStartOfTurnView view;
        private StartOfTurnPresenter startOfTurnPresenter;
        private IDataApplicationContainer dataApplicationContainer;
        private string playerID;
        private string matchID;
        private int roundID;
        private int turnID;
        private int numberOfRounds;
        private List<string> categoriesNames;
        private char letter;
        private float timeToChangeView;

        [SetUp]
        public void Setup()
        {
            view = Substitute.For<IStartOfTurnView>();
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
            startOfTurnPresenter = new StartOfTurnPresenter();
            playerID = "Player";
            matchID = "Match";
            roundID = 1;
            turnID = 1;
            numberOfRounds = 3;
            categoriesNames = new List<string>() { "Categoy1", "Categoy2", "Categoy3", "Categoy4", "Categoy5" };
            letter = 'A';
            timeToChangeView = 3;
        }
        [Test]
        public void Initialize_SendCategoriesNamesAndLetterToTheView()
        {
            DataApplication dataApplication = new DataApplication(matchID, roundID,turnID,
                numberOfRounds, playerID);
            StartTurnDTO startTurnData = new StartTurnDTO(roundID, numberOfRounds, 60, categoriesNames, letter);
            dataApplicationContainer.LoadData().Returns(dataApplication);
            turnGateway.GetStartTurnData(matchID, roundID, playerID).Returns(startTurnData);
            
            startOfTurnPresenter.Initialize(view, gatewayProvider,dataApplicationContainer,timeToChangeView);
            
            view.Received().ShowCategoriesAndLetter(categoriesNames,letter);
        }

        [Test]
        public void Initialize_SendTimeToChangeToTheView()
        {
            DataApplication dataApplication = new DataApplication(matchID, roundID,turnID,
                numberOfRounds, playerID);
            StartTurnDTO startTurnData = new StartTurnDTO(roundID, numberOfRounds, 60, categoriesNames, letter);
            dataApplicationContainer.LoadData().Returns(dataApplication);
            turnGateway.GetStartTurnData(matchID, roundID, playerID).Returns(startTurnData);
            
            startOfTurnPresenter.Initialize(view, gatewayProvider,dataApplicationContainer,timeToChangeView);
            
            view.Received().WaitThisTimeToChangeToTurn(timeToChangeView);
        }
        
    }
}