using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IDisease
    {
        string Name { get; }
        int Count { get; }
        void Increase();
        void Decrease();
        event EventHandler GameOver;
    }
}
