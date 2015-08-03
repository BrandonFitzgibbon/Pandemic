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
        public Medic(string name, IHand hand) : base(name, hand) 
        {
            Moved += MedicMoved;
        }

        private void MedicMoved(object sender, PlayerMovedEventArgs e)
        {
            
        }

        public void TreatDisease(IDisease disease)
        {

        }
    }
}
