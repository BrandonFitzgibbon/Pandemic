using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Contracts
{
    public interface IGameStatusViewModel : IViewModelBase
    {
        int Outbreaks { get; }
        int InfectionRate { get; }
        bool InfectionCounterOne { get; }
        bool InfectionCounterTwo { get; }
        bool InfectionCounterThree { get; }
        bool InfectionCounterFour { get; }
        bool InfectionCounterFive { get; }
        bool InfectionCounterSix { get; }
        bool InfectionCounterSeven { get; }
        int YellowCounters { get; }
        bool YellowCured { get; }
        bool YellowEradicated { get; }
        int RedCounters { get; }
        bool RedCured { get; }
        bool RedEradicated { get; }
        int BlueCounters { get; }
        bool BlueCured { get; }
        bool BlueEradicated { get; }
        int BlackCounters { get; }
        bool BlackCured { get; }
        bool BlackEradicated { get; }
        void NotifyChanges();
    }
}
