using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class ResearchStationCounter
    {
        public int Count { get; private set; }

        internal ResearchStationCounter()
        {
            Count = 6;
        }

        internal bool CanBuildResearchStation(Node node)
        {
            return Count > 0 && node != null && !node.ResearchStation;
        }

        internal bool CanMoveResearchStation(Node deconstructionNode, Node constructionNode)
        {
            return Count == 0 && deconstructionNode != null && constructionNode != null && deconstructionNode.ResearchStation && !constructionNode.ResearchStation;
        }

        internal void BuildResearchStation(Node node)
        {
            if (CanBuildResearchStation(node))
            {
                Count--;
                node.ResearchStation = true;
                if (ResearchStationConstructed != null) ResearchStationConstructed(this, new ResearchStationChangedEventArgs(node));
            }
        }

        internal void MoveResearchStation(Node deconstructionNode, Node constructionNode)
        {
            if (CanMoveResearchStation(deconstructionNode, constructionNode))
            {
                deconstructionNode.ResearchStation = false;
                constructionNode.ResearchStation = true;
                if (ResearchStationConstructed != null) ResearchStationConstructed(this, new ResearchStationChangedEventArgs(constructionNode));
                if (ResearchStationDestroyed != null) ResearchStationDestroyed(this, new ResearchStationChangedEventArgs(deconstructionNode));
            }
        }

        public event EventHandler<ResearchStationChangedEventArgs> ResearchStationConstructed;
        public event EventHandler<ResearchStationChangedEventArgs> ResearchStationDestroyed;
    }
}
