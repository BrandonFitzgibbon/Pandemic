using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class Dispatcher : Player, IDispatcher
    {
        public Dispatcher(string name) : base(name) { }

        public void DispatchDrive(IPlayer player, ICity destinationCity)
        {
            player.Drive(destinationCity);
        }

        public void Dispatch(IPlayer player, IPlayer playerDestination)
        {
            player.ShuttleFlight(playerDestination.Location);
        }
    }
}
