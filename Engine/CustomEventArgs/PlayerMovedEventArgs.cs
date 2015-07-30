using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.CustomEventArgs
{
    public class PlayerMovedEventArgs : EventArgs
    {
        private INode departedNode;
        public INode DepartedNode
        {
            get { return departedNode; }
        }

        private INode arrivedNode;
        public INode ArrivedNode
        {
            get { return arrivedNode; }
        }

        public PlayerMovedEventArgs(INode departedCity, INode arrivedCity)
        {
            this.departedNode = departedCity;
            this.arrivedNode = arrivedCity;
        }
    }
}
