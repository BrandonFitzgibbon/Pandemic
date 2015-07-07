using Engine.Contracts;
using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public abstract class Card : ICard
    {
        public void Discard()
        {
            if (Discarded != null) Discarded(this, new DiscardedEventArgs(this));
        }

        public event EventHandler<DiscardedEventArgs> Discarded;   
    }
}
