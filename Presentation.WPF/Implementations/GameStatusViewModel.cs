using Engine.Contracts;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public bool InfectionCounterOne
        {
            get { return game.InfectionRateCounter.Count >= 0; }
        }

        public bool InfectionCounterTwo
        {
            get { return game.InfectionRateCounter.Count >= 1; }
        }

        public bool InfectionCounterThree
        {
            get { return game.InfectionRateCounter.Count >= 2; }
        }

        public bool InfectionCounterFour
        {
            get { return game.InfectionRateCounter.Count >= 3; }
        }

        public bool InfectionCounterFive
        {
            get { return game.InfectionRateCounter.Count >= 4; }
        }

        public bool InfectionCounterSix
        {
            get { return game.InfectionRateCounter.Count >= 5; }
        }

        public bool InfectionCounterSeven
        {
            get { return game.InfectionRateCounter.Count >= 6; }
        }

        public int YellowCounters
        {
            get { return game.Diseases.Single(i => i.Type == DiseaseType.Yellow).Count; }
        }

        public bool YellowCured
        {
            get { return game.Diseases.Single(i => i.Type == DiseaseType.Yellow).IsCured; }
        }

        public bool YellowEradicated
        {
            get { return game.Diseases.Single(i => i.Type == DiseaseType.Yellow).IsEradicated; }
        }

        public int RedCounters
        {
            get { return game.Diseases.Single(i => i.Type == DiseaseType.Red).Count; }
        }

        public bool RedCured
        {
            get { return game.Diseases.Single(i => i.Type == DiseaseType.Red).IsCured; }
        }

        public bool RedEradicated
        {
            get { return game.Diseases.Single(i => i.Type == DiseaseType.Red).IsEradicated; }
        }

        public int BlueCounters
        {
            get { return game.Diseases.Single(i => i.Type == DiseaseType.Blue).Count; }
        }

        public bool BlueCured
        {
            get { return game.Diseases.Single(i => i.Type == DiseaseType.Blue).IsCured; }
        }

        public bool BlueEradicated
        {
            get { return game.Diseases.Single(i => i.Type == DiseaseType.Blue).IsEradicated; }
        }

        public int BlackCounters
        {
            get { return game.Diseases.Single(i => i.Type == DiseaseType.Black).Count; }
        }

        public bool BlackCured
        {
            get { return game.Diseases.Single(i => i.Type == DiseaseType.Black).IsCured; }
        }

        public bool BlackEradicated
        {
            get { return game.Diseases.Single(i => i.Type == DiseaseType.Black).IsEradicated; }
        }

        public GameStatusViewModel(IGame game)
        {
            this.game = game;        
        }

        public void NotifyChanges()
        {
            foreach (PropertyInfo prop in this.GetType().GetProperties())
            {
                NotifyPropertyChanged(prop.Name);
            }
        }
    }
}
