using Engine.Contracts;
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
        private IContext<IPlayer> context;

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
            set { currentPlayer = value; NotifyPropertyChanged(); }
        }   
           
        public PlayersViewModel(IContext<IPlayer> context, ICollection<IPlayer> players)
        {
            this.context = context;
            this.players = players;
        }
    }
}
