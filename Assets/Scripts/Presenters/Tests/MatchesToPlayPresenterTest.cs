using System;
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
    public class MatchesToPlayPresenterTest
    {
        private IGatewayProvider gatewayProvider;
        private IMatchGateway matchGateway;
        private IRoundGateway roundGateway;
        private ITurnGateway turnGateway;
        private IPlayerGateway playerGateway;
        private IMatchesToPlayView view;
        private MatchesToPlayPresenter matchesToPlayPresenter;
        private IDataApplicationContainer dataApplicationContainer;
        private ICommand goToStartTurnCommand;
        private ICommand goToEndRoundCommand;
        private DataApplication startDataApplication;
        private DataApplication actualMatchDataAfterFirstRound;
        private DataApplication MatchDataFirstRound;
        private MatchInfoDTO matchInfoData;
        private List<MatchInfoDTO> matchInfosDatas;
        private string playerID;
        private string matchID;

        [SetUp]
        public void Setup()
        {

            view = Substitute.For<IMatchesToPlayView>();
            gatewayProvider = Substitute.For<IGatewayProvider>();
            matchGateway = Substitute.For<IMatchGateway>();
            roundGateway = Substitute.For<IRoundGateway>();
            turnGateway = Substitute.For<ITurnGateway>();
            playerGateway = Substitute.For<IPlayerGateway>();
            goToStartTurnCommand = Substitute.For<ICommand>();
            goToEndRoundCommand = Substitute.For<ICommand>();
            gatewayProvider.ProvideMatchGateway().Returns(matchGateway);
            gatewayProvider.ProvidePlayerGateway().Returns(playerGateway);
            gatewayProvider.ProvideRoundGateway().Returns(roundGateway);
            gatewayProvider.ProvideTurnGateway().Returns(turnGateway);
            dataApplicationContainer = Substitute.For<IDataApplicationContainer>();
            playerID = "Player";
            matchID = "Match";
            startDataApplication = new DataApplication("", 0, 0, 0, playerID);
            dataApplicationContainer.LoadData().Returns(startDataApplication);
            actualMatchDataAfterFirstRound = new DataApplication(matchID, 3, 1, 3, playerID);
            MatchDataFirstRound = new DataApplication(matchID, 1, 1, 3, playerID);
            matchInfoData = new MatchInfoDTO(matchID, "Rival", 1, 1, 3, true);
            matchInfosDatas = new List<MatchInfoDTO>() { matchInfoData };
            playerGateway.GetThisMatchesToPlay(playerID).Returns(matchInfosDatas);
        }

        [Test]
        public void MatchesToPlayPresenter_GetMatchesToPlayInfo()
        {
            //Arrange
            //Act
            matchesToPlayPresenter = new MatchesToPlayPresenter(view, gatewayProvider, dataApplicationContainer, goToStartTurnCommand, goToEndRoundCommand);
            //Assert
            playerGateway.Received(1).GetThisMatchesToPlay(playerID);
        }

        [Test]
        public void MatchToPlayPresenter_SendToViewAListOfMatchesToPlayData()
        {
            //Arrange
            //Act
            matchesToPlayPresenter = new MatchesToPlayPresenter(view, gatewayProvider, dataApplicationContainer, goToStartTurnCommand, goToEndRoundCommand);
            //Assert
            view.Received(1).SetMatchesToPlay(matchInfosDatas);
        }

        [Test]
        public void OnPLayTurn_RoundOne_SaveActualTurnDataInApplication()
        {
            //Arrange
            matchGateway.GetMatchActualTurnData(matchID).Returns(MatchDataFirstRound);
            matchGateway.InThisMatchIsThisPlayerTurn(matchID, playerID).Returns(true);
            //Act
            matchesToPlayPresenter = new MatchesToPlayPresenter(view, gatewayProvider, dataApplicationContainer, goToStartTurnCommand, goToEndRoundCommand);
            view.OnPlayTurn += Raise.Event<Action<MatchInfoDTO>>(matchInfoData);
            //Assert
            dataApplicationContainer.Received().SaveData(matchID, MatchDataFirstRound.RoundID, MatchDataFirstRound.TurnID, MatchDataFirstRound.NumberOfRounds, playerID);

        }

        [Test]
        public void OnPLayTurn_FirstRound_ExecuteChangeToStartTurn()
        {
            //Arrange
            matchGateway.GetMatchActualTurnData(matchID).Returns(MatchDataFirstRound);
            matchGateway.InThisMatchIsThisPlayerTurn(matchID, playerID).Returns(true);
            //Act
            matchesToPlayPresenter = new MatchesToPlayPresenter(view, gatewayProvider, dataApplicationContainer, goToStartTurnCommand, goToEndRoundCommand);
            view.OnPlayTurn += Raise.Event<Action<MatchInfoDTO>>(matchInfoData);
            //Assert
            goToStartTurnCommand.Received().Execute();
        }

        [Test]
        public void OnPLayTurne_SaveActualTurnDataInApplication()
        {
            //Arrange
            var expectedRound = actualMatchDataAfterFirstRound.RoundID - 1;
            matchGateway.GetMatchActualTurnData(matchID).Returns(actualMatchDataAfterFirstRound);
            matchGateway.InThisMatchIsThisPlayerTurn(matchID, playerID).Returns(true);
            //Act
            matchesToPlayPresenter = new MatchesToPlayPresenter(view, gatewayProvider, dataApplicationContainer, goToStartTurnCommand, goToEndRoundCommand);
            view.OnPlayTurn += Raise.Event<Action<MatchInfoDTO>>(matchInfoData);
            //Assert            
            dataApplicationContainer.Received().SaveData(matchID, expectedRound, actualMatchDataAfterFirstRound.TurnID, actualMatchDataAfterFirstRound.NumberOfRounds, playerID);

        }

        [Test]
        public void OnPLayTurn_RoundIsHigherThanOne_ExecuteChangeToEndRoundResult()
        {
            //Arrange
            matchGateway.GetMatchActualTurnData(matchID).Returns(actualMatchDataAfterFirstRound);
            matchGateway.InThisMatchIsThisPlayerTurn(matchID, playerID).Returns(true);
            //Act
            matchesToPlayPresenter = new MatchesToPlayPresenter(view, gatewayProvider, dataApplicationContainer, goToStartTurnCommand, goToEndRoundCommand);
            view.OnPlayTurn += Raise.Event<Action<MatchInfoDTO>>(matchInfoData);
            //Assert
            goToEndRoundCommand.Received().Execute();
        }


    }
}