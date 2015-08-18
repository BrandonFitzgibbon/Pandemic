﻿using System;
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

        public bool CanInfect
        {
            get { return Count > 0; }
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
