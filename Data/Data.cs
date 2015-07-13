using Engine.Contracts;
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
        private Dictionary<ICity, string> diseaseDictionary;
        private Dictionary<ICity, IList<string>> connectionDictionary;

        public Data()
        {
            diseases = new List<IDisease>()
            {
                new Disease("Yellow"),
                new Disease("Red"),
                new Disease("Blue"),
                new Disease("Black")
            };

            cities = new List<ICity>();
            diseaseDictionary = new Dictionary<ICity, string>();
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

                ICity newCity = new City(cityName, countryName, population);
                cities.Add(newCity);
                diseaseDictionary.Add(newCity, cityDisease);
                connectionDictionary.Add(newCity, connectionNames);
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

        public string ResolveCityDisease(ICity city)
        {
            return diseaseDictionary[city];
        }

        public IList<string> ResolveCityConnections(ICity city)
        {
            return connectionDictionary[city];
        }

        public IList<IDiseaseCounter> GetCounters()
        {
            IList<IDiseaseCounter> counters = new List<IDiseaseCounter>();
            foreach (IDisease disease in diseases)
            {
                counters.Add(new Counter(disease));
            }
            return counters;
        }

        public IPlayerDeck GetPlayerDeck()
        {
            List<ICityCard> cityCards = new List<ICityCard>();
            foreach (ICity city in this.cities)
            {
                cityCards.Add(new CityCard(city));
            }
            return new PlayerDeck(cityCards);
        }

        public IInfectionDeck GetInfectionDeck()
        {
            List<IInfectionCard> infectionCards = new List<IInfectionCard>();
            foreach (ICity city in this.cities)
            {
                infectionCards.Add(new InfectionCard(city));
            }
            return new InfectionDeck(infectionCards);
        }
    }
}
