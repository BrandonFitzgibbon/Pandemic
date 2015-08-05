using Engine.Contracts;
using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public abstract class Player : IPlayer, IMove
    {
        public string Name { get; protected set; }
        public INode Location { get; protected set; }
        public IHand Hand { get; protected set; }
        public IActionCounter ActionCounter { get; protected set; }

        public Player(string name, IHand hand)
        {
            Name = name;
            Hand = hand;
            ActionCounter = new ActionCounter();
        }

        public void Move(INode node)
        {
            INode departedNode = Location;
            Location = node;
            if (Moved != null) Moved(this, new PlayerMovedEventArgs(departedNode, node));
        }

        public override string ToString()
        {
            return Name;
        }

        public event EventHandler<PlayerMovedEventArgs> Moved;
    }
}
