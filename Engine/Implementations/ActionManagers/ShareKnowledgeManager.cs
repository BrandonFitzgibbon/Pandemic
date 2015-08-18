using Engine.Implementations.ActionItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionManagers
{
    public class ShareKnowledgeManager
    {
        private Player player;
        private IEnumerable<Player> players;

        internal IEnumerable<ShareKnowledgeItem> Targets { get; private set; }

        internal ShareKnowledgeManager(Player player, IEnumerable<Player> players)
        {
            this.player = player;
            this.players = players;

            this.player.ActionCounter.ActionUsed += ActionUsed;

            foreach (Player plyr in players)
            {
                plyr.Moved += Moved;
                plyr.Hand.HandChanged += HandChanged;
            }

             Update();
        }

        internal bool CanShareKnowledge(ShareKnowledgeItem shareKnowledgeItem)
        {
            return shareKnowledgeItem != null;
        }

        internal void ShareKnowledge(ShareKnowledgeItem shareKnowledgeItem)
        {
            if(CanShareKnowledge(shareKnowledgeItem))
            {
                shareKnowledgeItem.Giver.Hand.RemoveFromHand(shareKnowledgeItem.CityCard);
                shareKnowledgeItem.Taker.Hand.AddToHand(shareKnowledgeItem.CityCard);
                player.ActionCounter.UseAction(1);
            }
        }

        private void Update()
        {
            Targets = GetTargets();
        }

        private void ActionUsed(object sender, EventArgs e)
        {
            Update();
        }

        private void Moved(object sender, CustomEventArgs.PlayerMovedEventArgs e)
        {
            Update();
        }

        private void HandChanged(object sender, EventArgs e)
        {
            Update();
        }

        private IEnumerable<ShareKnowledgeItem> GetTargets()
        {
            List<ShareKnowledgeItem> targets = new List<ShareKnowledgeItem>();

            if (player.ActionCounter.Count < 1)
                return targets;

            foreach (CityCard cityCard in player.Hand.CityCards.Where(i => i.Node == player.Location))
            {
                foreach (Player plyr in players.Where(i => i.Location == player.Location && i != player))
                {
                    targets.Add(new ShareKnowledgeItem(player, plyr, cityCard));
                }
            }

            foreach (Player plyr in players.Where(i => i.Location == player.Location && i != player))
            {
                foreach (CityCard cityCard in plyr.Hand.CityCards.Where(i => i.Node == player.Location))
                {
                    targets.Add(new ShareKnowledgeItem(plyr, player, cityCard));
                }
            }

            return targets;
        }
    }
}
