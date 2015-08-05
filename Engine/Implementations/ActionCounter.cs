using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class ActionCounter : IActionCounter
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

        public void ResetActions()
        {
            Count = 4;
        }

        public void UseAction(int value)
        {
            Count = Count - value;
        }

        public event EventHandler ActionsDepleted;
    }
}
