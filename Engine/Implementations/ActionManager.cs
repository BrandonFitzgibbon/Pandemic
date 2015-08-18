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
        private ResearchStationConstructionManager researchStationConstructionManager;
        private TreatDiseaseManager treatDiseaseManager;
        private ShareKnowledgeManager shareKnowledgeManager;
        private DiscoverCureManager discoverCureManager;

        public Func<DriveDestinationItem, bool> CanDrive { get; private set; }
        public Action<DriveDestinationItem> Drive { get; private set; }
        public IEnumerable<DriveDestinationItem> DriveDestinations 
        {
            get { return driveManager != null ? driveManager.Destinations.OrderBy(i => i.Cost).ThenBy(i => i.Node.City.Name) : null; }
        }

        public Func<DirectFlightItem, bool> CanDirectFlight { get; private set; }
        public Action<DirectFlightItem> DirectFlight { get; private set; }
        public IEnumerable<DirectFlightItem> DirectFlightDestinations
        {
            get { return directFlightManager != null ? directFlightManager.Destinations.OrderBy(i => i.CityCard.Node.City.Name) : null; }
        }

        public Func<CharterFlightItem, bool> CanCharterFlight { get; private set; }
        public Action<CharterFlightItem> CharterFlight { get; private set; }
        public IEnumerable<CharterFlightItem> CharterFlightDestinations
        {
            get { return charterFlightManager != null ? charterFlightManager.Destinations.OrderBy(i => i.Node.City.Name) : null; }
        }

        public Func<ShuttleFlightItem, bool> CanShuttleFlight { get; private set; }
        public Action<ShuttleFlightItem> ShuttleFlight { get; private set; }
        public IEnumerable<ShuttleFlightItem> ShuttleFlightDestinations
        {
            get { return shuttleFlightManager != null ? shuttleFlightManager.Destinations : null; }
        }

        public Func<ResearchStationConstructionItem, bool> CanBuildResearchStation { get; private set; }
        public Action<ResearchStationConstructionItem> BuildResearchStation { get; private set; }
        public IEnumerable<ResearchStationConstructionItem> ResearchStationTargets
        {
            get { return researchStationConstructionManager != null ? researchStationConstructionManager.Targets : null; }
        }

        public Func<TreatDiseaseItem, bool> CanTreatDisease { get; private set; }
        public Action<TreatDiseaseItem> TreatDisease { get; private set; }
        public IEnumerable<TreatDiseaseItem> TreatmentTargets
        {
            get { return treatDiseaseManager != null ? treatDiseaseManager.Targets : null; }
        }

        public Func<ShareKnowledgeItem, bool> CanShareKnowledge { get; private set; }
        public Action<ShareKnowledgeItem> ShareKnowledge { get; private set; }
        public IEnumerable<ShareKnowledgeItem> ShareTargets
        {
            get { return shareKnowledgeManager != null ? shareKnowledgeManager.Targets : null; }
        }

        public Func<DiscoverCureItem, bool> CanDiscoverCure { get; private set; }
        public Action<DiscoverCureItem> DiscoverCure { get; private set; }
        public IEnumerable<DiscoverCureItem> DiscoverTargets
        {
            get { return discoverCureManager != null ? discoverCureManager.Targets : null; }
        }

        public void SetPlayer(Player player, IEnumerable<Player> players, IEnumerable<Node> nodes, IEnumerable<NodeDiseaseCounter> nodeDiseaseCounters, ResearchStationCounter researchStationCounter, IEnumerable<Disease> diseases)
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

            researchStationConstructionManager = new ResearchStationConstructionManager(player, researchStationCounter, nodes);
            CanBuildResearchStation = researchStationConstructionManager.CanBuildResearchStation;
            BuildResearchStation = researchStationConstructionManager.BuildResearchStation;

            treatDiseaseManager = new TreatDiseaseManager(player, nodeDiseaseCounters);
            CanTreatDisease = treatDiseaseManager.CanTreat;
            TreatDisease = treatDiseaseManager.Treat;

            shareKnowledgeManager = new ShareKnowledgeManager(player, players);
            CanShareKnowledge = shareKnowledgeManager.CanShareKnowledge;
            ShareKnowledge = shareKnowledgeManager.ShareKnowledge;

            discoverCureManager = new DiscoverCureManager(player, diseases);
            CanDiscoverCure = discoverCureManager.CanDiscoverCure;
            DiscoverCure = discoverCureManager.DiscoverCure;
        }
    }
}
