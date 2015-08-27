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
    public abstract class Player
    {
        public string Name { get; protected set; }
        public string Role { get; protected set; }
        public Node Location { get; internal set; }
        public Hand Hand { get; internal set; }
        public ActionCounter ActionCounter { get; private set; }
        public DrawCounter DrawCounter { get; private set; }

        internal Player() { }

        internal Player(string name)
        {
            Name = name;
            Hand = new Hand(this);
            ActionCounter = new ActionCounter();
            DrawCounter = new DrawCounter();
        }

        internal void Move(Node node)
        {
            Node departedNode = Location;
            Location = node;
            if (Moved != null) Moved(this, new PlayerMovedEventArgs(departedNode, node));
        }

        public override string ToString()
        {
            return Name;
        }

        internal event EventHandler<PlayerMovedEventArgs> Moved;
    }
}
