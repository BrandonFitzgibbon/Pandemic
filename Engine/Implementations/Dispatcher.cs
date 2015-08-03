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
        public Dispatcher(string name, IHand hand) : base(name, hand) { }

        public void DispatchDrive(Player player, ICity destinationCity)
        {
            
        }

        public void Dispatch(Player player, Player playerDestination)
        {
            
        }
    }
}
