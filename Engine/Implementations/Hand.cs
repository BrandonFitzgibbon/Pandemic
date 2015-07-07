using Engine.Contracts;
using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class Hand : IHand
    {
        private IList<ICard> cards;
        public IEnumerable<ICard> Cards
        {
            get { return cards; }
        }

        public Hand()
        {
            cards = new List<ICard>();
        }

        public void Draw(IDeck deck)
        {
            ICard card = deck.Draw();
            card.Discarded += card_Discarded;
            cards.Add(card);
        }

        void card_Discarded(object sender, DiscardedEventArgs e)
        {
            cards.Remove(e.Card);
        }
    }
}
