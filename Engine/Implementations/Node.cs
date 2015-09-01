using Engine.Contracts;
using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class Node
    {
        public City City { get; private set; }
        public Disease Disease { get; private set; }
        public bool ResearchStation { get; internal set; }

        private List<Node> connections;
        public IEnumerable<Node> Connections
        {
            get { return connections; }
        }

        private List<Player> players;
        public IEnumerable<Player> Players
        {
            get { return players; }
        }

        public Node(City city, Disease disease)
        {
            City = city;
            Disease = disease;
            connections = new List<Node>();
            players = new List<Player>();
        }

        public void Connect(Node node)
        {
            this.connections.Add(node);
        }

        public void SubscribeToMover(Player mover)
        {
            mover.Moved += moverMoved;
        }

        private void moverMoved(object sender, PlayerMovedEventArgs e)
        {
            Player player = sender as Player;
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
            return City.Name;
        }

        public override int GetHashCode()
        {
            return City.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Node compareNode = obj as Node;
            return compareNode != null && compareNode.City != null ? compareNode.City == this.City : false;
        }
    }
}
