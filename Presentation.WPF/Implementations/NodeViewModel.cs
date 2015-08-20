using Engine.Implementations;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Implementations
{
    public class NodeViewModel : ViewModelBase, INodeViewModel
    {
        private Node node;

        private IEnumerable<INodeDiseaseCounterViewModel> nodeCounters;
        public IEnumerable<INodeDiseaseCounterViewModel> NodeCounters
        {
            get { return nodeCounters; }
        }

        public string Name
        {
            get { return node.City.Name; }
        }

        public Disease Disease
        {
            get { return node.Disease; }
        }

        public bool ResearchStation
        {
            get { return node.ResearchStation; }
        }

        public INodeDiseaseCounterViewModel YellowCounter
        {
            get { return nodeCounters != null ? nodeCounters.Single(i => i.Disease.Type == DiseaseType.Yellow) : null; }
        }

        public INodeDiseaseCounterViewModel RedCounter
        {
            get { return nodeCounters != null ? nodeCounters.Single(i => i.Disease.Type == DiseaseType.Red) : null; }
        }

        public INodeDiseaseCounterViewModel BlueCounter
        {
            get { return nodeCounters != null ? nodeCounters.Single(i => i.Disease.Type == DiseaseType.Blue) : null; }
        }

        public INodeDiseaseCounterViewModel BlackCounter
        {
            get { return nodeCounters != null ? nodeCounters.Single(i => i.Disease.Type == DiseaseType.Black) : null; }
        }

        public NodeViewModel(Node node, IEnumerable<INodeDiseaseCounterViewModel> nodeCounters)
        {
            this.node = node;
            this.nodeCounters = nodeCounters;
        }
    }
}
