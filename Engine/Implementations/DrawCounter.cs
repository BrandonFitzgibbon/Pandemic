using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class DrawCounter : IDrawCounter
    {
        private IPlayerDeck playerDeck;
        private IInfectionDeck infectionDeck;
        private IInfectionRateCounter infectionRateCounter;
        private IPlayer player;

        private int drawCount;
        public int DrawCount
        {
            get { return drawCount; }
            set
            {
                if (value > 0)
                {
                    drawCount = value;
                }
                else if (value == 0)
                {
                    drawCount = value;
                    if (DrawsDepleted != null) DrawsDepleted(this, EventArgs.Empty);
                }
            }
        }

        private int infectionCount;
        public int InfectionCount
        {
            get { return infectionCount; }
            set
            {
                if(value > 0)
                {
                    infectionCount = value;
                }
                else if (value == 0)
                {
                    infectionCount = 0;
                    if (InfectionsDepleted != null) InfectionsDepleted(this, EventArgs.Empty);
                }
            }
        }

        public DrawCounter(IPlayerDeck playerDeck, IInfectionDeck infectionDeck, IInfectionRateCounter infectionRateCounter, IPlayer player)
        {
            this.playerDeck = playerDeck;
            this.infectionDeck = infectionDeck;
            this.infectionRateCounter = infectionRateCounter;
            this.drawCount = 2;
            this.infectionCount = infectionRateCounter.InfectionRate;
            this.player = player;
        }

        public ICard DrawPlayerCard()
        {
            ICard card = playerDeck.Draw();
            DrawCount = DrawCount - 1;
            if (card is IEpidemicCard)
            {
                infectionRateCounter.Increase();
                IInfectionCard infectionCard = (IInfectionCard)infectionDeck.DrawBottom();
                infectionCard.Infect(3);
                infectionDeck.Intensify();
                card.Discard();
            }
            else
                player.Hand.AddToHand(card);
            return card;
        }

        public IInfectionCard DrawInfectionCard()
        {
            ICard card = infectionDeck.Draw();
            if (card is IInfectionCard)
            {
                IInfectionCard iCard = (IInfectionCard)card;
                iCard.Infect(1);
                InfectionCount = InfectionCount - 1;
                return iCard;
            }
            return null;
        }

        public event EventHandler DrawsDepleted;
        public event EventHandler InfectionsDepleted;
    }
}
