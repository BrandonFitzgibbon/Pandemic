using Engine.Implementations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Contracts
{
    public interface INodeViewModel : IViewModelBase
    {
        IEnumerable<INodeDiseaseCounterViewModel> NodeCounters { get; }
        string Name { get; }
        Disease Disease { get; }
        bool ResearchStation { get; }
        ICollection<Player> Players { get; }
    }
}
