using Engine.Contracts;
using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class Counter : IDiseaseCounter
    {
        private ICity city;

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

        public Counter(IDisease disease, ICity city)
        {
            this.disease = disease;
            this.city = city;
            this.count = 0;
        }

        public void Increase(int rate)
        {
            for (int i = 0; i < rate; i++)
            {
                if (count < 3)
                {
                    disease.Decrease();
                    count++;
                }
                else
                    if (Outbreak != null) Outbreak(this, new OutbreakEventArgs(city, disease));
            }
        }

        public void Decrease(int rate)
        {
            for (int i = 0; i < rate; i++)
            {
                if (count > 0)
                {
                    count--;
                    disease.Increase();
                }
            }
        }

        public override string ToString()
        {
            return disease.ToString() + ": " + count.ToString();
        }

        public event EventHandler<OutbreakEventArgs> Outbreak;
    }
}
