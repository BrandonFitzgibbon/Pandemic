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
        internal Player owner;

        public DiscardManager DiscardManager { get; private set; }

        internal IList<Card> cards;     
        public IEnumerable<Card> Cards
        {
            get { return cards; }
        }

        public IEnumerable<CityCard> CityCards
        {
            get { return cards.Where(i => i is CityCard).OfType<CityCard>().Count() > 0 ? cards.Where(i => i is CityCard).OfType<CityCard>() : null; }
        }

        public IEnumerable<BaseActionCard> ActionCards
        {
            get { return cards.Where(i => i is BaseActionCard).OfType<BaseActionCard>().Count() > 0 ? cards.Where(i => i is BaseActionCard).OfType<BaseActionCard>() : null; }
        }

        public Hand(Player player)
        {
            owner = player;
            cards = new List<Card>();
            DiscardManager = new DiscardManager(this);
        }

        internal void AddToHand(Card card)
        {
            card.Discarded += cardDiscarded;
            cards.Add(card);
            if (HandChanged != null) HandChanged(this, EventArgs.Empty);
        }

        internal void RemoveFromHand(Card card)     
        {
            card.Discarded -= cardDiscarded;
            cards.Remove(card);
            if (HandChanged != null) HandChanged(this, EventArgs.Empty);
        }

        private void cardDiscarded(object sender, DiscardedEventArgs e)
        {
            e.Card.Discarded -= cardDiscarded;
            cards.Remove(e.Card);
            if (HandChanged != null) HandChanged(this, EventArgs.Empty);
        }

        public event EventHandler HandChanged;
    }
}
