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
        private List<Disease> diseases;
        private List<Node> nodes;
        private Dictionary<Node, IList<string>> connectionDictionary;

        public Data()
        {
            diseases = new List<Disease>()
            {
                new Disease("Yellow", DiseaseType.Yellow),
                new Disease("Red", DiseaseType.Red),
                new Disease("Blue", DiseaseType.Blue),
                new Disease("Black", DiseaseType.Black)
            };

            nodes = new List<Node>();

            connectionDictionary = new Dictionary<Node, IList<string>>();

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

                Node newNode = new Node(new City(cityName, countryName, population), diseases.Single(i => i.Type.ToString() == cityDisease));
                nodes.Add(newNode);
                connectionDictionary.Add(newNode, connectionNames);
            }

            foreach (Node node in nodes)
            {
                foreach (string cityName in ResolveCityConnections(node))
                {
                    node.Connect(nodes.Single(i => i.City.Name == cityName));
                }
            }
        }

        private IList<string> ResolveCityConnections(Node node)
        {
            return connectionDictionary[node];
        }

        public IEnumerable<Disease> GetDiseases()
        {
            return this.diseases;
        }

        public IEnumerable<Node> GetNodes()
        {
            return this.nodes;
        }
    }
}
