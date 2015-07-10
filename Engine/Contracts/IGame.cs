using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IGame
    {
        IEnumerable<IDisease> Diseases { get; }
        IEnumerable<ICity> Cities { get; }
        IEnumerable<IPlayer> Players { get; }
        int NumberOfResearchStations { get; }
    }
}
