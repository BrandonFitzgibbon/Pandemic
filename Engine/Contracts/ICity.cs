using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface ICity
    {
        string Name { get; }
        string Country { get; }
        int Population { get; }
        IEnumerable<ICity> Connections { get; }
        IDisease Disease { get; }
        IList<ICounter> Counters { get; }
        IList<IPlayer> PlayersInCity { get; }
        bool HasResearchStation { get; }
        void FormConnection(ICity connection);
    }
}
