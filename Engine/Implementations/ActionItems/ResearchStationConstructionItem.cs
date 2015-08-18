using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionItems
{
    public class ResearchStationConstructionItem
    {
        public Node Node { get; private set; }
        public CityCard CityCard { get; private set; }

        public ResearchStationConstructionItem(CityCard cityCard, Node node)
        {
            Node = node;
            CityCard = cityCard;
        }
    }
}
