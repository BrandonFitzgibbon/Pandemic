using Engine.Contracts;
using Engine.Implementations;
using Presentation.WPF.Context;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Implementations
{
    public class PlayersViewModel : ViewModelBase, IPlayersViewModel
    {
        private IContext<Player> currentPlayer;

        public Player CurrentPlayer 
        { 
            get { return currentPlayer != null ? currentPlayer.Context : null; } 
        }

        private IContext<Player> selectedPlayer;

        private IEnumerable<IPlayerViewModel> playerViewModels;
        public IEnumerable<IPlayerViewModel> PlayerViewModels
        {
            get { return playerViewModels; }
        }

        private IPlayerViewModel selectedPlayerViewModel;
        public IPlayerViewModel SelectedPlayerViewModel
        {
            get { return selectedPlayerViewModel; }
            set { selectedPlayerViewModel = value; selectedPlayer.Context = selectedPlayerViewModel.Player; NotifyPropertyChanged(); }
        }

        public PlayersViewModel(IContext<Player> currentPlayer, IContext<Player> selectedPlayer, IEnumerable<IPlayerViewModel> playerViewModels, Notifier notifier)
        {
            this.currentPlayer = currentPlayer;
            this.currentPlayer.ContextChanged += ContextChanged;
            this.selectedPlayer = selectedPlayer;
            this.playerViewModels = playerViewModels;
            notifier.SubscribeToViewModel(this);
        }

        private void ContextChanged(object sender, ContextChangedEventArgs<Player> e)
        {
            NotifyPropertyChanged("CurrentPlayer");
        }
    }
}
