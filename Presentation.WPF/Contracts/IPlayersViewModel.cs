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
        ICollection<IPlayerViewModel> Players { get; }
        IPlayer SelectedPlayer { get; set; }
        IPlayer CurrentPlayer { get; }
        ICollection<ICity> PossibleDestinations { get; }
        int ActionsLeft { get; set; }
        ICommand DriveCommand { get; }
    }
}
