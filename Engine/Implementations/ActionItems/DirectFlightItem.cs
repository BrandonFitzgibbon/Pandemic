using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionItems
{
    public class DirectFlightItem
    {
        public CityCard CityCard { get; private set; }

        public DirectFlightItem(CityCard cityCard)
        {
            CityCard = cityCard;
        }
    }
}
