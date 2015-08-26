using Engine.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.CustomEventArgs
{
    public class PreventionEventArgs : EventArgs
    {
        public Player Player { get; private set; }
        public Disease Disease { get; private set; }
        public Node Node { get; private set; }

        public PreventionEventArgs(Player player, Disease disease, Node node)
        {
            Player = player;
            Disease = disease;
            Node = node;
        }
    }
}
