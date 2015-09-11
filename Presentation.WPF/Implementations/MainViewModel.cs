using Engine.Contracts;
using Engine.CustomEventArgs;
using Engine.Factories;
using Engine.Implementations;
using Engine.Implementations.ActionItems;
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
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        private Game game;
        private ActionCardManager actionCardManager;
        private ActionManager actionManager;
        private DrawManager drawManager;
        private InfectionManager infectionManager;
        private Notifier notifier;
        private IDataAccess data;

        private IContext<Player> currentPlayer;
        private IContext<Player> selectedPlayer;
        private IContext<StringBuilder> messageContext;

        private IBoardViewModel boardViewModel;
        public IBoardViewModel BoardViewModel
        {
            get { return boardViewModel; }
            set { boardViewModel = value; NotifyPropertyChanged(); }
        }

        private IMessageViewModel messageViewModel;
        public IMessageViewModel MessageViewModel
        {
            get { return messageViewModel; }
            set { messageViewModel = value; NotifyPropertyChanged(); }
        }

        private IEpidemicViewModel epidemicViewModel;
        public IEpidemicViewModel EpidemicViewModel
        {
            get { return epidemicViewModel; }
            set { epidemicViewModel = value; NotifyPropertyChanged(); }
        }

        private IDiscardViewModel discardViewModel;
        public IDiscardViewModel DiscardViewModel
        {
            get { return discardViewModel; }
            set { discardViewModel = value; NotifyPropertyChanged(); }
        }

        public MainViewModel()
        {
            data = new DataAccess.Data();
            game = new Game(data, new List<string> { "Jessica", "Jack", "John", "Jane" }, Difficulty.Introductory);
            notifier = new Notifier();

            actionCardManager = game.ActionCardManager;
            actionManager = game.ActionManager;
            drawManager = game.DrawManager;
            infectionManager = game.InfectionManager;

            currentPlayer = new ObjectContext<Player>();
            currentPlayer.Context = game.CurrentPlayer;

            selectedPlayer = new ObjectContext<Player>();

            messageContext = new ObjectContext<StringBuilder>();
            messageContext.Context = new StringBuilder();

            BoardViewModel = new BoardViewModel(game, currentPlayer, selectedPlayer, notifier);
            MessageViewModel = new MessageViewModel(messageContext, game.NodeCounters);

            foreach (Player player in game.Players)
            {
                player.Hand.DiscardManager.Block += DiscardManagerBlock;
                player.Hand.DiscardManager.Release += DiscardManagerRelease;
            }

            drawManager.EpidemicDrawn += EpidemicDrawn;

            game.GameOver += Game_GameOver;
            game.GameWon += Game_GameWon;
        }

        private void Game_GameWon(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Game_GameOver(object sender, EventArgs e)
        {
            game = null;
        }

        private void DiscardManagerBlock(object sender, EventArgs e)
        {
            DiscardViewModel dvm = new DiscardViewModel((DiscardManager)sender);
            DiscardViewModel = dvm;
        }

        private void DiscardManagerRelease(object sender, EventArgs e)
        {
            DiscardViewModel = null;
        }

        private void EpidemicDrawn(object sender, EventArgs e)
        {
            EpidemicViewModel evm = new EpidemicViewModel(drawManager.EpidemicManager, messageContext);
            EpidemicViewModel = evm;
        }
    }
}
