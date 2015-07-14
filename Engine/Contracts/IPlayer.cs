using Engine.CustomEventArgs;
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
        ICity Location { get; }
        IHand Hand { get; }

        void SetStartingLocation(ICity startingCity);
        void Drive(ICity destination);
        void DirectFlight(ICity destination, ICityCard destinationCard);
        void CharterFlight(ICity destination, ICityCard locationCard);
        void ShuttleFlight(ICity destination);
        void BuildResearchStation(ICityCard locationCard, ICity dismantledStation = null);
        void TreatDisease(IDisease disease);
        void TakeKnowledge(IPlayer giver);
        void GiveKnowledge(IPlayer taker);
        void DiscoverCure(IList<ICard> cards, IDisease disease);

        event EventHandler<PlayerMovedEventArgs> Moved;
        event EventHandler<ResearchStationChangedEventArgs> ResearchStationChanged;
    }
}
