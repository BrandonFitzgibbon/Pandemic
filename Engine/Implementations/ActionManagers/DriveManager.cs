using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionManagers
{
    public class DriveManager
    {
        private Player player;
        public Dictionary<INode, int> Destinations { get; private set; }

        public DriveManager(Player player)
        {
            this.player = player;
            player.Moved += PlayerMoved;
            Destinations = GetDestinations(player.ActionCounter.Count);
        }

        internal bool CanDrive(INode node)
        {
            return Destinations.ContainsKey(node) && player.ActionCounter.Count >= Destinations[node];
        }

        internal void Drive(INode node)
        {
            if (CanDrive(node))
            {
                player.ActionCounter.UseAction(Destinations[node]);
                player.Move(node);
                Update();
            }
        }

        private void Update()
        {
            Destinations = GetDestinations(player.ActionCounter.Count);
        }

        private void PlayerMoved(object sender, CustomEventArgs.PlayerMovedEventArgs e)
        {
            Update();
        }

        private Dictionary<INode, int> GetDestinations(int actionsLeft)
        {
            Dictionary<INode, int> destinations = new Dictionary<INode, int>();
            AddDestinations(destinations, 1, actionsLeft, player.Location, player.Location);
            return destinations;
        }

        private void AddDestinations(Dictionary<INode, int> dictionary, int cost, int actionsLeft, INode node, INode origin)
        {
            int initialCost = cost;

            foreach (INode connection in node.Connections)
            {
                cost = initialCost;

                if (connection == origin)
                    continue;

                if (!dictionary.ContainsKey(connection))
                {
                    dictionary.Add(connection, cost);
                }
                else if(dictionary.ContainsKey(connection) && dictionary[connection] > cost)
                {
                    dictionary[connection] = cost;
                }

                cost++;

                if (cost <= actionsLeft)
                    AddDestinations(dictionary, cost, actionsLeft, connection, origin);
            }
        }
    }
}
