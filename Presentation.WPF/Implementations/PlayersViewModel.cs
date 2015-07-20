using Engine.Contracts;
using Engine.Implementations;
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
    public class PlayersViewModel : ViewModelBase, IPlayersViewModel
    {
        private IGame game;
        private IContext<IPlayer> context;
        private IContext<IActions> actions;

        private ICollection<IPlayer> players;
        public ICollection<IPlayer> Players
        {
            get { return players; }
        }

        private IPlayer selectedPlayer;
        public IPlayer SelectedPlayer
        {
            get { return selectedPlayer; }
            set { selectedPlayer = value; context.Context = value; NotifyPropertyChanged(); }
        }

        private IPlayer currentPlayer;
        public IPlayer CurrentPlayer
        {
            get { return currentPlayer; }
            set { currentPlayer = value; actions.Context = new Actions(value); ActionsLeft = 4; NotifyPropertyChanged(); }
        }

        public ICollection<ICity> PossibleDestinations
        {
            get { return currentPlayer != null ? currentPlayer.Location.Connections.ToList() : null; }
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
                    RequestNextPlayer();
            }
        }
           
        public PlayersViewModel(IGame game, IContext<IActions> actions, IContext<IPlayer> context, ICollection<IPlayer> players)
        {
            this.game = game;
            this.actions = actions;
            this.context = context;
            this.players = players;
        }

        private void RequestNextPlayer()
        {
            game.DrawPhase();
            game.DrawPhase();
            game.InfectionPhase();
            game.NextPlayer();
            CurrentPlayer = game.CurrentPlayer;
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
            actions.Context.Drive(city);
            ActionsLeft = ActionsLeft - 1;
            NotifyPropertyChanged("PossibleDestinations");
            NotifyPropertyChanged("Players");
        }

        private bool CanDrive(ICity city)
        {
            return city != null;
        }

    }
}
