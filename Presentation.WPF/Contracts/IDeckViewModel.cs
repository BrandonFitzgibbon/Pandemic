using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.WPF.Contracts
{
    public interface IDeckViewModel : IViewModelBase
    {
        IPlayerDeck PlayerDeck { get;}
        ICommand DrawPlayerCardCommand { get; }
        event EventHandler CardDrawn;
    }
}
