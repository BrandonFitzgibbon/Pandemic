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

        private GameStatusViewModel gameStatusViewModel;
        public GameStatusViewModel GameStatusViewModel
        {
            get { return gameStatusViewModel; }
            set { gameStatusViewModel = value; NotifyPropertyChanged(); }
        }

        public MainViewModel()
        {
            data = new DataAccess.Data();
            game = new Game(data, new List<string> { "Jessica", "Jack" }, Difficulty.Standard);
            
            GameStatusViewModel = new GameStatusViewModel(game.OutbreakCounter, game.InfectionRateCounter, 
                game.DiseaseCounters.Single(i => i.Disease.Type == DiseaseType.Yellow), 
                game.DiseaseCounters.Single(i => i.Disease.Type == DiseaseType.Red), 
                game.DiseaseCounters.Single(i => i.Disease.Type == DiseaseType.Blue), 
                game.DiseaseCounters.Single(i => i.Disease.Type == DiseaseType.Black));
        }
    }
}
