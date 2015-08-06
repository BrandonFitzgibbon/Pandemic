using Engine.Contracts;
using Engine.CustomEventArgs;
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

        internal Dictionary<Node, CityCard> Destinations { get; private set; }

        internal CharterFlightManager(Player player, IEnumerable<Node> nodes)
        {
            this.player = player;
            this.nodes = nodes;
            player.Moved += PlayerMoved;
            player.Hand.HandChanged += HandChanged;
            player.ActionCounter.ActionUsed += ActionUsed;
            Destinations = GetDestinations(player.ActionCounter.Count, player);
        }

        internal bool CanCharterFlight(Node node, CityCard cityCard)
        {
            return Destinations.ContainsKey(node) && Destinations[node] == cityCard;
        }

        internal void CharterFlight(Node node, CityCard cityCard)
        {
            if (CanCharterFlight(cityCard.Node, cityCard))
            {
                cityCard.Discard();
                player.Move(node);
                player.ActionCounter.UseAction(1);
            }
        }

        private void Update()
        {
            Destinations = GetDestinations(player.ActionCounter.Count, player);
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

        private Dictionary<Node, CityCard> GetDestinations(int actionsLeft, Player player)
        {
            Dictionary<Node, CityCard> destinations = new Dictionary<Node, CityCard>();

            if (actionsLeft <= 0)
                return destinations;

            foreach (CityCard cityCard in player.Hand.CityCards)
            {
                if(cityCard.Node == player.Location)
                {
                    foreach (Node node in nodes)
                    {
                        destinations.Add(node, cityCard);
                    }
                }
            }

            return destinations;
        }
    }
}
