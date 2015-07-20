using Engine.Contracts;
using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class OutbreakCounter : IOutbreakCounter
    {
        private int count;
        public int Count
        {
            get { return count; }
        }

        public OutbreakCounter(IList<ICity> cities)
        {
            this.count = 0;

            foreach (ICity city in cities)
            {
                foreach (IDiseaseCounter diseaseCounter in city.Counters)
                {
                    diseaseCounter.Outbreak += diseaseCounterOutbreak;
                }
            }
        }

        private void diseaseCounterOutbreak(object sender, OutbreakEventArgs e)
        {
            Increase();
        }

        private void Increase()
        {
            if (count < 7)
                count++;
            else
                if (GameOver != null) GameOver(this, EventArgs.Empty);
        }

        public event EventHandler GameOver;
    }
}
