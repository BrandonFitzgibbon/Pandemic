using Engine.Contracts;
using Engine.Factories;
using Engine.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess
{
    public class Data : IDataAccess
    {
        private List<IDisease> diseases;
        private List<ICity> cities;
        private Dictionary<ICity, IList<string>> connectionDictionary;

        public Data()
        {
            diseases = new List<IDisease>()
            {
                new Disease("Yellow", DiseaseType.Yellow),
                new Disease("Red", DiseaseType.Red),
                new Disease("Blue", DiseaseType.Blue),
                new Disease("Black", DiseaseType.Black)
            };

            cities = new List<ICity>();

            connectionDictionary = new Dictionary<ICity, IList<string>>();

            XDocument xmlCities = XDocument.Load("Cities.XML");
            foreach (XElement city in xmlCities.Descendants("City"))
            {
                string cityName = city.Element("Name").Value;
                string countryName = city.Element("Country").Value;
                int population = int.Parse(city.Element("Population").Value);
                string cityDisease = city.Element("Disease").Value;
                List<string> connectionNames = new List<string>();
                foreach (XElement con in city.Element("Connections").Descendants())
                {
                    connectionNames.Add(con.Value);
                }

                ICity newCity = new City(cityName, countryName, population, diseases.Single(i => i.Type.ToString() == cityDisease), new DiseaseCounterFactory(diseases));
                cities.Add(newCity);
                connectionDictionary.Add(newCity, connectionNames);
            }

            foreach (ICity city in cities)
            {
                foreach (string cityName in ResolveCityConnections(city))
                {
                    city.Connect(cities.Single(i => i.Name == cityName));
                }
            }

        }

        public IList<IDisease> GetDiseases()
        {
            return this.diseases;
        }

        public IList<ICity> GetCities()
        {
            return this.cities;
        }

        private IList<string> ResolveCityConnections(ICity city)
        {
            return connectionDictionary[city];
        }
    }
}
