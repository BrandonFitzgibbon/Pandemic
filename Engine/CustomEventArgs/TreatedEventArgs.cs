using Engine.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.CustomEventArgs
{
    public class TreatedEventArgs : EventArgs
    {
        public NodeDiseaseCounter NodeDiseaseCounter { get; private set; }
        public Player Treater { get; private set; }
        public int Value { get; private set; }

        public TreatedEventArgs(NodeDiseaseCounter nodeDiseaseCounter, Player treater, int value)
        {
            NodeDiseaseCounter = nodeDiseaseCounter;
            Treater = treater;
            Value = value;
        }
    }
}
