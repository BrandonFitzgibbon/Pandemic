using Engine.Implementations.ActionItems;
using System;
using System.Collections.Generic;

namespace Engine.Implementations
{
    public class ActionCardManager
    {
        private IEnumerable<Node> nodes;
        public IEnumerable<Node> Nodes
        {
            get { return nodes; }
        }

        private IEnumerable<Player> players;
        public IEnumerable<Player> Players
        {
            get { return players; }
        }

        private InfectionDeck infectionDeck;

        public IEnumerable<InfectionCard> DiscardedInfectionCards
        {
            get { return infectionDeck.DiscardPile; }
        }

        public IEnumerable<InfectionCard> TopSixCards
        {
            get { return infectionDeck.TopSix; }
        }

        private ResearchStationCounter researchStationCounter;

        public ActionCardManager(IEnumerable<Node> nodes, IEnumerable<Player> players, ResearchStationCounter researchStationCounter, InfectionDeck infectionDeck)
        {
            this.nodes = nodes;
            this.players = players;
            this.researchStationCounter = researchStationCounter;
            this.infectionDeck = infectionDeck;
        }

        private bool CanGovernmentGrant(GovernmentGrantItem ggi)
        {
            return ggi != null && ggi.Node != null;
        }

        private void GovernmentGrant(GovernmentGrantItem ggi, Card card)
        {
            researchStationCounter.BuildResearchStation(ggi.Node);
            card.Discard();
        }

        private bool CanAirlift(AirliftItem ai)
        {
            return ai != null && ai.Node != null && ai.Player != null;
        }

        private void Airlift(AirliftItem ai, Card card)
        {
            ai.Player.Move(ai.Node);
            card.Discard();
        }

        private bool CanOneQuietNight(OneQuietNightItem oqni)
        {
            return oqni != null;
        }

        private void OneQuietNight(OneQuietNightItem oqni, Card card)
        {
            if (OneQuietNightPlayed != null) OneQuietNightPlayed(this, EventArgs.Empty);
            card.Discard();
        }

        private bool CanResilientPopulation(ResilientPopulationItem rpi)
        {
            return rpi != null & rpi.InfectionCard != null;
        }

        private void ResilientPopulation(ResilientPopulationItem rpi, Card card)
        {
            infectionDeck.RemoveFromDiscardPile(rpi.InfectionCard);
            card.Discard();
        }

        private void Forecast(Stack<InfectionCard> TopSix)
        {

        }

        internal IList<Card> GetActionCards()
        {
            return new List<Card>()
            {
                new ActionCard<GovernmentGrantItem>("Government Grant", "Add 1 Research Station to any City", new Action<GovernmentGrantItem, Card>(GovernmentGrant), new Func<GovernmentGrantItem, bool>(CanGovernmentGrant)),
                new ActionCard<AirliftItem>("Airlift", "Move any 1 Pawn to any City", new Action<AirliftItem, Card>(Airlift), new Func<AirliftItem, bool>(CanAirlift)),
                new ActionCard<OneQuietNightItem>("One Quiet Night", "Skip the next Infection Phase", new Action<OneQuietNightItem, Card>(OneQuietNight), new Func<OneQuietNightItem, bool>(CanOneQuietNight)),
                new ActionCard<ResilientPopulationItem>("Resilient Population", "Remove any 1 Card in the Infection Discard Pile from the Game", new Action<ResilientPopulationItem, Card>(ResilientPopulation), new Func<ResilientPopulationItem, bool>(CanResilientPopulation)),
            };
        }

        internal event EventHandler OneQuietNightPlayed;
    }
}
