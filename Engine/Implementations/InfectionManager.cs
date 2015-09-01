using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class InfectionManager
    {
        private InfectionRateCounter infectionRateCounter;
        private InfectionDeck infectionDeck;
        internal bool gameOver;

        public bool CanInfect
        {
            get { return Count > 0 && !gameOver; }
        }

        private int count;
        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                if (value == 0)
                    if (InfectionPhaseCompleted != null) InfectionPhaseCompleted(this, EventArgs.Empty);
            }
        }

        internal InfectionManager(InfectionRateCounter infectionRateCounter, InfectionDeck infectionDeck)
        {
            this.infectionRateCounter = infectionRateCounter;
            this.infectionDeck = infectionDeck;
        }

        internal void ResetInfectionManager()
        {
            count = infectionRateCounter.InfectionRate;
        }

        internal void SkipInfectionPhase()
        {
            for(int i = Count; i > 0; i--)
            {
                Count--;
            }
        }

        public void Infect()
        {
            if(Count > 0)
            {
                InfectionCard ic = infectionDeck.Draw();
                ic.Infect(1);
                Count = Count - 1;
            }
        }

        public event EventHandler InfectionPhaseCompleted;
    }
}
