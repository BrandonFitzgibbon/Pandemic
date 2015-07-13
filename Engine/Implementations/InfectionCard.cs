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
        public ICity City
        {
            get { return city; }
        }

        public InfectionCard(ICity city)
        {
            this.city = city;
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
                return city.Equals(compare.City);
            return false;
        }
    }
}
