using Engine.Implementations;
using Presentation.WPF.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Contracts
{
    public interface IBoardViewModel : IViewModelBase
    {
        IPlayerViewModel SelectedPlayerViewModel { get; }
        IPlayerViewModel CurrentPlayerViewModel { get; }
        IEnumerable<IPlayerViewModel> PlayerViewModels { get; }
        ICommandsViewModel CommandsViewModel { get; }
        IEnumerable<IConnectionViewModel> ConnectionViewModels { get; }
        IEnumerable<IAnchorViewModel> AnchorViewModels { get; }
        IPathAnimationViewModel PathAnimationViewModel { get; }
        IPawnViewModel PawnViewModel { get; }
        void SelectPlayer(int turnOrder);
    }
}
