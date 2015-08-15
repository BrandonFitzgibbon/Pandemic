using Engine.Contracts;
using Engine.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Implementations
{
    public class GameStatusViewModel : ViewModelBase
    {
        private OutbreakCounter outbreakCounter;
        
        public int OutbreakCount
        {
            get { return outbreakCounter != null ? outbreakCounter.Count : 0; }
        }

        public InfectionRateCounter InfectionRateCounter { get; private set; }
        public DiseaseCounter YellowCounter { get; private set; }
        public DiseaseCounter RedCounter { get; private set; }
        public DiseaseCounter BlueCounter { get; private set; }
        public DiseaseCounter BlackCounter { get; private set; }
        public int InfectionCount { get; private set; }

        public GameStatusViewModel(OutbreakCounter outbreakCounter, InfectionRateCounter infectionRateCounter, DiseaseCounter yellowCounter, DiseaseCounter redCounter, DiseaseCounter blueCounter, DiseaseCounter blackCounter)
        {
            this.outbreakCounter = outbreakCounter;
            InfectionRateCounter = infectionRateCounter;
            YellowCounter = yellowCounter;
            RedCounter = redCounter;
            BlueCounter = blueCounter;
            BlackCounter = blackCounter;
            InfectionCount = infectionRateCounter.Count;
        }
    }
}
