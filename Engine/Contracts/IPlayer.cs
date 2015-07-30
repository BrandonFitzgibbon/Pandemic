using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IPlayer
    {
        string Name { get; }
        INode Location { get; }
        IHand Hand { get; }
    }
}
