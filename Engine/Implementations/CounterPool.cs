using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class CounterPool : ICounterPool
    {
        private IDisease disease;
        public IDisease Disease
        {
            get { return disease; }
        }

        private int count;
        public int Count
        {
            get { return count; }
        }

        public void Decrease()
        {
            if (count > 0)
                count--;
            else
                if (GameOver != null) GameOver(this, EventArgs.Empty);
        }

        public void Increase()
        {
            if (count < 24)
                count++;
            else
                count = 24;
        }

        public CounterPool(IDisease disease)
        {
            this.disease = disease;
            count = 24;
        }

        public event EventHandler GameOver;
    }
}
