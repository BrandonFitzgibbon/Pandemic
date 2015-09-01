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
    public class InfectionDeck
    {
        private Stack<InfectionCard> cardPile;

        internal IEnumerable<InfectionCard> TopSix
        {
            get { return cardPile.Take(6); }
        }

        private Stack<InfectionCard> discardPile;

        internal IEnumerable<InfectionCard> DiscardPile
        {
            get { return discardPile; }
        }

        public InfectionDeck(IList<InfectionCard> infectionCards)
        {
            infectionCards.Shuffle();
            foreach (InfectionCard card in infectionCards)
            {
                card.Discarded += CardDiscarded;
            }
            cardPile = new Stack<InfectionCard>(infectionCards);
            discardPile = new Stack<InfectionCard>();
        }

        public InfectionCard Draw()
        {
            if (cardPile.Count > 0)
                return cardPile.Pop();
            else
                return null;
        }

        public InfectionCard DrawBottom()
        {
            if (cardPile.Count > 0)
            {
                InfectionCard drawn = cardPile.Last();
                List<InfectionCard> cards = new List<InfectionCard>(cardPile.ToList());
                cards.Remove(drawn);
                cards.Reverse();
                cardPile = new Stack<InfectionCard>(cards);
                return drawn;
            }
            else
                return null;
        }

        private void CardDiscarded(object sender, DiscardedEventArgs e)
        {
            discardPile.Push((InfectionCard)e.Card);
        }

        internal void RemoveFromDiscardPile(InfectionCard card)
        {
            List<InfectionCard> cards = new List<InfectionCard>(discardPile.ToList());
            cards.Remove(card);
            cards.Reverse();
            discardPile = new Stack<InfectionCard>(cards);
        }

        internal void Intensify()
        {
            List<InfectionCard> cards = discardPile.ToList();
            cards.Shuffle();
            cards.Reverse();
            for (int i = cards.Count - 1; i >= 0; i--)
            {
                cardPile.Push(cards[i]);
            }
            discardPile = new Stack<InfectionCard>();

            if (Intensified != null) Intensified(this, EventArgs.Empty);
        }

        public event EventHandler Intensified;
    }
}
