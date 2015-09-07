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
    public class NextTurnViewModel : ViewModelBase, INextTurnViewModel
    {
        private Game game;
        private IContext<Player> currentPlayer;

        public NextTurnViewModel(Game game, IContext<Player> currentPlayer, Notifier notifier)
        {
            this.game = game;
            this.currentPlayer = currentPlayer;
            notifier.SubscribeToViewModel(this);
        }

        private RelayCommand nextTurnCommand;
        public ICommand NextTurnCommand
        {
            get
            {
                if (nextTurnCommand == null)
                    nextTurnCommand = new RelayCommand(a => NextTurn(), p => CanNextTurn());
                return nextTurnCommand;
            }
        }

        public bool CanNextTurn()
        {
            return game.CanNextPlayer;
        }

        public void NextTurn()
        {
            game.NextPlayer();
            currentPlayer.Context = game.CurrentPlayer;
            if (TurnChanged != null) TurnChanged(this, EventArgs.Empty);
            RaiseChangeNotificationRequested(null);
        }

        public event EventHandler TurnChanged;
    }
}
