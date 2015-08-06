using Engine.Contracts;
using Engine.Implementations.ActionItems;
using Engine.Implementations.ActionManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class ActionManager 
    {
        private DriveManager driveManager;
        private DirectFlightManager directFlightManager;
        private CharterFlightManager charterFlightManager;
        private ShuttleFlightManager shuttleFlightManager;
        private TreatDiseaseManager treatDiseaseManager;

        public Func<DriveDestinationItem, bool> CanDrive { get; private set; }
        public Action<DriveDestinationItem> Drive { get; private set; }
        public IEnumerable<DriveDestinationItem> DriveDestinations 
        {
            get { return driveManager != null ? driveManager.Destinations.OrderBy(i => i.Cost).ThenBy(i => i.Node.City.Name) : null; }
        }

        public Func<Node, bool> CanDirectFlight { get; private set; }
        public Action<CityCard> DirectFlight { get; private set; }
        public Dictionary<Node, CityCard> DirectFlightDestinations
        {
            get { return directFlightManager != null ? directFlightManager.Destinations : null; }
        }

        public Func<Node, CityCard, bool> CanCharterFlight { get; private set; }
        public Action<Node, CityCard> CharterFlight { get; private set; }
        public Dictionary<Node, CityCard> CharterFlightDestinations
        {
            get { return charterFlightManager != null ? charterFlightManager.Destinations : null; }
        }

        public Func<Node, bool> CanShuttleFlight { get; private set; }
        public Action<Node> ShuttleFlight { get; private set; }
        public Dictionary<Node, int> ShuttleDestinations
        {
            get { return shuttleFlightManager != null ? shuttleFlightManager.Destinations : null; }
        }

        public Func<NodeDiseaseCounter, bool> CanTreatDisease { get; private set; }
        public Action<TreatDiseaseItem> TreatDisease { get; private set; }
        public IEnumerable<TreatDiseaseItem> TreatmentTargets
        {
            get { return treatDiseaseManager != null ? treatDiseaseManager.Targets : null; }
        }

        public void SetPlayer(Player player, IEnumerable<Node> nodes, IEnumerable<NodeDiseaseCounter> nodeDiseaseCounters)
        {
            driveManager = new DriveManager(player);
            CanDrive = driveManager.CanDrive;
            Drive = driveManager.Drive;

            directFlightManager = new DirectFlightManager(player);
            CanDirectFlight = directFlightManager.CanDirectFlight;
            DirectFlight = directFlightManager.DirectFlight;

            charterFlightManager = new CharterFlightManager(player, nodes);
            CanCharterFlight = charterFlightManager.CanCharterFlight;
            CharterFlight = charterFlightManager.CharterFlight;

            shuttleFlightManager = new ShuttleFlightManager(player, nodes);
            CanShuttleFlight = shuttleFlightManager.CanShuttleFlight;
            ShuttleFlight = shuttleFlightManager.ShuttleFlight;

            treatDiseaseManager = new TreatDiseaseManager(player, nodeDiseaseCounters);
            TreatDisease = treatDiseaseManager.Treat;
        }
    }
}
