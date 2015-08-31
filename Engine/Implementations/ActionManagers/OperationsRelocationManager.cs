using Engine.CustomEventArgs;
using Engine.Implementations.ActionItems;
using Engine.Implementations.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionManagers
{
    internal class OperationsRelocationManager
    {
        private Player player;
        private IEnumerable<Node> nodes;
        private bool usedThisTurn = false;

        internal IEnumerable<OperationsRelocationItem> Destinations { get; private set; }

        public OperationsRelocationManager(Player player, IEnumerable<Node> nodes)
        {
            if(player is OperationsExpert)
            {
                this.nodes = nodes;
                this.player = player;
                this.player.Moved += PlayerMoved;
                this.player.ActionCounter.ActionUsed += ActionUsed;
                Update();
            }
        }

        internal bool CanRelocate(OperationsRelocationItem ori)
        {
            return ori != null && ori.DiscardOption != null;
        }

        internal void Relocate(OperationsRelocationItem ori)
        {
            if (CanRelocate(ori))
            {
                player.Move(ori.Destination);
                player.ActionCounter.UseAction(1);
                ori.DiscardOption.Discard();
                usedThisTurn = true;
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

        private IEnumerable<OperationsRelocationItem> GetDestinations()
        {
            List<OperationsRelocationItem> destinations = new List<OperationsRelocationItem>();

            if (player.Location.ResearchStation == true && usedThisTurn == false)
            {
                foreach (Node node in nodes.Where(i => i != player.Location))
                {
                    destinations.Add(new OperationsRelocationItem(node));
                }
            }

            return destinations;
        }
    }
}
