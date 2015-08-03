using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class EpidemicCard : Card, IEpidemicCard
    {
        private IIncrease infectionRateCounter;
        private IInfectionDeck infectionDeck;

        public EpidemicCard(IIncrease infectionRateCounter, IInfectionDeck infectionDeck)
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
            IInfectionCard infectionCard = (IInfectionCard)infectionDeck.DrawBottom();
            infectionCard.Infect(3);
        }

        public void Intensify()
        {
            infectionDeck.Intensify();
        }
    }
}
