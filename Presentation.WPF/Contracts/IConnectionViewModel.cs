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
        double X1 { get; set; }
        double X2 { get; set; }
        double Y1 { get; set; }
        double Y2 { get; set; }
    }
}
