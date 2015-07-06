using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class Node : INode
    {
        private ICity city;
        public ICity City
        {
            get { return city; }
        }

        private IDisease disease;
        public IDisease Disease
        {
            get { return disease; }
        }

        private IList<ICounter> counters;
        public IList<ICounter> Counters
        {
            get { return counters; }
        }

        private IList<INode> connections;
        public IList<INode> Connections
        {
            get { return connections; }
        }

        private IList<IPlayer> players;
        public IList<IPlayer> Players
        {
            get { return players; }
        }

        private bool hasResearchStation;
        public bool HasResearchStation
        {
            get { return hasResearchStation; }
        }

        public Node(ICity city, IDisease disease, IList<ICounter> counters)
        {
            this.city = city;
            this.disease = disease;
            this.counters = counters;
            this.connections = new List<INode>();
            this.players = new List<IPlayer>();
        }

        public void BuildReasearchStation()
        {
            hasResearchStation = true;
        }

        public void DestroyResearchStation()
        {
            hasResearchStation = false;
        }

        public override string ToString()
        {
            return city.ToString();
        }

        public override int GetHashCode()
        {
            return city.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            INode node = (INode)obj;
            if (node != null)
                return city.Equals(node.City);
            else
                return false;
        }
    }
}
