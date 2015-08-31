using Engine.Implementations;
using Presentation.WPF.Context;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using System.Windows.Threading;

namespace Presentation.WPF.Implementations
{
    public class EpidemicViewModel : ViewModelBase, IEpidemicViewModel
    {
        private IContext<StringBuilder> messageContext;

        private EpidemicManager manager;
        public EpidemicManager Manager
        {
            get { return manager; }
        }

        private bool isOpen = true;
        public bool IsOpen
        {
            get { return isOpen; }
            set { isOpen = value; NotifyPropertyChanged(); }
        }

        private bool isLoaded;
        public bool IsLoaded
        {
            get { return isLoaded; }
            set { isLoaded = value;  NotifyPropertyChanged(); RaiseChangeNotificationRequested(); }
        }

        public EpidemicViewModel(EpidemicManager manager, IContext<StringBuilder> messageContext)
        {
            this.manager = manager;
            this.messageContext = messageContext;
            messageContext.Context.AppendLine("Epidemic!");
        }

        private RelayCommand increaseCommand;
        public ICommand IncreaseCommand
        {
            get
            {
                if (increaseCommand == null)
                    increaseCommand = new RelayCommand((a) => Increase(), (p) => CanIncrease());
                return increaseCommand;
            }
        }

        private bool CanIncrease()
        {
            return Manager.CanIncrease;
        }

        private void Increase()
        {
            Manager.Increase();
            messageContext.Context.AppendLine("The infection counter has increased.");
            RaiseChangeNotificationRequested();
        }

        private RelayCommand infectCommand;
        public ICommand InfectCommand
        {
            get
            {
                if (infectCommand == null)
                    infectCommand = new RelayCommand((a) => Infect(), (p) => CanInfect());
                return infectCommand;
            }
        }

        private bool CanInfect()
        {
            return Manager.CanInfect;
        }

        private void Infect()
        {
            Manager.Infect();
            RaiseChangeNotificationRequested();
        }

        private RelayCommand intensifyCommand;
        public ICommand IntensifyCommand
        {
            get
            {
                if (intensifyCommand == null)
                    intensifyCommand = new RelayCommand((a) => Intensify(), (p) => CanIntensify());
                return intensifyCommand;
            }
        }

        private bool CanIntensify()
        {
            return Manager.CanIntensify;
        }

        private void Intensify()
        {
            Manager.Intensify();
            messageContext.Context.AppendLine("The infection deck has intensified");
            RaiseChangeNotificationRequested();
            IsOpen = false;
        }
    }
}
