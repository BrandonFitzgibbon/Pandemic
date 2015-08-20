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
        private IEnumerable<INodeViewModel> nodes;
        public IEnumerable<INodeViewModel> Nodes
        {
            get { return nodes;  }
        }

        public BoardViewModel(IEnumerable<INodeViewModel> nodes)
        {
            this.nodes = nodes;
        }
    }
}
