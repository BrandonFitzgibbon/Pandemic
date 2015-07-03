using Engine.Contracts;
using Engine.Implementations;
using Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class Cities
    {
        public static IList<ICity> GetAll()
        {
            IList<ICity> cities = new List<ICity>();
            using (var context = new DataEntities())
            {
                foreach (DataAccess.City city in context.Cities)
                {
                    cities.Add(new Engine.Implementations.City(city.Id, city.Name, city.Country, (int)city.Population));
                }
            }
            return cities;
        }
    }
}
