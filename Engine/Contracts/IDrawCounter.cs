using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IDrawCounter
    {
        int DrawCount { get; }
        int InfectionCount { get; }

        ICard DrawPlayerCard();
        IInfectionCard DrawInfectionCard();

        event EventHandler DrawsDepleted;
        event EventHandler InfectionsDepleted;
    }
}
