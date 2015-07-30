using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Implementations
{
    public class GameStatusViewModel : ViewModelBase
    {
        public ICount OutbreakCounter { get; private set; }
        public IInfectionRateCounter InfectionRateCounter { get; private set; }
        public IDiseaseCounter YellowCounter { get; private set; }
        public IDiseaseCounter RedCounter { get; private set; }
        public IDiseaseCounter BlueCounter { get; private set; }
        public IDiseaseCounter BlackCounter { get; private set; }

        public GameStatusViewModel(ICount outbreakCounter, IInfectionRateCounter infectionRateCounter, IDiseaseCounter yellowCounter, IDiseaseCounter redCounter, IDiseaseCounter blueCounter, IDiseaseCounter blackCounter)
        {
            OutbreakCounter = outbreakCounter;
            InfectionRateCounter = infectionRateCounter;
            YellowCounter = yellowCounter;
            RedCounter = redCounter;
            BlueCounter = blueCounter;
            BlackCounter = blackCounter;
        }
    }
}
