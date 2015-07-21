using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation
{
    public static class DriveValidation
    {
        public static Dictionary<ICity, int> DriveDestinations(IPlayer player, int actionsAvailable)
        {
            Dictionary<ICity, int> driveDestinations = new Dictionary<ICity, int>();
            AddConnectionsToDriveDestinations(driveDestinations, player.Location, player.Location, 1, actionsAvailable);             
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
