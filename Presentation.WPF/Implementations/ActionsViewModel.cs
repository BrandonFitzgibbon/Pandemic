using Engine.Contracts;
using Presentation.WPF.Context;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.WPF.Implementations
{
    public class ActionsViewModel : ViewModelBase, IActionsViewModel
    {
        IContext<IActions> actionContext;

        public IDictionary<ICity, int> DriveDestinations
        {
            get { return actionContext.Context.DriveDestinations; }
        }

        public ActionsViewModel(IContext<IActions> actionContext)
        {
            this.actionContext = actionContext;
        }

        private RelayCommand driveCommand;
        public ICommand DriveCommand
        {
            get
            {
                if (driveCommand == null)
                    driveCommand = new RelayCommand(driveDestination => Drive((ICity)driveDestination), driveDestination => CanDrive((ICity)driveDestination));
                return driveCommand;
            }
        }

        private void Drive(ICity destination)
        {
            actionContext.Context.Drive.Invoke(destination);
            this.NotifyChanges();
        }

        private bool CanDrive(ICity destination)
        {
            return actionContext.Context.CanDrive(destination);
        }

        private RelayCommand treatDiseaseCommand;
        public ICommand TreatDiseaseCommand
        {
            get
            {
                if (treatDiseaseCommand == null)
                    treatDiseaseCommand = new RelayCommand(disease => TreatDisease((IDiseaseCounterViewModel)disease), disease => CanTreatDisease((IDiseaseCounterViewModel)disease));
                return treatDiseaseCommand;
            }
        }

        private void TreatDisease(IDiseaseCounterViewModel disease)
        {
            
        }

        private bool CanTreatDisease(IDiseaseCounterViewModel disease)
        {
            return true;
        }
    }
}
