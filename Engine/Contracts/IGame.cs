using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IGame
    {
        IList<IDisease> Diseases { get; }
        IEnumerable<ICity> Cities { get; }
        IList<IPlayer> Players { get; }
    }
}
