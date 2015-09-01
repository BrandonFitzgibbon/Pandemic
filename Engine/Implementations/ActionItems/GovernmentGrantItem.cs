using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionItems
{
    public class GovernmentGrantItem
    {
        public Node ConstructionNode { get; private set; }
        public Node DeconstructionNode { get; private set; }

        public GovernmentGrantItem(Node constructionNode, Node deconstructionNode)
        {
            ConstructionNode = constructionNode;
            DeconstructionNode = deconstructionNode;
        }
    }
}
