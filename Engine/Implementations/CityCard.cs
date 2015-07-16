using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class CityCard : Card, ICityCard
    {
        private ICity city;
        public ICity City
        {
            get { return city; }
        }

        public CityCard(ICity city)
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
            ICityCard compare = obj as ICityCard;
            if (compare != null)
                return city.Equals(compare.City);
            return false;
        }
    }
}
