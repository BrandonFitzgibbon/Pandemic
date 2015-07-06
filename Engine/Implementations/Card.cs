using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public abstract class Card : ICard
    {
        public virtual void Discard(IDeck deck)
        {
            deck.Discard(this);
        }
    }
}
