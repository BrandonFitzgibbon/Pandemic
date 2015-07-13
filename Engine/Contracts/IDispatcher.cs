using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IDispatcher : IPlayer
    {
        void Dispatch(IPlayer player, IPlayer playerDestination);
        void DispatchDrive(IPlayer player, ICity destinationCity);
    }
}
