using Engine.Implementations.ActionItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionManagers
{
    public class AirliftManager
    {
        private IEnumerable<Node> nodes;
        private IEnumerable<Player> players;
        private ActionCard<AirliftItem> card;

        internal IEnumerable<AirliftItem> Targets { get; private set; }

        public AirliftManager(ActionCard<AirliftItem> card, IEnumerable<Node> nodes, IEnumerable<Player> players)
        {
            this.nodes = nodes;
            this.players = players;
            this.card = card;

            foreach (Player player in this.players)
            {
                player.Moved += Moved;
            }

            Update();
        }

        internal bool CanAirlift(AirliftItem ali)
        {
            return ali != null && ali.Player != null && ali.Node != null;
        }

        internal void Airlift(AirliftItem ali)
        {
            if(CanAirlift(ali))
            {
                ali.Player.Move(ali.Node);
                card.Discard();
            }
        }

        private void Update()
        {
            Targets = GetTargets();
        }

        private void Moved(object sender, CustomEventArgs.PlayerMovedEventArgs e)
        {
            Update();
        }

        private IEnumerable<AirliftItem> GetTargets()
        {
            List<AirliftItem> targets = new List<AirliftItem>();

            foreach (Player player in players)
            {
                foreach (Node node in nodes.Where(i => i != player.Location))
                {
                    targets.Add(new AirliftItem(player, node));
                }
            }

            return targets;
        }
    }
}
