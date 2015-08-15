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
    internal class DirectFlightManager
    {
        private Player player;

        internal IEnumerable<DirectFlightItem> Destinations { get; private set; }

        internal DirectFlightManager(Player player)
        {
            this.player = player;
            this.player.Hand.HandChanged += HandChanged;
            this.player.Moved += PlayerMoved;
            this.player.ActionCounter.ActionUsed += ActionUsed;
            Update();
        }

        internal bool CanDirectFlight(DirectFlightItem directFlightItem)
        {
            return directFlightItem != null;
        }

        internal void DirectFlight(DirectFlightItem directFlightItem)
        {
            if(CanDirectFlight(directFlightItem))
            {
                directFlightItem.CityCard.Discard();
                player.Move(directFlightItem.CityCard.Node);
                player.ActionCounter.UseAction(1);
            }
        }

        private void Update()
        {
            Destinations = GetDestinations();
        }

        private void HandChanged(object sender, EventArgs e)
        {
            Update();
        }

        private void PlayerMoved(object sender, PlayerMovedEventArgs e)
        {
            Update();
        }

        private void ActionUsed(object sender, EventArgs e)
        {
            Update();
        }

        private IEnumerable<DirectFlightItem> GetDestinations()
        {
            List<DirectFlightItem> destinations = new List<DirectFlightItem>();

            if (player.ActionCounter.Count < 1)
                return destinations;

            foreach (CityCard cityCard in player.Hand.CityCards)
            {
                if (cityCard.Node != player.Location)
                    destinations.Add(new DirectFlightItem(cityCard));
            }

            return destinations;
        }
    }
}
