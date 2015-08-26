using Engine.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.CustomEventArgs
{
    public class InfectionEventArgs : EventArgs
    {
        public NodeDiseaseCounter NodeDiseaseCounter { get; private set; }
        public int Value { get; set; }

        public InfectionEventArgs(NodeDiseaseCounter nodeDiseaseCounter, int value)
        {
            NodeDiseaseCounter = nodeDiseaseCounter;
            Value = value;
        }
    }
}
