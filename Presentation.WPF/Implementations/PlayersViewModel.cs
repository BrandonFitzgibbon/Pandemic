using Engine.Contracts;
using Engine.Implementations;
using Presentation.WPF.Context;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Validation;

namespace Presentation.WPF.Implementations
{
    public class PlayersViewModel : ViewModelBase, IPlayersViewModel
    {
        private IContext<IPlayer> currentPlayerContext;
        private IContext<IPlayer> selectedPlayerContext;
        private IContext<IActions> actionsContext;

        private IEnumerable<IPlayerViewModel> players;
        public IEnumerable<IPlayerViewModel> Players
        {
            get { return players; }
        }

        private IEnumerable<IDiseaseCounterViewModel> diseaseCounters;
        public IEnumerable<IDiseaseCounterViewModel> DiseaseCounters
        {
            get { return diseaseCounters; }
        }

        private IPlayerViewModel selectedPlayer;
        public IPlayerViewModel SelectedPlayer
        {
            get { return selectedPlayer; }
            set { selectedPlayer = value; selectedPlayerContext.Context = value.Player; NotifyPropertyChanged(); }
        }

        public IPlayer CurrentPlayer
        {
            get { return currentPlayerContext.Context; }
        }

        public IList<DriveDestination> DriveDestinations
        {
            get { return currentPlayerContext.Context != null ? GetDriveDestinations(currentPlayerContext.Context, actionsLeft).OrderBy(i => i.ActionsRequired).ThenBy(j => j.Destination.Name).ToList() : null; }
        }

        public IEnumerable<IDiseaseCounterViewModel> LocationDiseaseCounters
        {
            get { return CurrentPlayer != null && DiseaseCounters != null ? DiseaseCounters.Where(i => i.City == CurrentPlayer.Location) : null; }
        }

        private int actionsLeft;
        public int ActionsLeft
        {
            get { return actionsLeft; }
            set 
            {
                if (value > 0)
                {
                    actionsLeft = value;
                    NotifyPropertyChanged();
                }
                else
                {
                    if (RequestPlayerChange != null) RequestPlayerChange(this, EventArgs.Empty);
                }
                    
            }
        }
           
        public PlayersViewModel(IContext<IPlayer> currentPlayerContext, IContext<IPlayer> selectedPlayerContext, IContext<IActions> actionsContext, ICollection<IPlayerViewModel> playerViewModels, ICollection<IDiseaseCounterViewModel> diseaseCounterViewModels)
        {
            this.actionsContext = actionsContext;
            this.selectedPlayerContext = selectedPlayerContext;
            this.currentPlayerContext = currentPlayerContext;
            this.players = playerViewModels;
            this.diseaseCounters = diseaseCounterViewModels;

            currentPlayerContext.ContextChanged += currentPlayerContextChanged;
            actionsContext.ContextChanged += actionsContextChanged;
        }

        private void actionsContextChanged(object sender, ContextChangedEventArgs<IActions> e)
        {
            ActionsLeft = 4;
            NotifyChanges();
        }

        private void currentPlayerContextChanged(object sender, ContextChangedEventArgs<IPlayer> e)
        {
            NotifyChanges();
        }

        private RelayCommand driveCommand;
        public ICommand DriveCommand
        {
            get 
            {
                if (driveCommand == null)
                    driveCommand = new RelayCommand(driveDestination => Drive((DriveDestination)driveDestination), driveDestination => CanDrive((DriveDestination)driveDestination));
                return driveCommand;
            }
        }

        private void Drive(DriveDestination destination)
        {
            actionsContext.Context.Drive(destination.Destination);
            ActionsLeft = ActionsLeft - destination.ActionsRequired;
            this.NotifyChanges();
            foreach (IPlayerViewModel pvm in players)
            {
                pvm.NotifyChanges();
            }
        }

        private bool CanDrive(DriveDestination driveDestination)
        {
            return driveDestination != null;
        }

        private RelayCommand treatDiseaseCommand;
        public ICommand TreatDiseaseCommand
        {
            get
            {
                if (treatDiseaseCommand == null)
                    treatDiseaseCommand = new RelayCommand(disease => TreatDisease((IDiseaseCounterViewModel)disease), disease => CanTreatDisease((IDiseaseCounterViewModel)disease));
                return treatDiseaseCommand;
            }
        }

        private void TreatDisease(IDiseaseCounterViewModel disease)
        {
            actionsContext.Context.TreatDisease(disease.Disease);
            ActionsLeft = ActionsLeft - 1;
            this.NotifyChanges();
            foreach (IDiseaseCounterViewModel dcvm in LocationDiseaseCounters)
            {
                dcvm.NotifyChanges();
            }
            if (RequestStateUpdate != null) RequestStateUpdate(this, EventArgs.Empty);          
        }


        private bool CanTreatDisease(IDiseaseCounterViewModel disease)
        {
            return disease != null && disease.Disease != null && disease.Count != 0;
        }

        public event EventHandler RequestPlayerChange;
        public event EventHandler RequestStateUpdate;

        public static List<DriveDestination> GetDriveDestinations(IPlayer player, int actionsLeft)
        {
            List<DriveDestination> driveDests = new List<DriveDestination>();
            Dictionary<ICity, int> dests = DriveValidation.DriveDestinations(player, actionsLeft);
            foreach (ICity city in dests.Keys)
            {
                driveDests.Add(new DriveDestination(city, dests[city]));
            }
            return driveDests;
        }

        public class DriveDestination
        {
            private ICity destination;
            public ICity Destination
            {
                get { return destination; }
            }

            private int actionsRequired;
            public int ActionsRequired
            {
                get { return actionsRequired; }
            }

            public DriveDestination(ICity destination, int actionsRequired)
            {
                this.destination = destination;
                this.actionsRequired = actionsRequired;
            }
        }
    }
}
