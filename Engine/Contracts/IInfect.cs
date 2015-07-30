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
        void RaiseInfection(int rate);
        void RaiseTreatment(int rate);
        event EventHandler Infection;
        event EventHandler Treatment;
    }
}
