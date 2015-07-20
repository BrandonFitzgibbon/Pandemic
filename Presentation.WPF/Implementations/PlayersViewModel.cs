using Engine.Contracts;
using Engine.Implementations;
using Presentation.WPF.Context;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.WPF.Implementations
{
    public class PlayersViewModel : ViewModelBase, IPlayersViewModel
    {
        private IGame game;
        private IMainViewModel mainViewModel;

        private IContext<IPlayer> playerContext;
        private IContext<IActions> actionsContext;

        private ICollection<IPlayerViewModel> players;
        public ICollection<IPlayerViewModel> Players
        {
            get { return players; }
        }

        private IPlayer selectedPlayer;
        public IPlayer SelectedPlayer
        {
            get { return selectedPlayer; }
            set { selectedPlayer = value; playerContext.Context = value; NotifyPropertyChanged(); }
        }

        public IPlayer CurrentPlayer
        {
            get { return game.CurrentPlayer; }
        }

        public ICollection<ICity> PossibleDestinations
        {
            get { return CurrentPlayer != null ? CurrentPlayer.Location.Connections.ToList() : null; }
        }

        private int actionsLeft;
        public int ActionsLeft
        {
            get { return actionsLeft; }
            set 
            {
                if (value > 0)
                {
                    actionsLeft = value;
                    NotifyPropertyChanged();
                }
                else
                {
                    mainViewModel.RequestCard();
                    mainViewModel.RequestCard();
                    mainViewModel.RequestInfection();
                    mainViewModel.RequestNextPlayer();
                    NotifyPropertyChanged("CurrentPlayer");
                    actionsLeft = 4;
                    NotifyPropertyChanged("ActionsLeft");
                    actionsContext.Context = new Actions(game.CurrentPlayer);
                }
                    
            }
        }
           
        public PlayersViewModel(IMainViewModel mainViewModel, IGame game, IContext<IActions> actions, IContext<IPlayer> context, ICollection<IPlayerViewModel> viewModels, ICollection<IPlayer> players)
        {
            this.mainViewModel = mainViewModel;
            this.game = game;
            this.actionsContext = actions;
            this.playerContext = context;
            this.players = viewModels;
            actionsLeft = 4;
            actions.Context = new Actions(game.CurrentPlayer);
        }

        private RelayCommand driveCommand;
        public ICommand DriveCommand
        {
            get 
            {
                if (driveCommand == null)
                    driveCommand = new RelayCommand(city => Drive((ICity)city), city => CanDrive((ICity)city));
                return driveCommand;
            }
        }

        private void Drive(ICity city)
        {
            actionsContext.Context.Drive(city);
            ActionsLeft = ActionsLeft - 1;
            NotifyPropertyChanged("PossibleDestinations");
            NotifyPropertyChanged("Players");
            foreach (IPlayerViewModel pvm in players)
            {
                pvm.NotifyChanges();
            }
        }

        private bool CanDrive(ICity city)
        {
            return city != null;
        }

    }
}
