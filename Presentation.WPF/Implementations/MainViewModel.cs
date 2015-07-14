using Engine.Contracts;
using Engine.Factories;
using Engine.Implementations;
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

        private IViewModelBase boardViewModel;
        public IViewModelBase BoardViewModel
        {
            get { return boardViewModel; }
            set { boardViewModel = value; NotifyPropertyChanged(); }
        }

        public MainViewModel()
        {
            data = new DataAccess.Data();
            game = new Game(data, new PlayerFactory(), new List<string>() { "John", "Jane" }, new OutbreakCounter(), new InfectionRateCounter(), Difficulty.Standard);
            BoardViewModel = new BoardViewModel(game.Cities.ToList());
        }
    }
}
