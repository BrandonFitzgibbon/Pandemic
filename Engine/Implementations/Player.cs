using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public abstract class Player : IPlayer
    {
        private string name;
        public string Name
        {
            get { return name; }
        }
        
        private INode location;
        public INode Location
        {
            get { return location; }
            set { location = value; }
        }

        private IList<ICard> hand;
        public IList<ICard> Hand
        {
            get { return hand; }
        }

        public Player(string name)
        {
            this.name = name;
            this.hand = new List<ICard>();
        }

        public virtual void Drive(INode destination)
        {
            if (this.location.Connections.Contains(destination))
                this.location = destination;
        }

    }
}
