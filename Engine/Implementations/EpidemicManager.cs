using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class EpidemicManager
    {
        private EpidemicCard epidemicCard;

        public bool CanIncrease { get; private set; }
        public bool CanInfect { get; private set; }
        public bool CanIntensify { get; private set; }

        public EpidemicManager(EpidemicCard epidemicCard)
        {
            this.epidemicCard = epidemicCard;
            CanIncrease = true;
        }

        public void Increase()
        {
            if (CanIncrease) epidemicCard.Increase();
            CanIncrease = false;
            CanInfect = true;
        }

        public void Infect()
        {
            if (CanInfect) epidemicCard.Infect();
            CanInfect = false;
            CanIntensify = true;
        }

        public void Intensify()
        {
            if (CanIntensify) epidemicCard.Intensify();
            CanIntensify = false;
            if (Resolved != null) Resolved(this, EventArgs.Empty);
        }

        public event EventHandler Resolved;
    }
}
