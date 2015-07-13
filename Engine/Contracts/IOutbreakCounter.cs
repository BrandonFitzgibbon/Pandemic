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
        void Increase();
        event EventHandler GameOver;
    }
}
