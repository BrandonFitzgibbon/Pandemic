using Engine.Contracts;
using Engine.CustomEventArgs;
using Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class InfectionDeck : IDeck
    {
        private Stack<ICard> cardPile;
        private Stack<ICard> discardPile;

        public InfectionDeck(IList<IInfectionCard> infectionCards)
        {
            infectionCards.Shuffle();
            foreach (IInfectionCard card in infectionCards)
            {
                card.Discarded += CardDiscarded;
            }
            cardPile = new Stack<ICard>(infectionCards);
            discardPile = new Stack<ICard>();
        }

        public void SubscribeToEpidemic(IEpidemicCard epidemicCard)
        {
            epidemicCard.Infect += epidemicCard_Infect;
            epidemicCard.Intensify += epidemicCard_Intensify;
        }

        private void epidemicCard_Intensify(object sender, EventArgs e)
        {
            Intensify();
        }

        private void epidemicCard_Infect(object sender, EventArgs e)
        {
            EpidemicInfection();
        }

        public ICard Draw()
        {
            if (cardPile.Count > 0)
                return cardPile.Pop();
            else
                return null;
        }

        public ICard DrawBottom()
        {
            if (cardPile.Count > 0)
            {
                ICard drawn = cardPile.Last();
                List<ICard> cards = new List<ICard>(cardPile.ToList());
                cards.Remove(drawn);
                cards.Reverse();
                cardPile = new Stack<ICard>(cards);
                return drawn;
            }
            else
                return null;
        }

        private void CardDiscarded(object sender, DiscardedEventArgs e)
        {
            discardPile.Push(e.Card);
        }

        private void EpidemicInfection()
        {
            IInfectionCard iCard = (IInfectionCard)DrawBottom();
            iCard.Infect(3);
        }

        private void Intensify()
        {
            List<ICard> cards = discardPile.ToList();
            cards.Shuffle();
            cards.Reverse();
            for (int i = cards.Count - 1; i >= 0; i--)
            {
                cardPile.Push(cards[i]);
            }
            discardPile = new Stack<ICard>();
        }
    }
}
