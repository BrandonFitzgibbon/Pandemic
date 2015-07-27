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
    public class DeckViewModel : ViewModelBase, IDeckViewModel
    {
        private IContext<IDrawCounter> drawCounterContext;
        private IContext<IInfectionCard> infectionCardContext;

        private IInfectionCardViewModel infectionCardViewModel;
        public IInfectionCardViewModel InfectionCardViewModel
        {
            get { return infectionCardViewModel; }
        }

        public DeckViewModel(IContext<IDrawCounter> drawCounterContext, IContext<IInfectionCard> infectionCardContext, InfectionCardViewModel infectionCardViewModel)
        {
            this.drawCounterContext = drawCounterContext;
            this.infectionCardViewModel = infectionCardViewModel;
            this.infectionCardContext = infectionCardContext;
        }

        private RelayCommand drawPlayerCardCommand;
        public ICommand DrawPlayerCardCommand
        {
            get 
            {
                if (drawPlayerCardCommand == null)
                    drawPlayerCardCommand = new RelayCommand(a => DrawPlayerCard(), a => CanDrawPlayerCard());
                return drawPlayerCardCommand;
            }
        }

        public void DrawPlayerCard()
        {
            drawCounterContext.Context.DrawPlayerCard();
            RaiseChangeNotificationRequested();
        }

        public bool CanDrawPlayerCard()
        {
            return drawCounterContext != null && drawCounterContext.Context != null ? drawCounterContext.Context.DrawCount > 0 : false;
        }

        private RelayCommand drawInfectionCardCommand;
        public ICommand DrawInfectionCardCommand
        {
            get
            {
                if (drawInfectionCardCommand == null)
                    drawInfectionCardCommand = new RelayCommand(a => DrawInfectionCard(), a => CanDrawInfectionCard());
                return drawInfectionCardCommand;
            }
        }

        public void DrawInfectionCard()
        {
            infectionCardContext.Context = drawCounterContext.Context.DrawInfectionCard();
            RaiseChangeNotificationRequested();
        }

        public bool CanDrawInfectionCard()
        {
            return drawCounterContext != null && drawCounterContext.Context != null ? drawCounterContext.Context.DrawCount == 0 && drawCounterContext.Context.InfectionCount > 0 : false;
        }
    }
}
