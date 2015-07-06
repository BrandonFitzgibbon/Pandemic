using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class PlayerDeck : IDeck
    {
        private Stack<ICard> cardPile;
        private Stack<ICard> discardPile;

        public PlayerDeck(IList<ICard> cards)
        {
            cardPile = new Stack<ICard>(cards);    
        }

        public ICard Draw()
        {
            return cardPile.Pop();
        }

        public void Discard(ICard discardedCard)
        {
            discardPile.Push(discardedCard);
        }
    }
}
