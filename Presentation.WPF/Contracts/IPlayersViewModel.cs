using Engine.Contracts;
using Engine.Implementations;
using Presentation.WPF.Context;
using Presentation.WPF.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Contracts
{
    public interface IPlayersViewModel : IViewModelBase
    {
        Player CurrentPlayer { get; }
        IEnumerable<IPlayerViewModel> PlayerViewModels { get; }
        IPlayerViewModel SelectedPlayerViewModel { get; set; }
    }
}
