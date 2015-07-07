using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IPlayer
    {
        string Name { get; }
        INode Location { get; set; }
        IHand Hand { get; }

        void Drive(INode destination);
        void DirectFlight(INode destination);
        void CharterFlight(INode destination);
        void ShuttleFlight(INode destination);
        void BuildResearchStation();
        void TreatDisease();
        void TakeKnowledge(IPlayer giver);
        void GiveKnowledge(IPlayer taker, ICard card);
        void DiscoverCure(IList<ICard> cards);
    }
}
