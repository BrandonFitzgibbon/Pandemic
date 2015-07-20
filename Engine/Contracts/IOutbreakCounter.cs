using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IOutbreakCounter
    {
        int Count { get; }
        event EventHandler GameOver;
    }
}
