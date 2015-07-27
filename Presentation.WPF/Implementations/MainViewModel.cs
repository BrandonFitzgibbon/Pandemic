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
        private IGame game;
        private IDataAccess data;

        private IContext<IPlayer> selectedPlayerContext;
        private IContext<IPlayer> currentPlayerContext;
        private IContext<IActions> actionContext;
        private IContext<IDrawCounter> drawCounterContext;
        private IContext<IInfectionCard> infectionCardContext;

        private ICollection<IPlayerViewModel> playerViewModels;

        private IViewModelBase boardViewModel;
        public IViewModelBase BoardViewModel
        {
            get { return boardViewModel; }
            set { boardViewModel = value; NotifyPropertyChanged(); }
        }

        private IGameStatusViewModel gameStatusViewModel;
        public IGameStatusViewModel GameStatusViewModel
        {
            get { return gameStatusViewModel; }
            set { gameStatusViewModel = value; NotifyPropertyChanged(); }
        }

        private IViewModelBase handViewModel;
        public IViewModelBase HandViewModel
        {
            get { return handViewModel; }
            set { handViewModel = value; NotifyPropertyChanged(); }
        }

        private IPlayersViewModel playersViewModel;
        public IPlayersViewModel PlayersViewModel
        {
            get { return playersViewModel; }
            set { playersViewModel = value; NotifyPropertyChanged(); }
        }

        private InfectionCardViewModel infectionCardViewModel;
        public InfectionCardViewModel InfectionCardViewModel
        {
            get { return infectionCardViewModel; }
            set { infectionCardViewModel = value; NotifyPropertyChanged(); }
        }

        private IDeckViewModel deckViewModel;
        public IDeckViewModel DeckViewModel
        {
            get { return deckViewModel; }
            set { deckViewModel = value; NotifyPropertyChanged(); }
        }

        private IActionsViewModel actionsViewModel;
        public IActionsViewModel ActionsViewModel
        {
            get { return actionsViewModel; }
            set { actionsViewModel = value; NotifyPropertyChanged(); }
        }

        public MainViewModel()
        {
            data = new DataAccess.Data();
            game = new Game(data.GetDiseases(), data.GetCities(), new PlayerFactory(), new List<string>() { "Jon", "Jessica", "Jack"}, new DeckFactory(data.GetCities()), new OutbreakCounter(data.GetCities()), new InfectionRateCounter(), Difficulty.Standard);

            BoardViewModel = new BoardViewModel(game.Cities.ToList());

            selectedPlayerContext = new ObjectContext<IPlayer>();
            currentPlayerContext = new ObjectContext<IPlayer>();
            actionContext = new ObjectContext<IActions>();
            drawCounterContext = new ObjectContext<IDrawCounter>();
            infectionCardContext = new ObjectContext<IInfectionCard>();

            playerViewModels = new Collection<IPlayerViewModel>();
            foreach (IPlayer player in game.Players)
            {
                playerViewModels.Add(new PlayerViewModel(player));
            }

            GameStatusViewModel = new GameStatusViewModel(game);
            PlayersViewModel = new PlayersViewModel(currentPlayerContext, selectedPlayerContext, actionContext, playerViewModels);
            HandViewModel = new HandViewModel(selectedPlayerContext);
            InfectionCardViewModel = new InfectionCardViewModel(infectionCardContext);
            DeckViewModel = new DeckViewModel(drawCounterContext, infectionCardContext, InfectionCardViewModel);
            ActionsViewModel = new ActionsViewModel(actionContext, currentPlayerContext);

            GameStatusViewModel.ChangeNotificationRequested += ViewModelChangeNotificationRequested;
            PlayersViewModel.ChangeNotificationRequested += ViewModelChangeNotificationRequested;
            HandViewModel.ChangeNotificationRequested += ViewModelChangeNotificationRequested;
            DeckViewModel.ChangeNotificationRequested += ViewModelChangeNotificationRequested;
            ActionsViewModel.ChangeNotificationRequested += ViewModelChangeNotificationRequested;
            InfectionCardViewModel.ChangeNotificationRequested += ViewModelChangeNotificationRequested;

            RequestNextPlayer();
        }

        private void ViewModelChangeNotificationRequested(object sender, EventArgs e)
        {
            NotifyViewModels();
        }

        private void NotifyViewModels()
        {
            GameStatusViewModel.NotifyChanges();
            PlayersViewModel.NotifyChanges();
            ActionsViewModel.NotifyChanges();
            HandViewModel.NotifyChanges();
            InfectionCardViewModel.NotifyChanges();

            foreach (IPlayerViewModel pvm in playerViewModels)
            {
                pvm.NotifyChanges();
            }
        }

        public void RequestNextPlayer()
        {
            game.NextPlayer();
            currentPlayerContext.Context = game.CurrentPlayer;
            actionContext.Context = game.CurrentActions;
            actionContext.Context.ActionsDepleted += ActionsDepleted;
        }

        private void ActionsDepleted(object sender, EventArgs e)
        {
            drawCounterContext.Context = game.CurrentDrawCounter;
            drawCounterContext.Context.InfectionsDepleted += InfectionsDepleted;
        }

        private void InfectionsDepleted(object sender, EventArgs e)
        {
            RequestNextPlayer();
        }
    }
}
