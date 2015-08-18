using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class ResearchStationCounter
    {
        public int Count { get; private set; }

        internal ResearchStationCounter()
        {
            Count = 6;
        }

        internal void BuildResearchStation(Node node)
        {
            if (node != null)
            {
                Count--;
                node.ResearchStation = true;
            }
        }

        internal void MoveResearchStation(Node oldNode, Node newNode)
        {
            if (oldNode != null && newNode != null)
            {
                oldNode.ResearchStation = false;
                newNode.ResearchStation = true;
            }
        }
    }
}
