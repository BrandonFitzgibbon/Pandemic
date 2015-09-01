using Engine.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.CustomEventArgs
{
    public class ResearchStationChangedEventArgs : EventArgs
    {
        public Node Node { get; private set; }

        public ResearchStationChangedEventArgs(Node node)
        {
            Node = node;
        }
    }
}
