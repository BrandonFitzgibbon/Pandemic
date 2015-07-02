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
        public IReadOnlyCollection<INode> Connections
        {
            get { return connections.ToList().AsReadOnly(); }
        }

        private bool hasResearchStation;
        public bool HasResearchStation
        {
            get { return hasResearchStation; }
            set { hasResearchStation = value; }
        }

        public Node(ICity city, IDisease disease, IList<ICounter> counters)
        {
            this.city = city;
            this.disease = disease;
            this.counters = counters;
            this.connections = new List<INode>();
        }

        public void FormConnection(INode connection)
        {
            if(connection != null && !connections.Contains(connection))
                connections.Add(connection);
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
