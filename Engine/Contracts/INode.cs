using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface INode
    {
        ICity City { get; }
        IDisease Disease { get; }
        IList<ICounter> Counters { get; }
        IList<INode> Connections { get; }
        IList<IPlayer> Players { get; }
        bool HasResearchStation { get; }
        void BuildReasearchStation();
        void DestroyResearchStation();
    }
}
