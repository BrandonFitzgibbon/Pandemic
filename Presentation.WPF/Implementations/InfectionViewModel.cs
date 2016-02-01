using Engine.Implementations;
using Presentation.WPF.Context;
using Presentation.WPF.Contracts;
using Presentation.WPF.Controls;
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
    public class InfectionViewModel : ViewModelBase, IInfectionViewModel
    {
        private IBoardViewModel boardViewModel;

        private InfectionManager infectionManager;
        public InfectionManager InfectionManager
        {
            get { return infectionManager; }
        }

        public InfectionViewModel(InfectionManager infectionManager, IEnumerable<NodeDiseaseCounter> nodeCounters, IBoardViewModel boardViewModel, Notifier notifier)
        {
            this.boardViewModel = boardViewModel;
            this.infectionManager = infectionManager;
            notifier.SubscribeToViewModel(this);

            foreach (NodeDiseaseCounter ndc in nodeCounters)
            {
                ndc.Infected += Ndc_Infected;
            }

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += Timer_Tick;

            animationTimer = new DispatcherTimer();
            animationTimer.Interval = TimeSpan.FromMilliseconds(1);
            animationTimer.Tick += TranslateTimer_Tick;
        }

        private void TranslateTimer_Tick(object sender, EventArgs e)
        {
            Translate = Translate - 0.75;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            BannerVisibility = System.Windows.Visibility.Hidden;
            InfectedCity = null;
            timer.Stop();
            animationTimer.Stop();
        }

        private void Ndc_Infected(object sender, Engine.CustomEventArgs.InfectionEventArgs e)
        {
            IAnchorViewModel avm = boardViewModel.AnchorViewModels.Single(i => i.Node == e.NodeDiseaseCounter.Node);
            Left = avm.Left;
            Top = avm.Top;
            ScaleX = 0;
            ScaleY = 0;
            Translate = 0;
            timer.Stop();
            animationTimer.Stop();
            InfectedCity = e.NodeDiseaseCounter.Node.City;
            InfectionCount = e.NodeDiseaseCounter.Count;

            Icons icons = new Icons();
            switch (e.NodeDiseaseCounter.Disease.Type)
            {
                case DiseaseType.Yellow:
                    InfectionIcon = icons["compoundYellow"];
                    break;
                case DiseaseType.Red:
                    InfectionIcon = icons["compoundRed"];
                    break;
                case DiseaseType.Blue:
                    InfectionIcon = icons["compoundBlue"];
                    break;
                case DiseaseType.Black:
                    InfectionIcon = icons["compoundBlack"];
                    break;
            }

            BannerVisibility = System.Windows.Visibility.Visible;
            timer.Start();
            animationTimer.Start();
        }

        private DispatcherTimer timer;
        private DispatcherTimer animationTimer;

        private double scaleX = 0;
        public double ScaleX
        {
            get { return scaleX; }
            set { scaleX = value;  NotifyPropertyChanged(); }
        }

        private double scaleY = 0;
        public double ScaleY
        {
            get { return scaleY; }
            set { scaleY = value; NotifyPropertyChanged(); }
        }

        private double translate = 0;
        public double Translate
        {
            get { return translate; }
            set { translate = value;  NotifyPropertyChanged(); }
        }

        private double left;
        public double Left
        {
            get { return left; }
            set { left = value; NotifyPropertyChanged(); }
        }

        private double top;
        public double Top
        {
            get { return top; }
            set { top = value; NotifyPropertyChanged(); }
        }

        private City infectedCity;
        public City InfectedCity
        {
            get { return infectedCity; }
            set { infectedCity = value;  NotifyPropertyChanged(); }
        }

        private int infectionCount;
        public int InfectionCount
        {
            get { return infectionCount; }
            set { infectionCount = value;  NotifyPropertyChanged(); }
        }

        private object infectionIcon;
        public object InfectionIcon
        {
            get { return infectionIcon; }
            set { infectionIcon = value;  NotifyPropertyChanged(); }
        }

        private System.Windows.Visibility bannerVisibility = System.Windows.Visibility.Hidden;
        public System.Windows.Visibility BannerVisibility
        {
            get { return bannerVisibility; }
            set { bannerVisibility = value;  NotifyPropertyChanged(); }
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
        }


    }
}
