using Engine.Implementations;
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
        IEnumerable<IDiseaseCounter> DiseaseCounters { get; }
        IEnumerable<INode> Nodes { get; }
        IEnumerable<INodeDiseaseCounter> NodeCounters { get; }
        IEnumerable<IPlayer> Players { get; }
        IDeck PlayerDeck { get; }
        IDeck InfectionDeck { get; }
        ICount OutbreakCounter { get; }
        IInfectionRateCounter InfectionRateCounter { get; }
    }
}
