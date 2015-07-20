using Engine.Contracts;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Implementations
{
    public class GameStatusViewModel : ViewModelBase, IGameStatusViewModel
    {
        private IGame game;

        public int Outbreaks
        {
            get { return game.OutbreakCounter.Count; }
        }

        public int InfectionRate
        {
            get { return game.InfectionRateCounter.InfectionRate; }
        }

        public int YellowCounters
        {
            get { return game.Diseases.Single(i => i.Type == DiseaseType.Yellow).Count; }
        }

        public int RedCounters
        {
            get { return game.Diseases.Single(i => i.Type == DiseaseType.Red).Count; }
        }

        public int BlueCounters
        {
            get { return game.Diseases.Single(i => i.Type == DiseaseType.Blue).Count; }
        }

        public int BlackCounters
        {
            get { return game.Diseases.Single(i => i.Type == DiseaseType.Black).Count; }
        }

        public GameStatusViewModel(IGame game)
        {
            this.game = game;        
        }
    }
}
