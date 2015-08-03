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
        bool ResearchStation { get; }
        IEnumerable<INode> Connections { get; }
        IEnumerable<IPlayer> Players { get; }

        void SubscribeToMover(IMove mover);
    }
}
