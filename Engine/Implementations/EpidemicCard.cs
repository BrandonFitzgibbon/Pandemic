using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class EpidemicCard : Card
    {
        private InfectionRateCounter infectionRateCounter;
        private InfectionDeck infectionDeck;

        public EpidemicCard(InfectionRateCounter infectionRateCounter, InfectionDeck infectionDeck)
        {
            this.infectionRateCounter = infectionRateCounter;
            this.infectionDeck = infectionDeck;
        }

        public void Increase()
        {
            infectionRateCounter.Increase();
        }

        public void Infect()
        {
            InfectionCard infectionCard = infectionDeck.DrawBottom();
            infectionCard.Infect(3);
        }

        public void Intensify()
        {
            infectionDeck.Intensify();
        }
    }
}
