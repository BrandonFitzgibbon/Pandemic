using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IMove
    {
        void Move(INode destinationNode);
        event EventHandler<PlayerMovedEventArgs> Moved;
    }
}
