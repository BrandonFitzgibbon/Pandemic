using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IInfectionRateCounter
    {
        int Count { get; }
        int InfectionRate { get; }
        void Increase();
    }
}
