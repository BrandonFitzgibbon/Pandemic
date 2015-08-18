using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class DrawCounter
    {
        private int count;
        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                if (value == 0)
                    if (DrawsDepleted != null) DrawsDepleted(this, EventArgs.Empty);
            }
        }

        internal void ResetDraws()
        {
            Count = 2;
        }

        internal void Draw()
        {
            Count = Count - 1;
        }

        public event EventHandler DrawsDepleted;
    }
}
