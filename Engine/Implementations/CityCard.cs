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
    }
}
