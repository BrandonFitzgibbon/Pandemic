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
        private Player player;

        private int actionCount;
        public int ActionCount
        {
            get { return actionCount; }
            set
            {
                if (value > 0)
                    actionCount = value;
                else if (value == 0)
                {
                    actionCount = value;
                    if (ActionsDepleted != null) ActionsDepleted(this, EventArgs.Empty);
                }
            }
        }

        private IDictionary<ICity, int> driveDestinations;
        public IDictionary<ICity, int> DriveDestinations
        {
            get { return driveDestinations; }
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
        public Action<IDisease> TreatDisease
        {
            get { return TreatDiseaseAction; }
        }

        public Actions(Player player)
        {
            this.player = player;

            //Hook drive
            drive = player.Drive;
            canDrive = CanDriveFunction;
            player.Moved += PlayerMoved;

            //Hook treatDisease
            treatDisease = player.TreatDisease;     
     
            //set actions
            ActionCount = 4;
            driveDestinations = ListExtensions.DriveDestinations(player.Location, ActionCount);
        }

        public void PlayerMoved(object sender, PlayerMovedEventArgs e)
        {
            driveDestinations = ListExtensions.DriveDestinations(e.ArrivedCity, ActionCount);
        }

        private bool CanDriveFunction(ICity city)
        {
            return city != null ? DriveDestinations.ContainsKey(city) && DriveDestinations[city] <= ActionCount : false;          
        }

        private void DriveAction(ICity city)
        {
            if (canDrive(city))
            {
                ActionCount = ActionCount - DriveDestinations[city];
                drive.Invoke(city);
            }
        }

        private void TreatDiseaseAction(IDisease disease)
        {
            ActionCount = ActionCount - 1;
            treatDisease.Invoke(disease);
        }

        public event EventHandler ActionsDepleted;
    }
}
