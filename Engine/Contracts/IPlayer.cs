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
        event EventHandler<PlayerMovedEventArgs> Moved;
        event EventHandler<ResearchStationChangedEventArgs> ResearchStationChanged;
    }
}
