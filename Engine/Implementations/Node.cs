using Engine.Contracts;
using System;
using System.Collections.Generic;
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

        private ICounter counter;
        public ICounter Counter
        {
            get { return counter; }
        }

        private IList<INode> connections;
        public IList<INode> Connections
        {
            get { return connections; }
        }

        private bool hasResearchStation;
        public bool HasResearchStation
        {
            get { return hasResearchStation; }
            set { hasResearchStation = value; }
        }
    }
}
