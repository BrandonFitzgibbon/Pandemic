using Engine.Contracts;
using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class DiseaseCounter
    {
        private IEnumerable<NodeDiseaseCounter> nodeCounters;
        private OutbreakCounter outbreakCounter;

        public Disease Disease { get; private set; }
        public int Count { get; private set; }

        public DiseaseCounter(Disease disease, IEnumerable<NodeDiseaseCounter> nodeCounters, OutbreakCounter outbreakCounter)
        {
            this.nodeCounters = nodeCounters;
            this.outbreakCounter = outbreakCounter;

            Disease = disease;
            Count = 24;

            foreach (NodeDiseaseCounter nodeCounter in nodeCounters)
            {
                nodeCounter.Infected += Infection;
                nodeCounter.ChainInfected += Infection;
                nodeCounter.Treated += Treatment;
                nodeCounter.Outbreak += Outbreak;
                nodeCounter.ChainOutbreak += ChainOutbreak;
            }
        }

        private void ChainOutbreak(object sender, OutbreakEventArgs e)
        {
            IEnumerable<NodeDiseaseCounter> outbreakCounters = nodeCounters.Where(i => e.OriginCounter.Node.Connections.Contains(i.Node));
            foreach (NodeDiseaseCounter nodeCounter in outbreakCounters.Where(i => !e.OriginList.Contains(i)))
            {
                if (Count == 0)
                    return;

                if (outbreakCounter.Count == 7)
                    return;

                nodeCounter.OutbreakInfection(e);
            }
        }

        private void Outbreak(object sender, OutbreakEventArgs e)
        {
            IEnumerable<NodeDiseaseCounter> outbreakCounters = nodeCounters.Where(i => e.OriginCounter.Node.Connections.Contains(i.Node));
            foreach (NodeDiseaseCounter nodeCounter in outbreakCounters.Where(i => !e.OriginList.Contains(i)))
            {
                if (Count == 0)
                    return;

                if (outbreakCounter.Count == 7)
                    return;

                nodeCounter.OutbreakInfection(e);
            }
        }

        private void Infection(object sender, InfectionEventArgs e)
        {
            Decrease(e.Value);
        }

        private void Treatment(object sender, TreatedEventArgs e)
        {
            Increase(e.Value);
        }

        internal void Increase(int value)
        {
            for (int i = value; i > 0; i--)
            {
                if (Count < 24)
                    Count++;
            }
        }

        internal void Decrease(int value)
        {
            for (int i = value; i > 0; i--)
            {
                if (Count > 0)
                    Count--;
                else
                    if (GameOver != null) GameOver(this, EventArgs.Empty);
            }
        }

        public event EventHandler GameOver;
    }
}
