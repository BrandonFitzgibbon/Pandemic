using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionItems
{
    public class OperationsRelocationItem
    {
        public Node Destination { get; private set; }
        public CityCard DiscardOption { get; set; }

        public OperationsRelocationItem(Node destination)
        {
            Destination = destination;
        }
    }
}
