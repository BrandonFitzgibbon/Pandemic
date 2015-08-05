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
        IEnumerable<INode> Connections { get; }
        IEnumerable<IPlayer> Players { get; }
        bool ResearchStation { get; }
    }
}
