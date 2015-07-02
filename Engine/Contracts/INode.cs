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
        IReadOnlyCollection<INode> Connections { get; }
        bool HasResearchStation { get; set; }
        void FormConnection(INode connection);
    }
}
