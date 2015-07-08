using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Data : IDataAccess
    {
        public IList<IDisease> GetDiseases()
        {
            IList<IDisease> diseases = new List<IDisease>();
            using (var context = new DataEntities())
            {
                foreach (Disease disease in context.Diseases)
                {
                    diseases.Add(new Engine.Implementations.Disease(disease.Id) { Name = disease.Name });
                }
            }
            return diseases;
        }

        public IList<ICity> GetCities()
        {
            IList<ICity> cities = new List<ICity>();
            IList<IDisease> diseases = GetDiseases();
            using (var context = new DataEntities())
            {
                foreach (City city in context.Cities)
                {
                    cities.Add(new Engine.Implementations.City(city.Id, city.Name, city.Country, (int)city.Population, diseases.Single(i => i.Id == city.DiseaseId), diseases));
                }

                foreach (ICity city in cities)
                {
                    foreach (Connection con in context.Connections.Where(i => i.CityId == city.Id))
                    {
                        city.FormConnection(cities.Single(i => i.Id == con.ConnectionId));
                    }
                }
            }
            return cities;
        }
    }
}
