using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.WPF.Contracts
{
    public interface IPlayersViewModel : IViewModelBase
    {
        IEnumerable<IPlayerViewModel> Players { get; }
        IEnumerable<IDiseaseCounterViewModel> LocationDiseaseCounters { get; }
        IPlayerViewModel SelectedPlayer { get; set; }
        IPlayer CurrentPlayer { get; }
        int ActionsLeft { get; set; }
        ICommand DriveCommand { get; }
        ICommand TreatDiseaseCommand { get; }
        event EventHandler RequestPlayerChange;
        event EventHandler RequestStateUpdate;
    }
}
