using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.CustomEventArgs
{
    public class DiscardedEventArgs : EventArgs
    {
        private ICard card;
        public ICard Card
        {
            get { return card; }
        }

        public DiscardedEventArgs(ICard card)
        {
            this.card = card;
        }
    }
}
