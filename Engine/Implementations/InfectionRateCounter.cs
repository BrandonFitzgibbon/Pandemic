using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class InfectionRateCounter
    {
        public InfectionRateCounter()
        {
            count = 0;
        }

        private int count;
        public int Count
        {
            get { return count; }
        }

        public int InfectionRate
        {
            get 
            {
                switch(count)
                {
                    case 0:
                        return 2;
                    case 1:
                        return 2;
                    case 2:
                        return 2;
                    case 3:
                        return 3;
                    case 4:
                        return 3;
                    case 5:
                        return 4;
                    case 6:
                        return 4;
                    default:
                        return 2;
                }
            }
        }

        public void Increase()
        {
            if (count < 6)
            {
                count++;
                if (Increased != null) Increased(this, EventArgs.Empty);
            }
        }

        public event EventHandler Increased;
    }
}
