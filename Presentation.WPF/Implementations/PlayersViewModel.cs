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

        public IEnumerable<Player> Players { get; private set; }

        public PlayersViewModel(IEnumerable<Player> players, IContext<Player> currentPlayer)
        {
            this.currentPlayer = currentPlayer;
            this.currentPlayer.ContextChanged += ContextChanged;
            Players = players;
        }

        private void ContextChanged(object sender, ContextChangedEventArgs<Player> e)
        {
            NotifyPropertyChanged("CurrentPlayer");
        }
    }
}
