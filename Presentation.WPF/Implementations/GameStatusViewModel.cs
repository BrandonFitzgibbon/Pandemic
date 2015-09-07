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
    public class GameStatusViewModel : ViewModelBase, IGameStatusViewModel
    {
        private OutbreakCounter outbreakCounter;
        private InfectionRateCounter infectionRateCounter;
        private ResearchStationCounter researchStationCounter;
        private IEnumerable<IDiseaseCounterViewModel> diseaseCounterViewModels;
        
        public int OutbreakCount
        {
            get { return outbreakCounter != null ? outbreakCounter.Count : 0; }
        }

        public int InfectionCount
        {
            get { return infectionRateCounter != null ? infectionRateCounter.Count : 0; }
        }

        public int ResearchStationCount
        {
            get { return researchStationCounter != null ? researchStationCounter.Count : 0; }
        }

        public IDiseaseCounterViewModel YellowCounter
        {
            get { return diseaseCounterViewModels.SingleOrDefault(i => i.Disease.Type == DiseaseType.Yellow); }
        }

        public IDiseaseCounterViewModel RedCounter
        {
            get { return diseaseCounterViewModels.SingleOrDefault(i => i.Disease.Type == DiseaseType.Red); }
        }

        public IDiseaseCounterViewModel BlueCounter
        {
            get { return diseaseCounterViewModels.SingleOrDefault(i => i.Disease.Type == DiseaseType.Blue); }
        }

        public IDiseaseCounterViewModel BlackCounter
        {
            get { return diseaseCounterViewModels.SingleOrDefault(i => i.Disease.Type == DiseaseType.Black); }
        }

        public GameStatusViewModel(OutbreakCounter outbreakCounter, InfectionRateCounter infectionRateCounter, ResearchStationCounter researchStationCounter, IEnumerable<IDiseaseCounterViewModel> diseaseCounterViewModels, Notifier notifier)
        {
            if (diseaseCounterViewModels == null)
                throw new ArgumentNullException("DiseaseCounterViewModels");

            this.outbreakCounter = outbreakCounter;
            this.infectionRateCounter = infectionRateCounter;
            this.researchStationCounter = researchStationCounter;
            this.diseaseCounterViewModels = diseaseCounterViewModels;
            notifier.SubscribeToViewModel(this);
        }
    }
}
