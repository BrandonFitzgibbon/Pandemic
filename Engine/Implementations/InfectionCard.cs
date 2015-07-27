using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class InfectionCard : Card, IInfectionCard
    {
        private ICity city;
        
        public string Name
        {
            get { return city.Name; }
        }

        public IDisease Disease
        {
            get { return city.Disease; }
        }

        public InfectionCard(ICity city)
        {
            this.city = city;
        }

        public void Infect(int rate)
        {
            city.Counters.Single(i => i.Disease == city.Disease).Increase(rate);
            this.Discard();
        }

        public override string ToString()
        {
            return city.ToString();
        }

        public override int GetHashCode()
        {
            return city.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            IInfectionCard compare = (IInfectionCard)obj;
            if (compare != null)
                return compare.Name == this.Name;
            return false;
        }
    }
}
