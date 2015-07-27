using Engine.Contracts;
using Presentation.WPF.Context;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.WPF.Implementations
{
    public class InfectionCardViewModel : ViewModelBase, IInfectionCardViewModel
    {
        private IContext<IInfectionCard> infectionCardContext;

        public string Name
        {
            get { return infectionCardContext != null && infectionCardContext.Context != null ? infectionCardContext.Context.Name : null; }
        }

        public IDisease Disease
        {
            get { return infectionCardContext != null && infectionCardContext.Context != null ? infectionCardContext.Context.Disease : null;}
        }

        public InfectionCardViewModel(IContext<IInfectionCard> infectionCardContext)
        {
            this.infectionCardContext = infectionCardContext;
        }
    }
}
