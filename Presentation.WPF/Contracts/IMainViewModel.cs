using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Contracts
{
    public interface IMainViewModel
    {
        IViewModelBase BoardViewModel { get; }
        IViewModelBase HandViewModel { get; }
        void RequestCard();
        void RequestInfection();
        void RequestNextPlayer();
    }
}
