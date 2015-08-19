using Engine.Implementations;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Implementations
{
    public class BoardViewModel : ViewModelBase, IBoardViewModel
    {
        private IEnumerable<INodeDiseaseCounterViewModel> nodeCounters;

        public IEnumerable<INodeDiseaseCounterViewModel> NodeCounters
        {
            get { return nodeCounters; }
        }

        public IEnumerable<INodeDiseaseCounterViewModel> YellowCounters
        {
            get { return nodeCounters != null ? nodeCounters.Where(i => i.Disease.Type == DiseaseType.Yellow).OrderByDescending(i => i.Count) : null; }
        }

        public IEnumerable<INodeDiseaseCounterViewModel> RedCounters
        {
            get { return nodeCounters != null ? nodeCounters.Where(i => i.Disease.Type == DiseaseType.Red).OrderByDescending(i => i.Count) : null; }
        }

        public IEnumerable<INodeDiseaseCounterViewModel> BlueCounters
        {
            get { return nodeCounters != null ? nodeCounters.Where(i => i.Disease.Type == DiseaseType.Blue).OrderByDescending(i => i.Count) : null; }
        }

        public IEnumerable<INodeDiseaseCounterViewModel> BlackCounters
        {
            get { return nodeCounters != null ? nodeCounters.Where(i => i.Disease.Type == DiseaseType.Black).OrderByDescending(i => i.Count) : null; }
        }

        public BoardViewModel(IEnumerable<INodeDiseaseCounterViewModel> nodeCounters)
        {
            this.nodeCounters = nodeCounters;
        }
    }
}
