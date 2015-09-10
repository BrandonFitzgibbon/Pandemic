using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Contracts
{
    public interface IConnectionViewModel : IViewModelBase
    {
        IAnchorViewModel Originator { get; }
        IAnchorViewModel Destination { get; }
        double Opacity { get; set; }
    }
}
