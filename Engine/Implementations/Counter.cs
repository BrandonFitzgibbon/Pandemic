using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class Counter : ICounter
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

        public void Increase()
        {
            if (count < 3)
            {
                disease.Pool.Decrease();
                count++;
            }
            else
                if (Outbreak != null) Outbreak(this, EventArgs.Empty);
        }

        public void Decrease()
        {
            if (count > 0)
                count--;
        }

        public Counter(IDisease disease)
        {
            this.disease = disease;
            this.count = 0;
        }

        public event EventHandler Outbreak;
    }
}
