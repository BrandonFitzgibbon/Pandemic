using Engine.Contracts;
using Engine.Implementations;
using Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class Diseases
    {
        public static IList<IDisease> GetAll()
        {
            List<IDisease> diseases = new List<IDisease>();
            using (var context = new DataEntities())
            {
                foreach (DataAccess.Disease disease in context.Diseases)
                {
                    diseases.Add(new Engine.Implementations.Disease(disease.Id) { Name = disease.Name });
                }
            }

            foreach (IDisease disease in diseases)
            {
                CounterPool cp = new CounterPool(disease);
                disease.CreatePool(cp);
            }

            return diseases;
        }
    }
}
