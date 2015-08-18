using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionItems
{
    public class DiscoverCureItem
    {
        public List<CityCard> Cards { get; private set; }
        public Disease Disease { get; private set; }

        public DiscoverCureItem(Disease disease)
        {
            Disease = disease;
            Cards = new List<CityCard>();
        }
    }
}
