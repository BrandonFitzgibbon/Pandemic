using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Contracts
{
    public interface IPlayersViewModel : IViewModelBase
    {
        ICollection<IPlayer> Players { get; }
        IPlayer SelectedPlayer { get; set; }
        IPlayer CurrentPlayer { get; set; }
    }
}
