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

        internal EpidemicCard(InfectionRateCounter infectionRateCounter, InfectionDeck infectionDeck)
        {
            this.infectionRateCounter = infectionRateCounter;
            this.infectionDeck = infectionDeck;
        }

        internal void Increase()
        {
            infectionRateCounter.Increase();
        }

        internal void Infect()
        {
            InfectionCard infectionCard = infectionDeck.DrawBottom();
            infectionCard.Infect(3);
            infectionCard.Discard();
        }

        internal void Intensify()
        {
            infectionDeck.Intensify();
        }
    }
}
