using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionItems
{
    public class ShuttleFlightItem
    {
        public Node Node { get; private set; }

        public ShuttleFlightItem(Node node)
        {
            Node = node;
        }
    }
}
