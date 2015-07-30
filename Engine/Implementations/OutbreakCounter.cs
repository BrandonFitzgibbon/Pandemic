using Engine.Contracts;
using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class OutbreakCounter : ICount, INotifyGameOver
    {
        public int Count { get; private set; }

        public OutbreakCounter(IEnumerable<IOutbreak> outbreakNotifiers)
        {
            Count = 0;

            foreach (IOutbreak outbreak in outbreakNotifiers)
            {
                outbreak.Outbreak += Outbreak;
            }
        }

        private void Outbreak(object sender, OutbreakEventArgs e)
        {
            Increase();
        }

        private void Increase()
        {
            if (Count < 7)
                Count++;
            else
                if (GameOver != null) GameOver(this, EventArgs.Empty);
        }

        public event EventHandler GameOver;
    }
}
