using Engine.Contracts;
using Engine.Implementations;
using Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Diseases : IDataAccess<IDisease>
    {
        public IList<IDisease> GetAll()
        {
            List<IDisease> diseases = new List<IDisease>()
            {
                new Disease(1) { Name = "Yellow" },
                new Disease(2) { Name = "Red" },
                new Disease(3) { Name = "Blue" },
                new Disease(4) { Name = "Black" }
            };

            foreach (IDisease disease in diseases)
            {
                CounterPool cp = new CounterPool(disease);
                disease.CreatePool(cp);
            }

            return diseases;
        }
    }
}
