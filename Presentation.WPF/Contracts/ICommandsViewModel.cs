using Engine.Implementations;
using Engine.Implementations.ActionItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.WPF.Contracts
{
    public interface ICommandsViewModel : IViewModelBase
    {
        DriveDestinationItem GetDriveDestinationItem(Node node);
        DispatchItem GetDispatchItem(Node node);
        DirectFlightItem GetDirectFlightItem(Node node);
        ICommand DispatchCommand { get; }
    }
}
