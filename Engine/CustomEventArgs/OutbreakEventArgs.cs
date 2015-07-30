using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.CustomEventArgs
{
    public class OutbreakEventArgs
    {
        public INodeDiseaseCounter OriginCounter { get; private set; }

        public OutbreakEventArgs(INodeDiseaseCounter originCounter)
        {
            OriginCounter = originCounter;            
        }
    }
}
