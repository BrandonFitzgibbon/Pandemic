using Engine.Contracts;
using Engine.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.CustomEventArgs
{
    public class PlayerMovedEventArgs : EventArgs
    {
        private Node departedNode;
        public Node DepartedNode
        {
            get { return departedNode; }
        }

        private Node arrivedNode;
        public Node ArrivedNode
        {
            get { return arrivedNode; }
        }

        public PlayerMovedEventArgs(Node departedCity, Node arrivedCity)
        {
            this.departedNode = departedCity;
            this.arrivedNode = arrivedCity;
        }
    }
}
