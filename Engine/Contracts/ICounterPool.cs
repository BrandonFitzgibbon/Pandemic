using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface ICounterPool
    {
        IDisease Disease { get; }
        int Count { get; }
        void Decrease();
        void Increase();
        event EventHandler GameOver;
    }
}
