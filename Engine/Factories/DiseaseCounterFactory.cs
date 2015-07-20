using Engine.Contracts;
using Engine.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    public class DiseaseCounterFactory : IDiseaseCounterFactory
    {
        private IList<IDisease> diseases;

        public DiseaseCounterFactory(IList<IDisease> diseases)
        {
            this.diseases = diseases;
        }

        public IList<IDiseaseCounter> GetCounters(ICity city)
        {
            List<IDiseaseCounter> counters = new List<IDiseaseCounter>();
            foreach (IDisease disease in diseases)
            {
                counters.Add(new Counter(disease, city));
            }
            return counters;
        }
    }
}
