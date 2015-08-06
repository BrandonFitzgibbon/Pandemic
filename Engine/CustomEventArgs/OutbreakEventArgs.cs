using Engine.Contracts;
using Engine.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.CustomEventArgs
{
    public class OutbreakEventArgs
    {
        public NodeDiseaseCounter OriginCounter { get; private set; }

        public OutbreakEventArgs(NodeDiseaseCounter originCounter)
        {
            OriginCounter = originCounter;            
        }
    }
}
