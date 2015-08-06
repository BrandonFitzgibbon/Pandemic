using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionItems
{
    public class TreatDiseaseItem
    {
        public NodeDiseaseCounter NodeDiseaseCounter { get; private set; }        
        public int TreatmentValue { get; private set; }
        public int Cost { get; private set; }

        public TreatDiseaseItem(NodeDiseaseCounter nodeDiseaseCounter, int treatmentValue, int cost)
        {
            NodeDiseaseCounter = nodeDiseaseCounter;
            TreatmentValue = treatmentValue;
            Cost = cost;
        }
    }
}
