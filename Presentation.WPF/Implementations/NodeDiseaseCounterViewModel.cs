using Engine.Implementations;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Implementations
{
    public class NodeDiseaseCounterViewModel : ViewModelBase, INodeDiseaseCounterViewModel
    {
        private NodeDiseaseCounter nodeDiseaseCounter;

        public Node Node
        {
            get { return nodeDiseaseCounter != null ? nodeDiseaseCounter.Node : null; }
        }

        public Disease Disease
        {
            get { return nodeDiseaseCounter != null ? nodeDiseaseCounter.Disease : null; }
        }

        public int Count
        {
            get { return nodeDiseaseCounter != null ? nodeDiseaseCounter.Count : 0; }
        }

        public NodeDiseaseCounterViewModel(NodeDiseaseCounter nodeDiseaseCounter)
        {
            this.nodeDiseaseCounter = nodeDiseaseCounter;
        }
    }
}
