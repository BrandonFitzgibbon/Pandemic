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
        private IContext<InfectionManager> infectionManager;

        public InfectionManager InfectionManager
        {
            get { return infectionManager != null ? infectionManager.Context : null; }
        }

        public InfectionViewModel(IContext<InfectionManager> InfectionManager)
        {
            this.infectionManager = InfectionManager;
        }

        private void ContextChanged(object sender, ContextChangedEventArgs<InfectionManager> e)
        {
            NotifyPropertyChanged("InfectionManager");
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
