using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IDisease
    {
        int Id { get; }
        string Name { get; set; }
        ICounterPool Pool { get; }
        void CreatePool(ICounterPool pool);
    }
}
