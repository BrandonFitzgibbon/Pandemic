using Engine.Implementations;
using Engine.Implementations.ActionItems;
using Presentation.WPF.Context;
using Presentation.WPF.Contracts;
using Presentation.WPF.CustomEventArgs;
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
        private IContext<Player> selectedPlayer;
        private IBoardViewModel boardViewModel;

        public CommandsViewModel(ActionManager actionManager, IContext<Player> selectedPlayer, IBoardViewModel boardViewModel, Notifier notifier)
        {
            if (actionManager == null)
                throw new ArgumentNullException();
            this.actionManager = actionManager;
            this.selectedPlayer = selectedPlayer;
            this.boardViewModel = boardViewModel;
            notifier.SubscribeToViewModel(this);
        }

        public DriveDestinationItem GetDriveDestinationItem(Node node)
        {
            if (node == null)
                return null;

            return actionManager.DriveDestinations.SingleOrDefault(i => i.Node == node);
        }

        private RelayCommand driveCommand;
        public ICommand DriveCommand
        {
            get
            {
                if (driveCommand == null)
                    driveCommand = new RelayCommand(node => Drive((Node)node), node => CanDrive((Node)node));
                return driveCommand;
            }
        }

        private bool CanDrive(Node node)
        {
            if (node == null)
                return false;
            else
            {
                DriveDestinationItem ddi = actionManager.DriveDestinations.SingleOrDefault(i => i.Node == node);
                return actionManager.CanDrive(ddi);
            }
        }

        private void Drive(Node node)
        {
            DriveDestinationItem ddi = actionManager.DriveDestinations.SingleOrDefault(i => i.Node == node);
            actionManager.Drive(ddi);
            RaiseChangeNotificationRequested(null);
            boardViewModel.PawnViewModel.AnimateDrive(boardViewModel.PathAnimationViewModel.Data);
        }

        public DispatchItem GetDispatchItem(Node node)
        {
            if (node == null || actionManager.DispatchDestinations == null)
                return null;

            return actionManager.DispatchDestinations.SingleOrDefault(i => i.DispatchDestination == node && i.Player == boardViewModel.SelectedPlayerViewModel.Player);
        }

        private RelayCommand dispatchCommand;
        public ICommand DispatchCommand
        {
            get
            {
                if (dispatchCommand == null)
                    dispatchCommand = new RelayCommand(dpi => Dispatch((DispatchItem)dpi), dpi => CanDispatch((DispatchItem)dpi));
                return dispatchCommand;
            }
        }

        private bool CanDispatch(DispatchItem dpi)
        {
            return actionManager.CanDispatch(dpi);
        }

        private void Dispatch(DispatchItem dpi)
        {
            actionManager.Dispatch(dpi);
            RaiseChangeNotificationRequested(null);
            boardViewModel.PawnViewModel.AnimateDrive(boardViewModel.PathAnimationViewModel.Data);
        }

        public DirectFlightItem GetDirectFlightItem(Node node)
        {
            if (node == null || actionManager.DirectFlightDestinations == null)
                return null;

            return actionManager.DirectFlightDestinations.SingleOrDefault(i => i.CityCard.Node == node);
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
            RaiseChangeNotificationRequested(null);
            boardViewModel.PawnViewModel.AnimateDrive(boardViewModel.PathAnimationViewModel.Data);
        }
    }
}
