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

        public MainViewModel()
        {
            data = new DataAccess.Data();
            game = new Game(data, new List<string> { "Jessica", "Jack" }, Difficulty.Standard);
        }
    }
}
