using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionItems
{
    public class DriveDestinationItem
    {
        public Node Node { get; private set; }
        public int Cost { get; private set; }

        public DriveDestinationItem(Node node, int cost)
        {
            Node = node;
            Cost = cost;
        }

        internal void ChangeCost(int newCost)
        {
            Cost = newCost;
        }
    }
}
