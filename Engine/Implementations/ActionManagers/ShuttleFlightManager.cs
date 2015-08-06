using Engine.Contracts;
using Engine.CustomEventArgs;
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

        internal Dictionary<Node, int> Destinations { get; private set; }

        internal ShuttleFlightManager(Player player, IEnumerable<Node> nodes)
        {
            this.player = player;
            this.nodes = nodes;
            this.player.Moved += PlayerMoved;
            this.player.ActionCounter.ActionUsed += ActionUsed;
            Destinations = GetDestinations(player.ActionCounter.Count, player.Location);
        }

        internal bool CanShuttleFlight(Node node)
        {
            return Destinations.ContainsKey(node);
        }

        internal void ShuttleFlight(Node node)
        {
            if (CanShuttleFlight(node))
            {
                player.Move(node);
                player.ActionCounter.UseAction(1);
            }
        }

        private void Update()
        {
            Destinations = GetDestinations(player.ActionCounter.Count, player.Location);
        }

        private void PlayerMoved(object sender, PlayerMovedEventArgs e)
        {
            Update();
        }

        private void ActionUsed(object sender, EventArgs e)
        {
            Update();
        }

        private Dictionary<Node, int> GetDestinations(int actionsLeft, Node location)
        {
            Dictionary<Node, int> destinations = new Dictionary<Node, int>();

            if (actionsLeft <= 0 && !location.ResearchStation)
                return destinations;

            foreach (Node node in nodes)
            {
                if (node.ResearchStation && node != location)
                    destinations.Add(node, 1);
            }

            return destinations;
        }
    }
}
