using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class DiscardManager
    {
        private bool blocking;

        private Hand hand;
        public Hand Hand
        {
            get { return hand; }
        }

        public string OwnerName
        {
            get { return hand.owner.Name; }
        }

        public DiscardManager(Hand hand)
        {
            this.hand = hand;
            this.hand.HandChanged += hand_HandChanged;
        }

        public bool CanDiscard(Card card)
        {
            return this.hand.Cards.Contains(card);
        }

        public void Discard(Card card)
        {
            if (CanDiscard(card))
                card.Discard();
        }

        private void hand_HandChanged(object sender, EventArgs e)
        {
            if(hand.Cards.Count() > 7 && blocking == false)
            {
                blocking = true;
                if (Block != null) Block(this, EventArgs.Empty);
            }
            else if(hand.Cards.Count() < 8 && blocking == true)
            {
                blocking = false;
                if (Release != null) Release(this, EventArgs.Empty);
            }
        }

        public event EventHandler Block;
        public event EventHandler Release;
    }
}
