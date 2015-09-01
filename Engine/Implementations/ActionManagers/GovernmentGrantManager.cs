using Engine.CustomEventArgs;
using Engine.Implementations.ActionItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionManagers
{
    internal class GovernmentGrantManager
    {
        private IEnumerable<Node> nodes;
        private ActionCard<GovernmentGrantItem> card;
        private ResearchStationCounter researchStationCounter;

        internal IEnumerable<GovernmentGrantItem> Targets { get; private set; }

        public GovernmentGrantManager(ActionCard<GovernmentGrantItem> card, IEnumerable<Node> nodes, ResearchStationCounter researchStationCounter)
        {
            this.nodes = nodes;
            this.card = card;
            this.researchStationCounter = researchStationCounter;
            this.researchStationCounter.ResearchStationConstructed += ResearchStationChanged;

            Update();
        }

        internal bool CanComissionResearchStation(GovernmentGrantItem ggi)
        {
            return ggi != null && (researchStationCounter.CanBuildResearchStation(ggi.ConstructionNode) || researchStationCounter.CanMoveResearchStation(ggi.DeconstructionNode, ggi.ConstructionNode));
        }

        internal void ComissionResearchStation(GovernmentGrantItem ggi)
        {
            if (researchStationCounter.CanBuildResearchStation(ggi.ConstructionNode))
            {
                researchStationCounter.BuildResearchStation(ggi.ConstructionNode);
                card.Discard();
            }
            else if (researchStationCounter.CanMoveResearchStation(ggi.DeconstructionNode, ggi.ConstructionNode))
            {
                researchStationCounter.MoveResearchStation(ggi.DeconstructionNode, ggi.ConstructionNode);
                card.Discard();
            }
        }

        private void Update()
        {
            Targets = GetTargets();
        }

        private void ResearchStationChanged(object sender, ResearchStationChangedEventArgs e)
        {
            Update();
        }

        private IEnumerable<GovernmentGrantItem> GetTargets()
        {
            List<GovernmentGrantItem> targets = new List<GovernmentGrantItem>();

            foreach (Node node in nodes.Where(i => researchStationCounter.CanBuildResearchStation(i)))
            {
                targets.Add(new GovernmentGrantItem(node, null));
            }

            foreach (Node deconstructionNode in nodes.Where(i => i.ResearchStation))
            {
                foreach (Node constructionNode in nodes.Where(i => researchStationCounter.CanMoveResearchStation(deconstructionNode, i)))
                {
                    targets.Add(new GovernmentGrantItem(constructionNode, deconstructionNode));
                }
            }

            return targets;
        }

    }
}
