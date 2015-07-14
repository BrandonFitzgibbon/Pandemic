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

        public IEnumerable<ICityCard> CityCards
        {
            get { return cards.Where(i => i is ICityCard).OfType<ICityCard>(); }
        }

        public Hand()
        {
            cards = new List<ICard>();
        }

        public void Draw(IDeck deck, out ICard drawnCard)
        {
            drawnCard = deck.Draw();
            drawnCard.Discarded += drawnCard_Discarded;
            cards.Add(drawnCard);
        }

        void drawnCard_Discarded(object sender, DiscardedEventArgs e)
        {
            cards.Remove(e.Card);
        }
    }
}
