using Engine.Contracts;
using Engine.Factories;
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
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        private IGame game;
        private IDataAccess data;

        private IContext<IPlayer> playerContext;
        private IContext<IActions> actionContext;

        private IViewModelBase boardViewModel;
        public IViewModelBase BoardViewModel
        {
            get { return boardViewModel; }
            set { boardViewModel = value; NotifyPropertyChanged(); }
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

        public MainViewModel()
        {
            data = new DataAccess.Data();
            game = new Game(data.GetDiseases(), data.GetCities(), new PlayerFactory(), new List<string>() { "John", "Jane", "Jack" }, new DeckFactory(data.GetCities()), new OutbreakCounter(data.GetCities()), new InfectionRateCounter(), Difficulty.Standard);

            BoardViewModel = new BoardViewModel(game.Cities.ToList());

            playerContext = new ObjectContext<IPlayer>();
            actionContext = new ObjectContext<IActions>();

            PlayersViewModel = new PlayersViewModel(game, actionContext, playerContext, game.Players.ToList());
            HandViewModel = new HandViewModel(playerContext);

            PlayersViewModel.CurrentPlayer = game.CurrentPlayer;
        }
    }
}
