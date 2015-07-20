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
        int YellowCounters { get; }
        int RedCounters { get; }
        int BlueCounters { get; }
        int BlackCounters { get; }
    }
}
