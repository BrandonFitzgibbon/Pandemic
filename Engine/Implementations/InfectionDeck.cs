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
        private Stack<Card> cardPile;
        private Stack<Card> discardPile;

        public InfectionDeck(IList<InfectionCard> infectionCards)
        {
            infectionCards.Shuffle();
            foreach (InfectionCard card in infectionCards)
            {
                card.Discarded += CardDiscarded;
            }
            cardPile = new Stack<Card>(infectionCards);
            discardPile = new Stack<Card>();
        }

        public Card Draw()
        {
            if (cardPile.Count > 0)
                return cardPile.Pop();
            else
                return null;
        }

        public Card DrawBottom()
        {
            if (cardPile.Count > 0)
            {
                Card drawn = cardPile.Last();
                List<Card> cards = new List<Card>(cardPile.ToList());
                cards.Remove(drawn);
                cards.Reverse();
                cardPile = new Stack<Card>(cards);
                return drawn;
            }
            else
                return null;
        }

        private void CardDiscarded(object sender, DiscardedEventArgs e)
        {
            discardPile.Push(e.Card);
        }

        public void Intensify()
        {
            List<Card> cards = discardPile.ToList();
            cards.Shuffle();
            cards.Reverse();
            for (int i = cards.Count - 1; i >= 0; i--)
            {
                cardPile.Push(cards[i]);
            }
            discardPile = new Stack<Card>();

            if (Intensified != null) Intensified(this, EventArgs.Empty);
        }

        public event EventHandler Intensified;
    }
}
