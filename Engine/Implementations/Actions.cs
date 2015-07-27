using Engine.Contracts;
using Engine.CustomEventArgs;
using Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class Actions : IActions
    {
        private ICity location;

        private int actionCount;
        public int ActionCount
        {
            get { return actionCount; }
            set
            {
                if (value > 0)
                {
                    actionCount = value;
                    driveDestinations = ListExtensions.GetDriveDestinations(location, ActionCount);
                    diseaseTreatmentOptions = ListExtensions.GetDiseaseTreatmentOptions(location, ActionCount);
                }
                else if (value == 0)
                {
                    actionCount = value;
                    driveDestinations = ListExtensions.GetDriveDestinations(location, ActionCount);
                    diseaseTreatmentOptions = ListExtensions.GetDiseaseTreatmentOptions(location, ActionCount);
                    if (ActionsDepleted != null) ActionsDepleted(this, EventArgs.Empty);
                }
            }
        }

        private IDictionary<ICity, int> driveDestinations;
        public IDictionary<ICity, int> DriveDestinations
        {
            get { return driveDestinations.OrderBy(i => i.Value).ThenBy(i => i.Key.Name).ToDictionary(pair => pair.Key, pair => pair.Value); }
        }

        private IDictionary<IDisease, int> diseaseTreatmentOptions;
        public IDictionary<IDisease, int> DiseaseTreatmentOptions
        {
            get { return diseaseTreatmentOptions; }
        }

        private Action<ICity> drive;
        public Action<ICity> Drive
        {
            get { return DriveAction; }
        }

        private Func<ICity, bool> canDrive;
        public Func<ICity, bool> CanDrive
        {
            get { return canDrive; }
        }

        private Action<IDisease> treatDisease;
        public Action<IDisease, ICity> TreatDisease
        {
            get { return TreatDiseaseAction; }
        }

        public Actions(Player player)
        {
            location = player.Location;
            player.Moved += PlayerMoved;

            //Hook drive
            drive = player.Drive;
            canDrive = CanDriveFunction;

            //Hook treatDisease
            treatDisease = player.TreatDisease;     
     
            //set actions
            ActionCount = 4;
            driveDestinations = ListExtensions.GetDriveDestinations(location, ActionCount);
            diseaseTreatmentOptions = ListExtensions.GetDiseaseTreatmentOptions(location, ActionCount);
        }

        public void PlayerMoved(object sender, PlayerMovedEventArgs e)
        {
            driveDestinations = ListExtensions.GetDriveDestinations(e.ArrivedCity, ActionCount);
            diseaseTreatmentOptions = ListExtensions.GetDiseaseTreatmentOptions(e.ArrivedCity, ActionCount);
            location = e.ArrivedCity;
        }

        private bool CanDriveFunction(ICity city)
        {
            return city != null ? DriveDestinations.ContainsKey(city) && DriveDestinations[city] <= ActionCount : false;          
        }

        private void DriveAction(ICity city)
        {
            if (canDrive(city))
            {
                int actionsRequired = DriveDestinations[city];
                drive.Invoke(city);
                ActionCount = ActionCount - actionsRequired;
            }
        }

        private void TreatDiseaseAction(IDisease disease, ICity city)
        {
            treatDisease.Invoke(disease);
            ActionCount = ActionCount - 1;
        }

        public event EventHandler ActionsDepleted;
    }
}
