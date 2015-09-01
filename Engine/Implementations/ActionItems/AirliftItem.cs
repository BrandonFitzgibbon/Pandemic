using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionItems
{
    public class AirliftItem
    {
        public Player Player { get; private set; }
        public Node Node { get; private set; }

        public AirliftItem(Player player, Node node)
        {
            Player = player;
            Node = node;
        }
    }
}
