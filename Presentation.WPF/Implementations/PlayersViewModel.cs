﻿using Engine.Contracts;
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

        private IEnumerable<IPlayerViewModel> playerViewModels;
        public IEnumerable<IPlayerViewModel> PlayerViewModels
        {
            get { return playerViewModels; }
        }

        public PlayersViewModel(IContext<Player> currentPlayer, IEnumerable<IPlayerViewModel> playerViewModels, Notifier notifier)
        {
            this.currentPlayer = currentPlayer;
            this.playerViewModels = playerViewModels;
            notifier.SubscribeToViewModel(this);
        }
    }
}
