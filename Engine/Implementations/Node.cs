using Engine.Contracts;
using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class Node : INode
    {
        public ICity City { get; private set; }
        public IDisease Disease { get; private set; }
        public bool ResearchStation { get; internal set; }

        private List<INode> connections;
        public IEnumerable<INode> Connections
        {
            get { return connections; }
        }

        private List<IPlayer> players;
        public IEnumerable<IPlayer> Players
        {
            get { return players; }
        }

        public Node(ICity city, IDisease disease)
        {
            City = city;
            Disease = disease;
            connections = new List<INode>();
            players = new List<IPlayer>();
        }

        public void Connect(INode node)
        {
            this.connections.Add(node);
        }

        public void SubscribeToMover(IMove mover)
        {
            mover.Moved += moverMoved;
        }

        private void moverMoved(object sender, PlayerMovedEventArgs e)
        {
            IPlayer player = sender as IPlayer;
            if (player != null)
            {
                if (e.DepartedNode == this)
                    this.players.Remove(player);
                else if (e.ArrivedNode == this)
                    this.players.Add(player);
            }
        }

        public override string ToString()
        {
            return City.ToString();
        }

        public override int GetHashCode()
        {
            return City.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            INode compareNode = (INode)obj;
            return compareNode != null && compareNode.City != null ? compareNode.City == this.City : false;
        }
    }
}
