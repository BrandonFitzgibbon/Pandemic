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
    public class InfectionDeck : IInfectionDeck
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

        private void ShuffleDiscardPileOntoCardPile()
        {
            List<ICard> cards = discardPile.ToList();
            cards.Shuffle();
            cards.Reverse();
            foreach (ICard card in discardPile)
            {
                cardPile.Push(discardPile.Pop());
            }
        }

        private void CardDiscarded(object sender, DiscardedEventArgs e)
        {
            discardPile.Push(e.Card);
        }
    }
}
