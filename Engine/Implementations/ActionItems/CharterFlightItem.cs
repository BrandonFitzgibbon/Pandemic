using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionItems
{
    public class CharterFlightItem
    {
        public CityCard CityCard { get; private set; }
        public Node Node { get; private set; }

        public CharterFlightItem(CityCard cityCard, Node node)
        {
            CityCard = cityCard;
            Node = node;
        }
    }
}
