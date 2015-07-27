using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Infrastructure
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void SubscribeCitiesToPlayerMovements(this IList<ICity> cities, IList<IPlayer> players)
        {
            foreach (ICity city in cities)
            {
                foreach (IPlayer player in players)
                {
                    city.Subscribe(player);
                }
            }
        }

        public static Dictionary<IDisease, int> GetDiseaseTreatmentOptions(ICity location, int actionCount)
        {
            Dictionary<IDisease, int> diseaseTreatmentOptions = new Dictionary<IDisease, int>();
            if (actionCount > 0)
            {
                foreach (IDiseaseCounter dc in location.Counters)
                {
                    if (dc.Count > 0)
                        diseaseTreatmentOptions.Add(dc.Disease, dc.Count);
                }
            }
            return diseaseTreatmentOptions;
        }

        public static Dictionary<ICity, int> GetDriveDestinations(ICity location, int actionsAvailable)
        {
            Dictionary<ICity, int> driveDestinations = new Dictionary<ICity, int>();
            if (actionsAvailable == 0)
                return driveDestinations;
            AddConnectionsToDriveDestinations(driveDestinations, location, location, 1, actionsAvailable);
            return driveDestinations;
        }

        private static void AddConnectionsToDriveDestinations(IDictionary<ICity, int> destinations, ICity city, ICity origin, int actionsRequired, int actionsAvailable)
        {
            foreach (ICity connection in city.Connections)
            {
                if (!destinations.ContainsKey(connection) && connection != origin)
                    destinations.Add(connection, actionsRequired);
                else if (destinations.ContainsKey(connection))
                {
                    if (destinations[connection] > actionsRequired)
                        destinations[connection] = actionsRequired;
                }
            }

            foreach (ICity connection in city.Connections)
            {
                if (connection != origin && connection.Connections.Count() > 0 && actionsRequired < actionsAvailable)
                    AddConnectionsToDriveDestinations(destinations, connection, origin, actionsRequired + 1, actionsAvailable);
            }
        }
    }
}
