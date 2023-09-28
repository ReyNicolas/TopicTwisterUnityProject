using System;
using System.Collections.Generic;
using System.Linq;
using Commands;
using DataApplicationsContainer;
using DTOs;
using Gateways.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Providers;


namespace Presenters.Tests
{
    public class TurnPresenterTest
    {
        private IGatewayProvider gatewayProvider;
        private IMatchGateway matchGateway;
        private IRoundGateway roundGateway;
        private ITurnGateway turnGateway;
        private IPlayerGateway playerGateway;
        private ITurnView view;
        private TurnPresenter turnPresenter;
        private IDataApplicationContainer dataApplicationContainer;
        private ICommand changeToMainSceneCommand;
        private ICommand changeToEndRoundSceneCommand;
        private string playerID;
        private string matchID;
        private int roundID;
        private int turnID;
        private int numberOfRounds;
        private List<string> categoriesNames;
        private char letter;

        [SetUp]
        public void Setup()
        {
            view = Substitute.For<ITurnView>();
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
            changeToEndRoundSceneCommand = Substitute.For<ICommand>();
            turnPresenter = new TurnPresenter();
            playerID = "Player";
            matchID = "Match";
            roundID = 1;
            turnID = 1;
            numberOfRounds = 3;
            categoriesNames = new List<string>() { "Categoy1", "Categoy2", "Categoy3", "Categoy4", "Categoy5" };
            letter = 'A';
        }
        [Test]
        public void ShowCategoriesAndLetter_ShouldIntializeAndShowCategoriesAndLetter()
        {
            DataApplication dataApplication = new DataApplication(matchID, roundID,turnID,
                numberOfRounds, playerID);
            StartTurnDTO startTurnData = new StartTurnDTO(roundID, numberOfRounds, 60, categoriesNames, letter);
            dataApplicationContainer.LoadData().Returns(dataApplication);
            turnGateway.GetStartTurnData(matchID, roundID, playerID).Returns(startTurnData);
            
            turnPresenter.Initialize(view, gatewayProvider,dataApplicationContainer,changeToMainSceneCommand,changeToEndRoundSceneCommand);
            
            view.Received().ShowCategoriesAndLetter(categoriesNames,letter);
        }

        [Test]
        public void EndTurn_ShouldValidateWordsToSetAnswers()
        {
            DataApplication dataApplication = new DataApplication(matchID, roundID,turnID,
                numberOfRounds, playerID);
            StartTurnDTO startTurnData = new StartTurnDTO(roundID, numberOfRounds, 60, categoriesNames, letter);
            List<AnswerDTO> answerDatas =
                categoriesNames.Select(cn => new AnswerDTO(cn, cn + "word", letter, false)).ToList();
            TurnResultDTO turnResult = new TurnResultDTO(answerDatas, 0);
            dataApplicationContainer.LoadData().Returns(dataApplication);
            turnGateway.GetStartTurnData(matchID, roundID, playerID).Returns(startTurnData);
            turnGateway.ReturnTurnResult(matchID, roundID, turnID).Returns(turnResult);
            view.GetWordFromCategoryName(categoriesNames[0]).Returns(answerDatas[0].Word);
            view.GetWordFromCategoryName(categoriesNames[1]).Returns(answerDatas[1].Word);
            view.GetWordFromCategoryName(categoriesNames[2]).Returns(answerDatas[2].Word);
            view.GetWordFromCategoryName(categoriesNames[3]).Returns(answerDatas[3].Word);
            view.GetWordFromCategoryName(categoriesNames[4]).Returns(answerDatas[4].Word);
            view.GetTimeLeft().Returns(0);
            dataApplicationContainer.LoadData().Returns(dataApplication);

            //Act
            turnPresenter.Initialize(view, gatewayProvider,dataApplicationContainer,changeToMainSceneCommand,changeToEndRoundSceneCommand);
            view.OnEndTurn += Raise.Event<Action>();
            
            //Assert
            foreach (var categoryName in categoriesNames)
            {
                view.Received().GetWordFromCategoryName(categoryName);
            }
            foreach (var answerData in answerDatas)
            {
                view.Received().ShowAnswer(answerData);
            }
            view.Received().GetTimeLeft();

        }
    }
}
