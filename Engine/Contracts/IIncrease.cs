using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IIncrease
    {
        void Increase();
        event EventHandler Increased;
    }
}
