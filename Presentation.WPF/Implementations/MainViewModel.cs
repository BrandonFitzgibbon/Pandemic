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
using System.Windows.Input;

namespace Presentation.WPF.Implementations
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        private Game game;
        private IDataAccess data;
        private IContext<Player> currentPlayer;
        private IContext<ActionManager> actionManager;
        private IContext<DrawManager> drawManager;
        private IContext<InfectionManager> infectionManager;

        private GameStatusViewModel gameStatusViewModel;
        public GameStatusViewModel GameStatusViewModel
        {
            get { return gameStatusViewModel; }
            set { gameStatusViewModel = value; NotifyPropertyChanged(); }
        }

        private IPlayersViewModel playersViewModel;
        public IPlayersViewModel PlayersViewModel
        {
            get { return playersViewModel; }
            set { playersViewModel = value; NotifyPropertyChanged(); }
        }

        private IActionsViewModel actionsViewModel;
        public IActionsViewModel ActionsViewModel
        {
            get { return actionsViewModel; }
            set { actionsViewModel = value; NotifyPropertyChanged(); }
        }

        private IDrawViewModel drawViewModel;
        public IDrawViewModel DrawViewModel
        {
            get { return drawViewModel; }
            set { drawViewModel = value; NotifyPropertyChanged(); }
        }

        private IInfectionViewModel infectionViewModel;
        public IInfectionViewModel InfectionViewModel
        {
            get { return infectionViewModel; }
            set { infectionViewModel = value; NotifyPropertyChanged(); }
        }

        private IEnumerable<IPlayerViewModel> playerViewModels;
        private IEnumerable<IDiseaseCounterViewModel> diseaseCounterViewModels;

        public MainViewModel()
        {
            data = new DataAccess.Data();
            game = new Game(data, new List<string> { "Jessica", "Jack" }, Difficulty.Standard);

            currentPlayer = new ObjectContext<Player>();
            currentPlayer.Context = game.CurrentPlayer;

            actionManager = new ObjectContext<ActionManager>();
            actionManager.Context = game.ActionManager;

            drawManager = new ObjectContext<DrawManager>();
            drawManager.Context = game.DrawManager;

            infectionManager = new ObjectContext<InfectionManager>();
            infectionManager.Context = game.InfectionManager;

            playerViewModels = CreatePlayerViewModels(game.Players);
            diseaseCounterViewModels = CreateDiseaseCounterViewModels(game.DiseaseCounters);

            GameStatusViewModel = new GameStatusViewModel(game.OutbreakCounter, game.InfectionRateCounter, 
                diseaseCounterViewModels.Single(i => i.Disease.Type == DiseaseType.Yellow), 
                diseaseCounterViewModels.Single(i => i.Disease.Type == DiseaseType.Red), 
                diseaseCounterViewModels.Single(i => i.Disease.Type == DiseaseType.Blue), 
                diseaseCounterViewModels.Single(i => i.Disease.Type == DiseaseType.Black));

            PlayersViewModel = new PlayersViewModel(currentPlayer, playerViewModels);
            ActionsViewModel = new ActionsViewModel(actionManager, currentPlayer);
            DrawViewModel = new DrawViewModel(drawManager);
            InfectionViewModel = new InfectionViewModel(infectionManager);

            GameStatusViewModel.ChangeNotificationRequested += ChangeNotificationRequested;
            PlayersViewModel.ChangeNotificationRequested += ChangeNotificationRequested;
            ActionsViewModel.ChangeNotificationRequested += ChangeNotificationRequested;
            DrawViewModel.ChangeNotificationRequested += ChangeNotificationRequested;
            InfectionViewModel.ChangeNotificationRequested += ChangeNotificationRequested;
        }

        private IEnumerable<IPlayerViewModel> CreatePlayerViewModels(IEnumerable<Player> players)
        {
            List<IPlayerViewModel> playerViewModels = new List<IPlayerViewModel>();
            foreach (Player player in players)
            {
                PlayerViewModel pvm = new PlayerViewModel(player);
                pvm.ChangeNotificationRequested += ChangeNotificationRequested;
                playerViewModels.Add(pvm);
            }
            return playerViewModels;
        }

        private IEnumerable<IDiseaseCounterViewModel> CreateDiseaseCounterViewModels(IEnumerable<DiseaseCounter> diseases)
        {
            List<IDiseaseCounterViewModel> diseaseCounterViewModels = new List<IDiseaseCounterViewModel>();
            foreach (DiseaseCounter disease in diseases)
            {
                DiseaseCounterViewModel dcvm = new DiseaseCounterViewModel(disease);
                dcvm.ChangeNotificationRequested += ChangeNotificationRequested;
                diseaseCounterViewModels.Add(dcvm);
            }
            return diseaseCounterViewModels;
        }

        private new void ChangeNotificationRequested(object sender, EventArgs e)
        {
            GameStatusViewModel.NotifyChanges();
            PlayersViewModel.NotifyChanges();
            ActionsViewModel.NotifyChanges();
            DrawViewModel.NotifyChanges();
            InfectionViewModel.NotifyChanges();

            foreach (IPlayerViewModel playerViewModel in playerViewModels)
            {
                playerViewModel.NotifyChanges();
            }

            foreach (IDiseaseCounterViewModel diseaseCounterViewModel in diseaseCounterViewModels)
            {
                diseaseCounterViewModel.NotifyChanges();
            }

            NotifyChanges();
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
            ChangeNotificationRequested(this, EventArgs.Empty);
        }
    }
}
