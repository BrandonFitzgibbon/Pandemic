using Engine.Contracts;
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
        private Player player;
        private IEnumerable<INode> nodes;

        private DriveManager driveManager;
        private DirectFlightManager directFlightManager;
        private CharterFlightManager charterFlightManager;

        public Func<INode, bool> CanDrive { get; protected set; }
        public Action<INode> Drive { get; protected set; }
        public Dictionary<INode, int> DriveDestinations 
        {
            get { return driveManager != null ? driveManager.Destinations : null; }
        }

        public Func<INode, bool> CanDirectFlight { get; protected set; }
        public Action<ICityCard> DirectFlight { get; protected set; }
        public Dictionary<INode, ICityCard> DirectFlightDestinations
        {
            get { return directFlightManager != null ? directFlightManager.Destinations : null; }
        }

        public Func<INode, ICityCard, bool> CanCharterFlight { get; protected set; }
        public Action<INode, ICityCard> CharterFlight { get; protected set; }
        public Dictionary<INode, ICityCard> CharterFlightDestinations
        {
            get { return charterFlightManager != null ? charterFlightManager.Destinations : null; }
        }

        public void SetPlayer(Player player, IEnumerable<INode> nodes)
        {
            this.nodes = nodes;

            driveManager = new DriveManager(player);
            CanDrive = driveManager.CanDrive;
            Drive = driveManager.Drive;

            directFlightManager = new DirectFlightManager(player);
            CanDirectFlight = directFlightManager.CanDirectFlight;
            DirectFlight = directFlightManager.DirectFlight;

            charterFlightManager = new CharterFlightManager(player, nodes);
            CanCharterFlight = charterFlightManager.CanCharterFlight;
            CharterFlight = charterFlightManager.CharterFlight;
        }
    }
}
