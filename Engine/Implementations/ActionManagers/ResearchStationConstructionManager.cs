using Engine.CustomEventArgs;
using Engine.Implementations.ActionItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionManagers
{
    public class ResearchStationConstructionManager
    {
        private Player player;
        private ResearchStationCounter counter;
        private IEnumerable<Node> nodes;

        internal IEnumerable<ResearchStationConstructionItem> Targets { get; private set; }

        internal ResearchStationConstructionManager(Player player, ResearchStationCounter counter, IEnumerable<Node> nodes)
        {
            this.player = player;
            this.player.Hand.HandChanged += HandChanged;
            this.player.Moved += PlayerMoved;
            this.player.ActionCounter.ActionUsed += ActionUsed;
            this.counter = counter;
            this.nodes = nodes;
            Update();
        }

        internal bool CanBuildResearchStation(ResearchStationConstructionItem researchStationConstructionItem)
        {
            return researchStationConstructionItem != null;
        }

        internal void BuildResearchStation(ResearchStationConstructionItem researchStationConstructionItem)
        {
            if(CanBuildResearchStation(researchStationConstructionItem))
            {
                if (researchStationConstructionItem.Node != null)
                    researchStationConstructionItem.Node.ResearchStation = false;
                researchStationConstructionItem.CityCard.Discard();
                researchStationConstructionItem.CityCard.Node.ResearchStation = true;
                player.ActionCounter.UseAction(1);
            }
        }

        private void Update()
        {
            Targets = GetTargets();
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

        private IEnumerable<ResearchStationConstructionItem> GetTargets()
        {
            List<ResearchStationConstructionItem> targets = new List<ResearchStationConstructionItem>();

            if (player.ActionCounter.Count < 1)
                return targets;

            foreach (CityCard cityCard in player.Hand.CityCards.Where(i => i.Node == player.Location && i.Node.ResearchStation == false))
            {
                if (counter.Count > 0)
                {
                    targets.Add(new ResearchStationConstructionItem(cityCard, null));
                }
                else
                {
                    foreach (Node node in nodes.Where(i => i.ResearchStation == true))
                    {
                        targets.Add(new ResearchStationConstructionItem(cityCard, node));
                    }
                }
            }

            return targets;
        }
    }
}
