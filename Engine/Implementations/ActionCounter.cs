using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class ActionCounter
    {
        private int count;
        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                if (value == 0)
                    if (ActionsDepleted != null) ActionsDepleted(this, EventArgs.Empty);
            }
        }

        internal void ResetActions()
        {
            Count = 4;
        }

        internal void UseAction(int value)
        {
            Count = Count - value;
            if (ActionUsed != null) ActionUsed(this, EventArgs.Empty);
        }

        public event EventHandler ActionsDepleted;
        public event EventHandler ActionUsed;
    }
}
