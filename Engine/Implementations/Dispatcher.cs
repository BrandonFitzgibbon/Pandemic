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
        public Dispatcher(IGame game, string name, IHand hand) : base(game, name, hand) { }

        public void DispatchDrive(Player player, ICity destinationCity)
        {
            player.Drive();
        }

        public void Dispatch(Player player, Player playerDestination)
        {
            player.ShuttleFlight();
        }
    }
}
