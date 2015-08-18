using Engine.Contracts;
using Engine.Implementations;
using Presentation.WPF.Contracts;
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
        private InfectionRateCounter infectionRateCounter;
        private IDiseaseCounterViewModel yellowCounter;
        private IDiseaseCounterViewModel redCounter;
        private IDiseaseCounterViewModel blueCounter;
        private IDiseaseCounterViewModel blackCounter;
        
        public int OutbreakCount
        {
            get { return outbreakCounter != null ? outbreakCounter.Count : 0; }
        }

        public int InfectionCount
        {
            get { return infectionRateCounter != null ? infectionRateCounter.Count : 0; }
        }

        public IDiseaseCounterViewModel YellowCounter
        {
            get { return yellowCounter; }
        }

        public IDiseaseCounterViewModel RedCounter
        {
            get { return redCounter; }
        }

        public IDiseaseCounterViewModel BlueCounter
        {
            get { return blueCounter; }
        }

        public IDiseaseCounterViewModel BlackCounter
        {
            get { return blackCounter; }
        }

        public GameStatusViewModel(OutbreakCounter outbreakCounter, InfectionRateCounter infectionRateCounter, IDiseaseCounterViewModel yellowCounter, IDiseaseCounterViewModel redCounter, IDiseaseCounterViewModel blueCounter, IDiseaseCounterViewModel blackCounter)
        {
            this.outbreakCounter = outbreakCounter;
            this.infectionRateCounter = infectionRateCounter;
            this.yellowCounter = yellowCounter;
            this.redCounter = redCounter;
            this.blueCounter = blueCounter;
            this.blackCounter = blackCounter;
        }
    }
}
