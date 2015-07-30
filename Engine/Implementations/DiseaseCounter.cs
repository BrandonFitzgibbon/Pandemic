using Engine.Contracts;
using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class DiseaseCounter : IDiseaseCounter, INotifyGameOver
    {
        private IEnumerable<INodeDiseaseCounter> nodeCounters;

        public IDisease Disease { get; private set; }
        public int Count { get; private set; }

        public DiseaseCounter(IDisease disease, IEnumerable<INodeDiseaseCounter> nodeCounters)
        {
            this.nodeCounters = nodeCounters;

            Disease = disease;
            Count = 24;

            foreach (INodeDiseaseCounter nodeCounter in nodeCounters)
            {
                nodeCounter.Infection += InfectionNotification;
                nodeCounter.Treatment += TreatmentNotification;
                nodeCounter.Outbreak += OutbreakNotification;              
            }
        }

        private void OutbreakNotification(object sender, OutbreakEventArgs e)
        {
            IEnumerable<INodeDiseaseCounter> outbreakCounters = nodeCounters.Where(i => e.OriginCounter.Node.Connections.Contains(i.Node));
            foreach (INodeDiseaseCounter nodeCounter in outbreakCounters)
            {
                nodeCounter.RaiseInfection(1);
            }
        }

        private void InfectionNotification(object sender, EventArgs e)
        {
            Increase();
        }

        private void TreatmentNotification(object sender, EventArgs e)
        {
            Decrease();
        }

        public void Increase()
        {
            if (Count < 24)
                Count++;
        }

        public void Decrease()
        {
            if (Count > 0)
                Count--;
            else
                if (GameOver != null) GameOver(this, EventArgs.Empty);
        }

        public event EventHandler GameOver;
    }
}
