using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionItems
{
    public class ResearchStationConstructionItem
    {
        public Node DeconstructionNode { get; private set; }
        public Node ConstructionNode { get; private set; }
        public CityCard CityCard { get; private set; }

        public ResearchStationConstructionItem(CityCard cityCard, Node constructionNode, Node deconstructionNode)
        {
            DeconstructionNode = deconstructionNode;
            ConstructionNode = constructionNode;
            CityCard = cityCard;
        }
    }
}
