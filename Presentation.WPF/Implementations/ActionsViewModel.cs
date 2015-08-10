using Engine.Implementations;
using Engine.Implementations.ActionItems;
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
        private IContext<ActionManager> actionManager;

        public ActionManager ActionManager
        {
            get { return actionManager != null ? actionManager.Context : null; }
        }

        public IEnumerable<DriveDestinationItem> DriveDestinations
        {
            get { return ActionManager != null ? ActionManager.DriveDestinations : null; }
        }

        public IEnumerable<DirectFlightItem> DirectFlightDestinations
        {
            get { return ActionManager != null ? ActionManager.DirectFlightDestinations : null; }
        }

        public IEnumerable<TreatDiseaseItem> TreatmentTargets
        {
            get { return ActionManager != null ? ActionManager.TreatmentTargets : null; }
        }

        public ActionsViewModel(IContext<ActionManager> actionManager)
        {
            this.actionManager = actionManager;
            this.actionManager.ContextChanged += ContextChanged;
        }

        private void ContextChanged(object sender, ContextChangedEventArgs<ActionManager> e)
        {
            NotifyPropertyChanged("ActionManager");
        }

        private RelayCommand driveCommand;
        public ICommand DriveCommand
        {
            get 
            {
                if (driveCommand == null)
                    driveCommand = new RelayCommand(ddi => Drive((DriveDestinationItem)ddi), ddi => CanDrive((DriveDestinationItem)ddi));
                return driveCommand;
            }
        }

        private bool CanDrive(DriveDestinationItem ddi)
        {
            return ActionManager.CanDrive(ddi);
        }

        private async void Drive(DriveDestinationItem ddi)
        {
            await Task.Run(() => ActionManager.Drive(ddi));
            RaiseChangeNotificationRequested();
        }

        private RelayCommand treatCommand;
        public ICommand TreatCommand
        {
            get 
            {
                if (treatCommand == null)
                    treatCommand = new RelayCommand(tdi => Treat((TreatDiseaseItem)tdi), tdi => CanTreat((TreatDiseaseItem)tdi));
                return treatCommand;
            }
        }

        private bool CanTreat(TreatDiseaseItem tdi)
        {
           return ActionManager.CanTreatDisease(tdi);
        }

        private void Treat(TreatDiseaseItem tdi)
        {
            ActionManager.TreatDisease(tdi);
            RaiseChangeNotificationRequested();
        }
    }
}
