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
        IEnumerable<ICity> Cities { get; }
        IEnumerable<IPlayer> Players { get; }
        IPlayer CurrentPlayer { get; }
        IActions CurrentActions { get; }
        DrawCounter CurrentDrawCounter { get; }
        int NumberOfResearchStations { get; }
        IOutbreakCounter OutbreakCounter { get; }
        IInfectionRateCounter InfectionRateCounter { get; }
        IPlayerDeck PlayerDeck { get; }
        
        void NextPlayer();

        bool IsGameOn { get; }
    }
}
