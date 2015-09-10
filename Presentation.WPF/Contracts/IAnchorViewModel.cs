using Engine.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Presentation.WPF.Contracts
{
    public interface IAnchorViewModel : IViewModelBase
    {
        Node Node { get; }
        double Left { get; set; }
        double Top { get; set; }
        SolidColorBrush Background { get; set; }
    }
}
