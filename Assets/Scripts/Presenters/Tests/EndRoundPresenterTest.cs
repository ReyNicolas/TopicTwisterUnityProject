
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
    public class EndRoundPresenterTest
    {
        private IGatewayProvider gatewayProvider;
        private IMatchGateway matchGateway;
        private IRoundGateway roundGateway;
        private ITurnGateway turnGateway;
        private IPlayerGateway playerGateway;
        private IEndRoundView view;
        private EndRoundPresenter endRoundPresenter;
        private IDataApplicationContainer dataApplicationContainer;
        private ICommand changeToMainSceneCommand;
        private ICommand changeToEndMatchSceneCommand;
        private ICommand changeToTurnSceneCommand;
        private string playerID;
        private string rivalID;
        private string matchID;
        private int roundID;
        private int numberOfRounds;

        [SetUp]
        public void Setup()
        {
            view = Substitute.For<IEndRoundView>();
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
            changeToEndMatchSceneCommand = Substitute.For<ICommand>();
            changeToTurnSceneCommand = Substitute.For<ICommand>();
            endRoundPresenter = new EndRoundPresenter();
            playerID = "Player";
            rivalID = "Rival";
            matchID = "Match";
            roundID = 1;
            numberOfRounds = 3;
        }
        
        [Test]
        public void Initialize_ShouldEndTheRound()
        {
            DataApplication data = new DataApplication(matchID,roundID,2,numberOfRounds,playerID);
            RoundResultDTO roundResult = new RoundResultDTO(playerID, rivalID, roundID, numberOfRounds, 'A',
                new List<AnswerComparerDTO>(), 2, 30, 1, 20, true, false);
            dataApplicationContainer.LoadData().Returns(data);
            roundGateway.EndThisRound(matchID, roundID).Returns(true);
            roundGateway.GetRoundResultForPlayer(matchID, roundID, playerID).Returns(roundResult);
            matchGateway.CheckIfMatchIsOver(matchID).Returns(false);
            matchGateway.InThisMatchIsThisPlayerTurn(matchID, playerID).Returns(false);
            DataApplication newRoundTurnData = new DataApplication(matchID,roundID +1,1,numberOfRounds,playerID);
            matchGateway.GetMatchActualTurnData(matchID).Returns(newRoundTurnData);
            
            endRoundPresenter.Initialize(view,gatewayProvider,dataApplicationContainer,changeToMainSceneCommand,changeToEndMatchSceneCommand,changeToTurnSceneCommand);
            
            roundGateway.Received(1).EndThisRound(matchID, roundID);
        }
        [Test]
        public void Initialize_ShouldSendToViewTheRoundResult()
        {
            DataApplication data = new DataApplication(matchID,roundID,2,numberOfRounds,playerID);
            RoundResultDTO roundResult = new RoundResultDTO(playerID, rivalID, roundID, numberOfRounds, 'A',
                new List<AnswerComparerDTO>(), 2, 30, 1, 20, true, false);
            dataApplicationContainer.LoadData().Returns(data);
            roundGateway.EndThisRound(matchID, roundID).Returns(true);
            roundGateway.GetRoundResultForPlayer(matchID, roundID, playerID).Returns(roundResult);
            matchGateway.CheckIfMatchIsOver(matchID).Returns(false);
            matchGateway.InThisMatchIsThisPlayerTurn(matchID, playerID).Returns(false);
            DataApplication newRoundTurnData = new DataApplication(matchID,roundID +1,1,numberOfRounds,playerID);
            matchGateway.GetMatchActualTurnData(matchID).Returns(newRoundTurnData);
            
            endRoundPresenter.Initialize(view,gatewayProvider,dataApplicationContainer,changeToMainSceneCommand,changeToEndMatchSceneCommand,changeToTurnSceneCommand);
            
            roundGateway.Received(1).GetRoundResultForPlayer(matchID,roundID,playerID);
            view.Received(1).ShowResult(roundResult);
        }
        [Test]
        public void Initialize_IsEndOfMatch_ShouldSendToViewToActiveGoToEndMatch()
        {
            var finalRound = 3;
            DataApplication data = new DataApplication(matchID,finalRound,2,numberOfRounds,playerID);
            RoundResultDTO roundResult = new RoundResultDTO(playerID, rivalID, finalRound, numberOfRounds, 'A',
                new List<AnswerComparerDTO>(), 2, 30, 1, 20, true, false);
            dataApplicationContainer.LoadData().Returns(data);
            roundGateway.EndThisRound(matchID, roundID).Returns(true);
            roundGateway.GetRoundResultForPlayer(matchID, roundID, playerID).Returns(roundResult);
            matchGateway.CheckIfMatchIsOver(matchID).Returns(true);
            
            endRoundPresenter.Initialize(view,gatewayProvider,dataApplicationContainer,changeToMainSceneCommand,changeToEndMatchSceneCommand,changeToTurnSceneCommand);
            
            view.Received(1).ActiveGoToEndMatch();
        }
        [Test]
        public void Initialize_NextRoundTurnIsPlayerTurn_ShouldSendToViewToActiveGoToNextTurn()
        {
            DataApplication data = new DataApplication(matchID,roundID,2,numberOfRounds,playerID);
            RoundResultDTO roundResult = new RoundResultDTO(playerID, rivalID, roundID, numberOfRounds, 'A',
                new List<AnswerComparerDTO>(), 2, 30, 1, 20, true, false);
            dataApplicationContainer.LoadData().Returns(data);
            roundGateway.EndThisRound(matchID, roundID).Returns(true);
            roundGateway.GetRoundResultForPlayer(matchID, roundID, playerID).Returns(roundResult);
            matchGateway.CheckIfMatchIsOver(matchID).Returns(false);
            matchGateway.InThisMatchIsThisPlayerTurn(matchID, playerID).Returns(true);
            DataApplication newRoundTurnData = new DataApplication(matchID,roundID +1,1,numberOfRounds,playerID);
            matchGateway.GetMatchActualTurnData(matchID).Returns(newRoundTurnData);
            
            endRoundPresenter.Initialize(view,gatewayProvider,dataApplicationContainer,changeToMainSceneCommand,changeToEndMatchSceneCommand,changeToTurnSceneCommand);
            
            view.Received(1).ActiveGoToNextTurn();
        }
        //[Test]
        public void Initialize_NextRoundTurnIsNotPlayerTurn_ShouldSendToViewToActiveReturnHomePage()
        {
            DataApplication data = new DataApplication(matchID,roundID,2,numberOfRounds,playerID);
            RoundResultDTO roundResult = new RoundResultDTO(playerID, rivalID, roundID, numberOfRounds, 'A',
                new List<AnswerComparerDTO>(), 2, 30, 1, 20, true, false);
            dataApplicationContainer.LoadData().Returns(data);
            roundGateway.EndThisRound(matchID, roundID).Returns(true);
            roundGateway.GetRoundResultForPlayer(matchID, roundID, playerID).Returns(roundResult);
            matchGateway.CheckIfMatchIsOver(matchID).Returns(false);
            matchGateway.InThisMatchIsThisPlayerTurn(matchID, playerID).Returns(false);
            
            endRoundPresenter.Initialize(view,gatewayProvider,dataApplicationContainer,changeToMainSceneCommand,changeToEndMatchSceneCommand,changeToTurnSceneCommand);
            
            view.Received(1).ActiveReturnHomePage();
        }
       
    }
}