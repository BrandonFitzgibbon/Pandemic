using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionItems
{
    public class ShareKnowledgeItem
    {
        public CityCard CityCard { get; private set; }
        public Player Giver { get; private set; }
        public Player Taker { get; private set; }

        public ShareKnowledgeItem(Player giver, Player taker, CityCard cityCard)
        {
            Giver = giver;
            Taker = taker;
            CityCard = cityCard;
        }
    }
}
