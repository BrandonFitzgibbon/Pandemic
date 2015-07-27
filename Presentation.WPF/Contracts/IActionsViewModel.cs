using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.WPF.Contracts
{
    public interface IActionsViewModel : IViewModelBase
    {
        IDictionary<ICity, int> DriveDestinations { get; }
        IDictionary<IDisease, int> DiseaseTreatmentOptions { get; }
        ICommand DriveCommand { get; }
        ICommand TreatDiseaseCommand { get; }
    }
}
