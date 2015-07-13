using Engine.Contracts;
using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class Medic : Player, IMedic
    {
        public Medic(string name) : base(name) 
        {
            Moved += MedicMoved;
        }

        private void MedicMoved(object sender, PlayerMovedEventArgs e)
        {
            foreach (IDiseaseCounter counter in e.ArrivedCity.Counters)
            {
                if (counter.Disease.IsCured)
                    TreatDisease(counter.Disease);
            }
        }

        public override void TreatDisease(IDisease disease)
        {
            IDiseaseCounter treatTarget = location.Counters.SingleOrDefault(i => i.Disease == disease);
            if (treatTarget != null)
            {
                do
                {
                    treatTarget.Decrease();
                } while (treatTarget.Count > 0);
            }
        }
    }
}
