using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionManagers
{
    public class DirectFlightManager
    {
        private Player player;
        public Dictionary<INode, ICityCard> Destinations { get; private set; }

        public DirectFlightManager(Player player)
        {
            this.player = player;
            player.Hand.HandChanged += HandChanged;
            Destinations = GetDestinations(player.ActionCounter.Count, player.Hand);
        }

        internal bool CanDirectFlight(INode node)
        {
            return Destinations.ContainsKey(node);
        }

        internal void DirectFlight(ICityCard cityCard)
        {
            if (CanDirectFlight(cityCard.Node))
            {
                player.ActionCounter.UseAction(1);
                cityCard.Discard();
                player.Move(cityCard.Node);
                Update();
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

        private Dictionary<INode, ICityCard> GetDestinations(int actionsLeft, IHand hand)
        {
            Dictionary<INode, ICityCard> destinations = new Dictionary<INode, ICityCard>();

            if (actionsLeft <= 0)
                return destinations;

            foreach (ICityCard cityCard in hand.CityCards)
            {
                destinations.Add(cityCard.Node, cityCard);
            }

            return destinations;
        }
    }
}
