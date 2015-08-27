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

        public EpidemicViewModel(EpidemicManager manager)
        {
            this.manager = manager;
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
            RaiseChangeNotificationRequested();
            IsOpen = false;
        }
    }
}
