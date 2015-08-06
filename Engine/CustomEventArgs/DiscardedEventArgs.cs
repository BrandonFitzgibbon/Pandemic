using Engine.Contracts;
using Engine.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.CustomEventArgs
{
    public class DiscardedEventArgs : EventArgs
    {
        private Card card;
        public Card Card
        {
            get { return card; }
        }

        public DiscardedEventArgs(Card card)
        {
            this.card = card;
        }
    }
}
