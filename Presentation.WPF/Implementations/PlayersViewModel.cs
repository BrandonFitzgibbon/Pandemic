using Engine.Contracts;
using Engine.Implementations;
using Presentation.WPF.Context;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Validation;

namespace Presentation.WPF.Implementations
{
    public class PlayersViewModel : ViewModelBase, IPlayersViewModel
    {
        private IContext<IPlayer> currentPlayerContext;
        private IContext<IPlayer> selectedPlayerContext;
        private IContext<IActions> actionsContext;

        private IEnumerable<IPlayerViewModel> players;
        public IEnumerable<IPlayerViewModel> Players
        {
            get { return players; }
        }

        private IPlayerViewModel selectedPlayer;
        public IPlayerViewModel SelectedPlayer
        {
            get { return selectedPlayer; }
            set { selectedPlayer = value; selectedPlayerContext.Context = value.Player; NotifyPropertyChanged(); }
        }

        public IPlayer CurrentPlayer
        {
            get { return currentPlayerContext.Context; }
        }

        public int CurrentPlayerActionsRemaining
        {
            get { return actionsContext.Context.ActionCount; }
        }
           
        public PlayersViewModel(IContext<IPlayer> currentPlayerContext, IContext<IPlayer> selectedPlayerContext, IContext<IActions> actionsContext, ICollection<IPlayerViewModel> playerViewModels)
        {
            this.actionsContext = actionsContext;
            this.selectedPlayerContext = selectedPlayerContext;
            this.currentPlayerContext = currentPlayerContext;
            this.players = playerViewModels;

            currentPlayerContext.ContextChanged += currentPlayerContextChanged;
            actionsContext.ContextChanged += actionsContextChanged;
        }

        private void actionsContextChanged(object sender, ContextChangedEventArgs<IActions> e)
        {
            NotifyChanges();
        }

        private void currentPlayerContextChanged(object sender, ContextChangedEventArgs<IPlayer> e)
        {
            NotifyChanges();
        }
    }
}
