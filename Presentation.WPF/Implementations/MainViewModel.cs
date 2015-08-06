using Engine.Contracts;
using Engine.Factories;
using Engine.Implementations;
using Presentation.WPF.Context;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Implementations
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        private Game game;
        private IDataAccess data;
        private IContext<Player> currentPlayer;
        private IContext<ActionManager> actionManager;

        private GameStatusViewModel gameStatusViewModel;
        public GameStatusViewModel GameStatusViewModel
        {
            get { return gameStatusViewModel; }
            set { gameStatusViewModel = value; NotifyPropertyChanged(); }
        }

        private PlayersViewModel playersViewModel;
        public PlayersViewModel PlayersViewModel
        {
            get { return playersViewModel; }
            set { playersViewModel = value; NotifyPropertyChanged(); }
        }

        private ActionsViewModel actionsViewModel;
        public ActionsViewModel ActionsViewModel
        {
            get { return actionsViewModel; }
            set { actionsViewModel = value; NotifyPropertyChanged(); }
        }

        public MainViewModel()
        {
            data = new DataAccess.Data();
            game = new Game(data, new List<string> { "Jessica", "Jack" }, Difficulty.Standard);

            currentPlayer = new ObjectContext<Player>();
            currentPlayer.Context = game.CurrentPlayer;

            actionManager = new ObjectContext<ActionManager>();
            actionManager.Context = game.ActionManager;

            GameStatusViewModel = new GameStatusViewModel(game.OutbreakCounter, game.InfectionRateCounter, 
                game.DiseaseCounters.Single(i => i.Disease.Type == DiseaseType.Yellow), 
                game.DiseaseCounters.Single(i => i.Disease.Type == DiseaseType.Red), 
                game.DiseaseCounters.Single(i => i.Disease.Type == DiseaseType.Blue), 
                game.DiseaseCounters.Single(i => i.Disease.Type == DiseaseType.Black));

            PlayersViewModel = new PlayersViewModel(game.Players, currentPlayer);
            ActionsViewModel = new ActionsViewModel(actionManager);

            GameStatusViewModel.ChangeNotificationRequested += ChangeNotificationRequested;
            PlayersViewModel.ChangeNotificationRequested += ChangeNotificationRequested;
            ActionsViewModel.ChangeNotificationRequested += ChangeNotificationRequested;
        }

        private void ChangeNotificationRequested(object sender, EventArgs e)
        {
            GameStatusViewModel.NotifyChanges();
            PlayersViewModel.NotifyChanges();
            ActionsViewModel.NotifyChanges();
        }
    }
}
