using System;
using System.Collections.Generic;
using Actions;

namespace Providers
{
    public class ActionProvider
    {
        private Dictionary<Type, object> actions;
        
        public ActionProvider(IGatewayProvider gatewayProvider)
        {
            actions = new Dictionary<Type, object>()
            {
                {
                    typeof(CreateMatchWithThisConfig),
                    new CreateMatchWithThisConfig(gatewayProvider.ProvideMatchGateway())
                },
                { typeof(EndThisMatch), new EndThisMatch(gatewayProvider.ProvideMatchGateway()) },
                { typeof(SendEndMatchNotificationsToPlayers), new SendEndMatchNotificationsToPlayers(gatewayProvider.ProvideMatchGateway()) },
                { typeof(SendMatchChangeNotificationsToPlayers), new SendMatchChangeNotificationsToPlayers(gatewayProvider.ProvideMatchGateway()) },
                { typeof(EndThisRound), new EndThisRound(gatewayProvider.ProvideRoundGateway()) },
                { typeof(EndThisTurn), new EndThisTurn(gatewayProvider.ProvideTurnGateway()) },
                { typeof(GetMatchActualTurnData), new GetMatchActualTurnData(gatewayProvider.ProvideMatchGateway()) },
                { typeof(CheckLoginData), new CheckLoginData(gatewayProvider.ProvidePlayerGateway()) },
                { typeof(CheckRegisterData), new CheckRegisterData(gatewayProvider.ProvidePlayerGateway()) },
                { typeof(GetPlayerMatchHistory), new GetPlayerMatchHistory(gatewayProvider.ProvidePlayerGateway()) },
                { typeof(GetPlayerMatchesToPlay), new GetPlayerMatchesToPlay(gatewayProvider.ProvidePlayerGateway()) },
                { typeof(GetPlayerStats), new GetPlayerStats(gatewayProvider.ProvidePlayerGateway()) },
                { typeof(GetPlayerRematchNotifications), new GetPlayerRematchNotifications(gatewayProvider.ProvidePlayerGateway()) },
                { typeof(RemovePlayerRematchNotification), new RemovePlayerRematchNotification(gatewayProvider.ProvidePlayerGateway()) },
                { typeof(SendRematchNotification), new SendRematchNotification(gatewayProvider.ProvidePlayerGateway()) },
                { typeof(FindNewRivalID), new FindNewRivalID(gatewayProvider.ProvidePlayerGateway()) },
                { typeof(GetStartTurnData), new GetStartTurnData(gatewayProvider.ProvideTurnGateway()) },
                { typeof(GetTurnResult), new GetTurnResult(gatewayProvider.ProvideTurnGateway()) },
                { typeof(IsEndOfThisMatch), new IsEndOfThisMatch(gatewayProvider.ProvideMatchGateway()) },
                { typeof(IsPlayerTurn), new IsPlayerTurn(gatewayProvider.ProvideMatchGateway()) },
                { typeof(IsThisRoundOver), new IsThisRoundOver(gatewayProvider.ProvideRoundGateway()) },
                {
                    typeof(ReturnMatchResultForPlayer),
                    new ReturnMatchResultForPlayer(gatewayProvider.ProvideMatchGateway())
                },
                {
                    typeof(ReturnRoundResultForPlayer),
                    new ReturnRoundResultForPlayer(gatewayProvider.ProvideRoundGateway())
                },
                { typeof(SetThisTurnAnswer), new SetThisTurnAnswer(gatewayProvider.ProvideTurnGateway()) }

            };


        }
        
        public T  Provide<T>()
        {
            actions.TryGetValue(typeof(T), out object action);
            return (T)action;
        }

    }
}