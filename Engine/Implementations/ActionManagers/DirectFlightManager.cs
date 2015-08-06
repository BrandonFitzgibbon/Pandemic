using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionManagers
{
    internal class DirectFlightManager
    {
        private Player player;

        internal Dictionary<Node, CityCard> Destinations { get; private set; }

        internal DirectFlightManager(Player player)
        {
            this.player = player;
            this.player.Hand.HandChanged += HandChanged;
            this.player.ActionCounter.ActionUsed += ActionUsed;
            Destinations = GetDestinations(player.ActionCounter.Count, player.Hand);
        }

        internal bool CanDirectFlight(Node node)
        {
            return Destinations.ContainsKey(node);
        }

        internal void DirectFlight(CityCard cityCard)
        {
            if (CanDirectFlight(cityCard.Node))
            {
                cityCard.Discard();
                player.Move(cityCard.Node);
                player.ActionCounter.UseAction(1);
            }
        }

        private void Update()
        {
            Destinations = GetDestinations(player.ActionCounter.Count, player.Hand);
        }

        private void HandChanged(object sender, EventArgs e)
        {
            Update();
        }

        private void ActionUsed(object sender, EventArgs e)
        {
            Update();
        }

        private Dictionary<Node, CityCard> GetDestinations(int actionsLeft, Hand hand)
        {
            Dictionary<Node, CityCard> destinations = new Dictionary<Node, CityCard>();

            if (actionsLeft <= 0)
                return destinations;

            foreach (CityCard cityCard in hand.CityCards)
            {
                destinations.Add(cityCard.Node, cityCard);
            }

            return destinations;
        }
    }
}
