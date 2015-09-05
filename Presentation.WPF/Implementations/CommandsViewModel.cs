using Engine.Implementations;
using Engine.Implementations.ActionItems;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.WPF.Implementations
{
    public class CommandsViewModel : ViewModelBase, ICommandsViewModel
    {
        private ActionManager actionManager;

        public CommandsViewModel(ActionManager actionManager, Notifier notifier)
        {
            if (actionManager == null)
                throw new ArgumentNullException();
            this.actionManager = actionManager;
            notifier.SubscribeToViewModel(this);
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
            return actionManager.CanDrive(ddi);
        }

        private void Drive(DriveDestinationItem ddi)
        {
            actionManager.Drive(ddi);
            RaiseChangeNotificationRequested();
        }

        private RelayCommand directFlightCommand;
        public ICommand DirectFlightCommand
        {
            get
            {
                if (directFlightCommand == null)
                    directFlightCommand = new RelayCommand(dfi => DirectFlight((DirectFlightItem)dfi), dfi => CanDirectFlight((DirectFlightItem)dfi));
                return directFlightCommand;
            }
        }

        private bool CanDirectFlight(DirectFlightItem dfi)
        {
            return actionManager.CanDirectFlight(dfi);
        }

        public void DirectFlight(DirectFlightItem dfi)
        {
            actionManager.DirectFlight(dfi);
            RaiseChangeNotificationRequested();
        }
    }
}
