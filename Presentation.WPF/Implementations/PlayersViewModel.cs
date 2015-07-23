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

        private IEnumerable<IDiseaseCounterViewModel> diseaseCounters;
        public IEnumerable<IDiseaseCounterViewModel> DiseaseCounters
        {
            get { return diseaseCounters; }
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
           
        public PlayersViewModel(IContext<IPlayer> currentPlayerContext, IContext<IPlayer> selectedPlayerContext, IContext<IActions> actionsContext, ICollection<IPlayerViewModel> playerViewModels, ICollection<IDiseaseCounterViewModel> diseaseCounterViewModels)
        {
            this.actionsContext = actionsContext;
            this.selectedPlayerContext = selectedPlayerContext;
            this.currentPlayerContext = currentPlayerContext;
            this.players = playerViewModels;
            this.diseaseCounters = diseaseCounterViewModels;

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

        public event EventHandler RequestStateUpdate;
    }
}
