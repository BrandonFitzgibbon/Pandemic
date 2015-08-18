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
    internal class CharterFlightManager
    {
        private Player player;
        private IEnumerable<Node> nodes;

        internal IEnumerable<CharterFlightItem> Destinations { get; private set; }

        internal CharterFlightManager(Player player, IEnumerable<Node> nodes)
        {
            this.player = player;
            this.nodes = nodes;
            this.player.Hand.HandChanged += HandChanged;
            this.player.Moved += PlayerMoved;
            this.player.ActionCounter.ActionUsed += ActionUsed;
            Update();
        }

        internal bool CanCharterFlight(CharterFlightItem charterFlightItem)
        {
            return charterFlightItem != null;
        }

        internal void CharterFlight(CharterFlightItem charterFlightItem)
        {
            if (CanCharterFlight(charterFlightItem))
            {
                charterFlightItem.CityCard.Discard();
                player.Move(charterFlightItem.Node);
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

        private void HandChanged(object sender, EventArgs e)
        {
            Update();
        }

        private void ActionUsed(object sender, EventArgs e)
        {
            Update();
        }

        private IEnumerable<CharterFlightItem> GetDestinations()
        {
            List<CharterFlightItem> destinations = new List<CharterFlightItem>();

            if (player.ActionCounter.Count < 1)
                return destinations;

            foreach (CityCard cityCard in player.Hand.CityCards)
            {
                if(cityCard.Node == player.Location)
                {
                    foreach (Node node in nodes.Where(i => i != player.Location))
                    {
                        destinations.Add(new CharterFlightItem(cityCard, node));
                    }
                }
            }

            return destinations;
        }
    }
}
