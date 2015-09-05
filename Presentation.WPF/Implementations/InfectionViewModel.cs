using Engine.Implementations;
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
    public class InfectionViewModel : ViewModelBase, IInfectionViewModel
    {
        private InfectionManager infectionManager;
        public InfectionManager InfectionManager
        {
            get { return infectionManager; }
        }

        public InfectionViewModel(InfectionManager InfectionManager)
        {
            this.infectionManager = InfectionManager;
        }

        private RelayCommand infectionCommand;
        public ICommand InfectionCommand
        {
            get
            {
                if (infectionCommand == null)
                    infectionCommand = new RelayCommand(m => Infection(), p => CanInfection());
                return infectionCommand;
            }
        }

        private bool CanInfection()
        {
            return InfectionManager.CanInfect;
        }

        public void Infection()
        {
            InfectionManager.Infect();
            RaiseChangeNotificationRequested();
        }
    }
}
