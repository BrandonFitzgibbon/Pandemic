using Engine.Contracts;
using Engine.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Infrastructure
{
    public class Counters
    {
        public IList<ICounter> GetCounters(IList<IDisease> diseases)
        {
            List<ICounter> counters = new List<ICounter>();
            foreach (IDisease disease in diseases)
            {
                counters.Add(new Counter(disease));
            }
            return counters;
        }
    }
}
