using Engine.Contracts;
using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class Hand
    {
        internal IList<Card> cards;     
        public IEnumerable<Card> Cards
        {
            get { return cards; }
        }

        public IEnumerable<CityCard> CityCards
        {
            get { return cards.Where(i => i is CityCard).OfType<CityCard>(); }
        }

        public Hand()
        {
            cards = new List<Card>();
        }

        internal void AddToHand(Card card)
        {
            card.Discarded += cardDiscarded;
            cards.Add(card);
        }

        internal void RemoveFromHand(Card card)
        {
            card.Discarded -= cardDiscarded;
            cards.Remove(card);
        }

        private void cardDiscarded(object sender, DiscardedEventArgs e)
        {
            cards.Remove(e.Card);
            if (HandChanged != null) HandChanged(this, EventArgs.Empty);
        }

        public event EventHandler HandChanged;
    }
}
