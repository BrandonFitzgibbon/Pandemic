using Engine.CustomEventArgs;
using System;

namespace Engine.Implementations
{
    public abstract class Player
    {
        public string Name { get; protected set; }
        public string Role { get; protected set; }
        public int TurnOrder { get; internal set; }
        public Node Location { get; internal set; }
        public Hand Hand { get; internal set; }
        public ActionCounter ActionCounter { get; private set; }

        internal Player() { }

        internal Player(string name)
        {
            Name = name;
            Hand = new Hand(this);
            ActionCounter = new ActionCounter();
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
