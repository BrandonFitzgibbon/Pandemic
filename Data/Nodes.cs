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
    public static class Nodes
    {
        public static IList<INode> GetAll()
        {
            List<ICity> cities = Cities.GetAll().ToList();
            List<IDisease> diseases = Diseases.GetAll().ToList();
            List<ICounter> counters = new List<ICounter>(CounterBuilder.GetCounters(diseases));
            IList<INode> nodes = new List<INode>();

            using (var context = new DataEntities())
            {
                foreach (DataAccess.Node node in context.Nodes)
                {
                    nodes.Add(new Engine.Implementations.Node(cities.Single(i => i.Id == node.CityId), diseases.Single(i => i.Id == node.DiseaseId), counters));
                }

                foreach (INode node in nodes)
                {
                    foreach (DataAccess.Connection con in context.Connections.Where(i => i.NodeId == node.City.Id))
                    {
                        node.FormConnection(nodes.Single(i => i.City.Id == con.ConnectionId));
                    }
                }
            }

            return nodes;
        }
    }
}
