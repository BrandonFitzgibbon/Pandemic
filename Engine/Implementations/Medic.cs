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
        public Medic(IGame game, string name, IHand hand) : base(game, name, hand) 
        {
            Moved += MedicMoved;
        }

        private void MedicMoved(object sender, PlayerMovedEventArgs e)
        {
            
        }

        public override void TreatDisease(IDisease disease)
        {
            INodeDiseaseCounter treatTarget = game.NodeCounters.Single(i => i.Node == Location); 
            if (treatTarget != null)
            {
                treatTarget.RaiseTreatment(treatTarget.Count);
            }
        }
    }
}
