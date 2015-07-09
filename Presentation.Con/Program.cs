using Engine.Contracts;
using Engine.Implementations;
using Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Presentation.Con
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument cities = XDocument.Load("Cities.XML");
            foreach (XElement city in cities.Descendants("City"))   
            {
                string cityName = city.Element("Name").Value;
                string countryName = city.Element("Country").Value;
                int population = int.Parse(city.Element("Population").Value);
                List<string> connectionNames = new List<string>();
                foreach (XElement con in city.Element("Connections").Descendants())
                {
                    connectionNames.Add(con.Value);
                }
            }
        }
    }
}
