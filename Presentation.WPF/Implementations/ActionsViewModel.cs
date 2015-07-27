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
        IContext<IPlayer> currentPlayerContext;

        public IDictionary<ICity, int> DriveDestinations
        {
            get { return actionContext.Context.DriveDestinations; }
        }

        public IDictionary<IDisease, int> DiseaseTreatmentOptions
        {
            get { return actionContext.Context.DiseaseTreatmentOptions; }
        }

        public ActionsViewModel(IContext<IActions> actionContext, IContext<IPlayer> currentPlayerContext)
        {
            this.actionContext = actionContext;
            this.currentPlayerContext = currentPlayerContext;
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
            RaiseChangeNotificationRequested();
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
                    treatDiseaseCommand = new RelayCommand(disease => TreatDisease((IDisease)disease), disease => CanTreatDisease((IDisease)disease));
                return treatDiseaseCommand;
            }
        }

        private void TreatDisease(IDisease disease)
        {
            actionContext.Context.TreatDisease.Invoke(disease, currentPlayerContext.Context.Location);
            RaiseChangeNotificationRequested();
        }

        private bool CanTreatDisease(IDisease disease)
        {
            return disease != null; 
        }
    }
}
