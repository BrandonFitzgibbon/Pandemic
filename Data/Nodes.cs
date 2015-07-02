using Engine.Contracts;
using Engine.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class Nodes
    {
        public static IList<INode> GetAll()
        {
            List<ICity> cities = Cities.GetAll().ToList();
            List<IDisease> diseases = Diseases.GetAll().ToList();
            IList<INode> nodes = new List<INode>();

            List<ICounter> counters = new List<ICounter>();
            foreach (IDisease disease in diseases)
            {
                counters.Add(new Counter(disease));
            }

            //Yellow
            nodes.Add(new Node(cities.Single(i => i.Name == "Bogota"), diseases.Single(i => i.Id == 1), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Buenos Aires"), diseases.Single(i => i.Id == 1), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Johannesburg"), diseases.Single(i => i.Id == 1), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Khartoum"), diseases.Single(i => i.Id == 1), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Kinshasa"), diseases.Single(i => i.Id == 1), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Lagos"), diseases.Single(i => i.Id == 1), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Lima"), diseases.Single(i => i.Id == 1), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Mexico City"), diseases.Single(i => i.Id == 1), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Miami"), diseases.Single(i => i.Id == 1), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Santiago"), diseases.Single(i => i.Id == 1), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Sao Paulo"), diseases.Single(i => i.Id == 1), counters));

            //Red
            nodes.Add(new Node(cities.Single(i => i.Name == "Bangkok"), diseases.Single(i => i.Id == 2), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Beijing"), diseases.Single(i => i.Id == 2), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Ho Chi Minh City"), diseases.Single(i => i.Id == 2), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Hong Kong"), diseases.Single(i => i.Id == 2), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Jakarta"), diseases.Single(i => i.Id == 2), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Manila"), diseases.Single(i => i.Id == 2), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Osaka"), diseases.Single(i => i.Id == 2), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Seoul"), diseases.Single(i => i.Id == 2), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Shanghai"), diseases.Single(i => i.Id == 2), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Sydney"), diseases.Single(i => i.Id == 2), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Taipei"), diseases.Single(i => i.Id == 2), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Tokyo"), diseases.Single(i => i.Id == 2), counters));

            //Blue
            nodes.Add(new Node(cities.Single(i => i.Name == "Atlanta"), diseases.Single(i => i.Id == 3), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Chicago"), diseases.Single(i => i.Id == 3), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Essen"), diseases.Single(i => i.Id == 3), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "London"), diseases.Single(i => i.Id == 3), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Madrid"), diseases.Single(i => i.Id == 3), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Milan"), diseases.Single(i => i.Id == 3), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Montreal"), diseases.Single(i => i.Id == 3), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "New York"), diseases.Single(i => i.Id == 3), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Paris"), diseases.Single(i => i.Id == 3), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "San Francisco"), diseases.Single(i => i.Id == 3), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "St. Petersburg"), diseases.Single(i => i.Id == 3), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Washington"), diseases.Single(i => i.Id == 3), counters));

            //Black
            nodes.Add(new Node(cities.Single(i => i.Name == "Algiers"), diseases.Single(i => i.Id == 4), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Baghdad"), diseases.Single(i => i.Id == 4), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Cairo"), diseases.Single(i => i.Id == 4), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Chennai"), diseases.Single(i => i.Id == 4), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Delhi"), diseases.Single(i => i.Id == 4), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Istanbul"), diseases.Single(i => i.Id == 4), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Karachi"), diseases.Single(i => i.Id == 4), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Kolkata"), diseases.Single(i => i.Id == 4), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Moscow"), diseases.Single(i => i.Id == 4), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Mumbai"), diseases.Single(i => i.Id == 4), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Riyadh"), diseases.Single(i => i.Id == 4), counters));
            nodes.Add(new Node(cities.Single(i => i.Name == "Tehran"), diseases.Single(i => i.Id == 4), counters));

            foreach (INode node in nodes)
            {
                //node.FormConnection(nodes.Single(i => i.City.Name == ""));
                switch (node.City.Name)
                {
                    case "Bogota":
                        node.FormConnection(nodes.Single(i => i.City.Name == "Buenos Aires"));
                        node.FormConnection(nodes.Single(i => i.City.Name == "Lima"));
                        node.FormConnection(nodes.Single(i => i.City.Name == "Mexico City"));
                        node.FormConnection(nodes.Single(i => i.City.Name == "Miami"));
                        node.FormConnection(nodes.Single(i => i.City.Name == "Sao Paulo"));
                        break;

                    case "Buenos Aires":
                        node.FormConnection(nodes.Single(i => i.City.Name == "Bogota"));
                        node.FormConnection(nodes.Single(i => i.City.Name == "Sao Paulo"));
                        break;

                    case "Johannesburg":
                        node.FormConnection(nodes.Single(i => i.City.Name == "Khartoum"));
                        node.FormConnection(nodes.Single(i => i.City.Name == "Kinshasa"));
                        break;

                    case "Khartoum":
                        node.FormConnection(nodes.Single(i => i.City.Name == "Cairo"));
                        node.FormConnection(nodes.Single(i => i.City.Name == "Johannesburg"));
                        node.FormConnection(nodes.Single(i => i.City.Name == "Kinshasa"));
                        node.FormConnection(nodes.Single(i => i.City.Name == "Lagos"));
                        break;
                }
            }

            return nodes;
        }
    }
}
