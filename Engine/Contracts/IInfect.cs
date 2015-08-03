using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IInfect
    {
        void Infection(int rate);
        void Treatment(int rate);
        event EventHandler Infected;
        event EventHandler Treated;
    }
}
