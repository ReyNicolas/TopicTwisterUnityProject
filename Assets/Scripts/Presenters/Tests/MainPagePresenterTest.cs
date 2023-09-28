using System;
using System.Collections.Generic;
using Commands;
using DataApplicationsContainer;
using DTOs;
using Gateways.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Providers;


namespace Presenters.Tests
{
    public class MainPagePresenterTest
    {
        private IGatewayProvider gatewayProvider;
        private IMatchGateway matchGateway;
        private IRoundGateway roundGateway;
        private ITurnGateway turnGateway;
        private IPlayerGateway playerGateway;
        private IMainView view;
        private MainPagePresenter mainPagePresenter;
        private IDataApplicationContainer dataApplicationContainer;
        private DataApplication initialDataApplication;
        private DataApplication actualTurnDataApplication;
        private ICommand changeToTurnSceneCommand;
        private ICommand returnLoginMenuCommand;
        private string matchID;
        private string playerID;
        private string rivalID;
        MatchConfigurationDTO matchConfig;
        private PlayerStats playerStats;
        private int victoryPoints;

        [SetUp]
        public void Setup()
        {
            view = Substitute.For<IMainView>();
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
            changeToTurnSceneCommand = Substitute.For<ICommand>();
            returnLoginMenuCommand = Substitute.For<ICommand>();
            
            mainPagePresenter = new MainPagePresenter();
            matchID = "Match";
            playerID = "Player";
            rivalID = "Rival";
            victoryPoints = 10;
            initialDataApplication = new DataApplication("", 0, 0, 0, playerID);
            dataApplicationContainer.LoadData().Returns(initialDataApplication);
            matchConfig = new MatchConfigurationDTO(new List<string>() { playerID, rivalID }, 3, 5, 60);
            playerStats = new PlayerStats(victoryPoints, 0);

            actualTurnDataApplication = new DataApplication(matchID, 1, 1, 3, playerID);
            playerGateway.GetPlayerStats(playerID).Returns(playerStats);
            playerGateway.FindNewRivalID(playerID).Returns(rivalID);
            matchGateway.InThisMatchIsThisPlayerTurn(matchID,playerID).Returns(true);
            matchGateway.GetMatchActualTurnData(matchID).Returns(actualTurnDataApplication);
            matchGateway.CreateMatch(Arg.Any<MatchConfigurationDTO>()).Returns(matchID);
        }

        [Test]
        public void Initialize_ShouldCallViewShowPlayerNameAndVictoryPointsMethod()
        {
            
            mainPagePresenter.Initialize(view,gatewayProvider,dataApplicationContainer,changeToTurnSceneCommand,returnLoginMenuCommand);
            
            view.Received().ShowPlayerNameAndVictoryPoints(playerID,victoryPoints);

        }
        
        [Test]
        public void OnStartMatchEventRaise_ShouldSaveDataApplication()
        {
            
            mainPagePresenter.Initialize(view,gatewayProvider,dataApplicationContainer,changeToTurnSceneCommand,returnLoginMenuCommand);
            view.OnStartMatch += Raise.Event<Action>();

            dataApplicationContainer.Received().SaveData(Arg.Any<string>(),Arg.Any<int>(),Arg.Any<int>(),Arg.Any<int>(),Arg.Any<string>());
        }
        
        [Test]
        public void OnStartMatchEventRaise_ShouldConnectPlayerWithARival()
        {
            mainPagePresenter.Initialize(view,gatewayProvider,dataApplicationContainer,changeToTurnSceneCommand,returnLoginMenuCommand);
            view.OnStartMatch += Raise.Event<Action>();

            view.Received().ShowRivalFound(rivalID);
        }
        
        [Test]
        public void OnStartMatchEventRaise_IsPlayerTurnOfTheNewMatch_ChangeSceneToTurnScene()
        {
            matchGateway.InThisMatchIsThisPlayerTurn(matchID,playerID).Returns(true);

            mainPagePresenter.Initialize(view,gatewayProvider,dataApplicationContainer,changeToTurnSceneCommand,returnLoginMenuCommand);
            view.OnStartMatch += Raise.Event<Action>();

            changeToTurnSceneCommand.Received().Execute();
        }
        
       

    }
}