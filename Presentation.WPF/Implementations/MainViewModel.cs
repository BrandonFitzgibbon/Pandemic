﻿using Engine.Contracts;
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
            GameStatusViewModel = new GameStatusViewModel(game);

            selectedPlayerContext = new ObjectContext<IPlayer>();
            currentPlayerContext = new ObjectContext<IPlayer>();
            actionContext = new ObjectContext<IActions>();

            Collection<IPlayerViewModel> playerViewModels = new Collection<IPlayerViewModel>();
            foreach (IPlayer player in game.Players)
            {
                playerViewModels.Add(new PlayerViewModel(player));
            }

            Collection<IDiseaseCounterViewModel> diseaseViewModels = new Collection<IDiseaseCounterViewModel>();
            foreach (ICity city in game.Cities)
            {
                foreach (IDiseaseCounter counter in city.Counters)
                {
                    diseaseViewModels.Add(new DiseaseCounterViewModel(city, counter.Disease));
                }
            }

            PlayersViewModel = new PlayersViewModel(currentPlayerContext, selectedPlayerContext, actionContext, playerViewModels, diseaseViewModels);
            PlayersViewModel.RequestStateUpdate += PlayersViewModelRequestStateUpdate;

            HandViewModel = new HandViewModel(selectedPlayerContext);
            DeckViewModel = new DeckViewModel(game.PlayerDeck, this);
            ActionsViewModel = new ActionsViewModel(actionContext);

            DeckViewModel.CardDrawn += DeckViewModel_CardDrawn;

            RequestNextPlayer();
        }

        void DeckViewModel_CardDrawn(object sender, EventArgs e)
        {
            RequestCard();
        }

        private void PlayersViewModelRequestStateUpdate(object sender, EventArgs e)
        {
            GameStatusViewModel.NotifyChanges();
        }

        public void PlayersViewModelActionsDepleted(object sender, EventArgs e)
        {
            if (DrawPhase != null) DrawPhase(this, EventArgs.Empty);
        }

        public void RequestCard()
        {
            game.DrawPhase();
            GameStatusViewModel.NotifyChanges();
            HandViewModel.NotifyChanges();
        }

        public void RequestInfection()
        {
            game.InfectionPhase();
            GameStatusViewModel.NotifyChanges();
        }

        public void RequestNextPlayer()
        {
            game.NextPlayer();
            currentPlayerContext.Context = game.CurrentPlayer;
            actionContext.Context = new Actions((Player)currentPlayerContext.Context);
        }

        public event EventHandler DrawPhase;
    }
}
