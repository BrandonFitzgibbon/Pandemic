using Engine.Contracts;
using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class NodeDiseaseCounter : INodeDiseaseCounter
    {
        public INode Node { get; private set; }
        public IDisease Disease { get; private set; }
        public int Count { get; private set; }

        public NodeDiseaseCounter(IDisease disease, INode node)
        {
            Disease = disease;
            Node = node;
            Count = 0;
        }

        public void Infection(int rate)
        {
            for (int i = 0; i < rate; i++)
            {
                if (Count < 3)
                {
                    Count++;
                    if (Infected != null) Infected(this, EventArgs.Empty);
                }
                else
                    if (Outbreak != null) Outbreak(this, new OutbreakEventArgs(this));
            }
        }

        public void Treatment(int rate)
        {
            for (int i = 0; i < rate; i++)
            {
                if (Count > 0)
                {
                    Count--;
                    if (Treated != null) Treated(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler<OutbreakEventArgs> Outbreak;
        public event EventHandler Infected;
        public event EventHandler Treated;
    }
}
