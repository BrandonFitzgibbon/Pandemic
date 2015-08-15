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
    internal class DriveManager
    {
        private Player player;

        internal IEnumerable<DriveDestinationItem> Destinations { get; private set; }

        internal DriveManager(Player player)
        {
            this.player = player;
            this.player.Moved += PlayerMoved;
            this.player.ActionCounter.ActionUsed += ActionUsed;
            Update();
        }

        internal bool CanDrive(DriveDestinationItem driveDestinationItem)
        {
            return driveDestinationItem != null;
        }

        internal void Drive(DriveDestinationItem driveDestinationItem)
        {
            if (CanDrive(driveDestinationItem))
            {
                player.Move(driveDestinationItem.Node);
                this.player.ActionCounter.UseAction(driveDestinationItem.Cost);
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

        private IEnumerable<DriveDestinationItem> GetDestinations()
        {
            List<DriveDestinationItem> destinations = new List<DriveDestinationItem>();
            AddDestinations(destinations, 1, player.ActionCounter.Count, player.Location, player.Location);
            return destinations;
        }

        private void AddDestinations(List<DriveDestinationItem> items, int cost, int actionsLeft, Node node, Node origin)
        {
            if (actionsLeft == 0)
                return;

            int initialCost = cost;

            foreach (Node connection in node.Connections)
            {
                cost = initialCost;

                if (connection == origin)
                    continue;

                if (items.Where(i => i.Node == connection).Count() == 0)
                    items.Add(new DriveDestinationItem(connection, cost));
                else if (items.SingleOrDefault(i => i.Node == connection) != null && items.Single(i => i.Node == connection).Cost > cost)
                    items.Single(i => i.Node == connection).ChangeCost(cost);

                cost++;

                if (cost <= actionsLeft)
                    AddDestinations(items, cost, actionsLeft, connection, origin);
            }
        }
    }
}
