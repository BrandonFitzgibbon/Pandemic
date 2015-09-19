using Engine.Implementations;
using Engine.Implementations.ActionItems;
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
        DriveDestinationItem DriveDestinationItem { get; }
        DispatchItem DispatchItem { get; }
        double Left { get; set; }
        double Top { get; set; }

    }
}
