using Engine.Implementations.ActionItems;
using Engine.Implementations.ActionManagers;
using System;
using System.Collections.Generic;

namespace Engine.Implementations
{
    public class ActionCardManager
    {
        private IEnumerable<Node> nodes;
        private IEnumerable<Player> players;
        private InfectionDeck infectionDeck;
        private ResearchStationCounter researchStationCounter;

        private GovernmentGrantManager governmentGrantManager;
        private AirliftManager airliftManager;

        public IEnumerable<GovernmentGrantItem> GovernmentGrantTargets
        {
            get { return governmentGrantManager != null ? governmentGrantManager.Targets : null; }
        }

        public IEnumerable<AirliftItem> AirliftTargets
        {
            get { return airliftManager != null ? airliftManager.Targets : null; }
        }

        public ActionCardManager(IEnumerable<Node> nodes, IEnumerable<Player> players, ResearchStationCounter researchStationCounter, InfectionDeck infectionDeck)
        {
            this.nodes = nodes;
            this.players = players;
            this.researchStationCounter = researchStationCounter;
            this.infectionDeck = infectionDeck;

            governmentGrant = new ActionCard<GovernmentGrantItem>("Government Grant", "Add 1 Research Station to any City");
            governmentGrantManager = new GovernmentGrantManager(GovernmentGrant, nodes, researchStationCounter);
            governmentGrant.CanAction = governmentGrantManager.CanComissionResearchStation;
            governmentGrant.Action = governmentGrantManager.ComissionResearchStation;

            airlift = new ActionCard<AirliftItem>("Airlift", "Move any 1 Player to any City");
            airliftManager = new AirliftManager(Airlift, nodes, players);
            airlift.CanAction = airliftManager.CanAirlift;
            airlift.Action = airliftManager.Airlift;
        }

        private ActionCard<GovernmentGrantItem> governmentGrant;
        internal ActionCard<GovernmentGrantItem> GovernmentGrant
        {
            get { return governmentGrant; }
        }

        private ActionCard<AirliftItem> airlift;
        internal ActionCard<AirliftItem> Airlift
        {
            get { return airlift; }
        }
    }
}
