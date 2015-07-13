using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class OutbreakCounter : IOutbreakCounter
    {
        public OutbreakCounter()
        {
            this.count = 0;
        }

        private int count;
        public int Count
        {
            get { return count; }
        }

        public void Increase()
        {
            if (count < 7)
                count++;
            else
                if (GameOver != null) GameOver(this, EventArgs.Empty);
        }

        public event EventHandler GameOver;
    }
}
