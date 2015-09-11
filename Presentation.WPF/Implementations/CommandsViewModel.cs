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
            boardViewModel.PawnViewModel.AnimateDrive(boardViewModel.PathAnimationViewModel.Data);
            RaiseChangeNotificationRequested(null);
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
            RaiseChangeNotificationRequested(new ChangeNotificationRequestedArgs(typeof(NodeViewModel)));
        }

        private RelayCommand selectPlayerCommand;
        public ICommand SelectPlayerCommand
        {
            get
            {
                if (selectPlayerCommand == null)
                    selectPlayerCommand = new RelayCommand(p => SelectPlayer((Player)p));
                return selectPlayerCommand;
            }
        }

        private void SelectPlayer(Player player)
        {
            selectedPlayer.Context = player;
            RaiseChangeNotificationRequested(null);
        }
    }
}
