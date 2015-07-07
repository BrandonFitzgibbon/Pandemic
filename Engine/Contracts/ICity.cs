using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface ICity
    {
        int Id { get; }
        string Name { get; }
        string Country { get; }
        int Population { get; }
        IEnumerable<ICity> Connections { get; }
        IDisease Disease { get; }
        IList<ICounter> Counters { get; }
        IList<IPlayer> Players { get; }
        bool HasResearchStation { get; }
    }
}
