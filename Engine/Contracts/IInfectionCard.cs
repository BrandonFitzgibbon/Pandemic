using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IInfectionCard : ICard
    {
        string Name { get; }
        IDisease Disease { get; }
        void Infect(int rate);
    }
}
