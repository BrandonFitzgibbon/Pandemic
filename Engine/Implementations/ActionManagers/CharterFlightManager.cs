using Engine.Contracts;
using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionManagers
{
    public class CharterFlightManager
    {
        private Player player;
        private IEnumerable<INode> nodes;
        public Dictionary<INode, ICityCard> Destinations { get; private set; }

        public CharterFlightManager(Player player, IEnumerable<INode> nodes)
        {
            this.player = player;
            this.nodes = nodes;
            player.Moved += PlayerMoved;
            player.Hand.HandChanged += HandChanged;
            Destinations = GetDestinations(player.ActionCounter.Count, player);
        }

        internal bool CanCharterFlight(INode node, ICityCard cityCard)
        {
            return Destinations.ContainsKey(node) && Destinations[node] == cityCard;
        }

        internal void CharterFlight(INode node, ICityCard cityCard)
        {
            if (CanCharterFlight(cityCard.Node, cityCard))
            {
                player.ActionCounter.UseAction(1);
                cityCard.Discard();
                player.Move(node);
                Update();
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

        private Dictionary<INode, ICityCard> GetDestinations(int actionsLeft, IPlayer player)
        {
            Dictionary<INode, ICityCard> destinations = new Dictionary<INode, ICityCard>();

            if (actionsLeft <= 0)
                return destinations;

            foreach (ICityCard cityCard in player.Hand.CityCards)
            {
                if(cityCard.Node == player.Location)
                {
                    foreach (INode node in nodes)
                    {
                        destinations.Add(node, cityCard);
                    }
                }
            }

            return destinations;
        }
    }
}
