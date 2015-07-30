using Engine.Contracts;
using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class Node : INode, IConnectable
    {
        public ICity City { get; private set; }
        public IDisease Disease { get; private set; }
        public bool ResearchStation { get; private set; }

        public Node(ICity city, IDisease disease)
        {
            City = city;
            Disease = disease;
            connections = new List<INode>();
        }

        private List<INode> connections;
        public IEnumerable<INode> Connections
        {
            get { return connections; }
        }

        public void Connect(INode node)
        {
            this.connections.Add(node);
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
