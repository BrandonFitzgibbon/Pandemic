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
        IPlayerViewModel SelectedPlayer { get; set; }
        IPlayer CurrentPlayer { get; }
        event EventHandler RequestStateUpdate;
    }
}
