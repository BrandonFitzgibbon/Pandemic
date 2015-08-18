using Engine.Contracts;
using Engine.CustomEventArgs;
using Engine.Implementations.ActionItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionManagers
{
    internal class ShuttleFlightManager
    {
        private Player player;
        private IEnumerable<Node> nodes;

        internal IEnumerable<ShuttleFlightItem> Destinations { get; private set; }

        internal ShuttleFlightManager(Player player, IEnumerable<Node> nodes)
        {
            this.player = player;
            this.nodes = nodes;
            this.player.Moved += PlayerMoved;
            this.player.ActionCounter.ActionUsed += ActionUsed;
            Update();
        }

        internal bool CanShuttleFlight(ShuttleFlightItem shuttleFlightItem)
        {
            return shuttleFlightItem != null;
        }

        internal void ShuttleFlight(ShuttleFlightItem shuttleFlightItem)
        {
            if (CanShuttleFlight(shuttleFlightItem))
            {
                player.Move(shuttleFlightItem.Node);
                player.ActionCounter.UseAction(1);
            }
        }

        private void Update()
        {
            Destinations = GetDestinations();
        }

        private void PlayerMoved(object sender, PlayerMovedEventArgs e)
        {
            Update();
        }

        private void ActionUsed(object sender, EventArgs e)
        {
            Update();
        }

        private IEnumerable<ShuttleFlightItem> GetDestinations()
        {
            List<ShuttleFlightItem> destinations = new List<ShuttleFlightItem>();

            if (player.ActionCounter.Count < 1 || !player.Location.ResearchStation)
                return destinations;

            foreach (Node node in nodes)
            {
                if (node.ResearchStation && node != player.Location)
                    destinations.Add(new ShuttleFlightItem(node));
            }

            return destinations;
        }
    }
}
