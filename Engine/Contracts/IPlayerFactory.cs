using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IPlayerFactory
    {
        IList<IPlayer> GetPlayers(IList<string> names);
    }
}
