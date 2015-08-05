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

        public void AddToHand(ICard card)
        {
            card.Discarded += cardDiscarded;
            cards.Add(card);
        }

        private void cardDiscarded(object sender, DiscardedEventArgs e)
        {
            cards.Remove(e.Card);
            if (HandChanged != null) HandChanged(this, EventArgs.Empty);
        }

        public event EventHandler HandChanged;
    }
}
