using Engine.Contracts;
using Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class PlayerDeck : IPlayerDeck
    {
        private Stack<ICard> cardPile;
        private Stack<ICard> discardPile;
        private bool IsSetup;

        public PlayerDeck(IList<ICity> cities)
        {
            IList<ICard> cityCards = CreateCityCards(cities);
            cityCards.Shuffle();
            cardPile = new Stack<ICard>(cityCards);
            discardPile = new Stack<ICard>();
            IsSetup = false;
        }

        public ICard Draw()
        {
            if (cardPile.Count > 0)
                return cardPile.Pop();
            else
                if (GameOver != null) GameOver(this, EventArgs.Empty);
                return null;
        }

        public void Discard(ICard discardedCard)
        {
            discardPile.Push(discardedCard);
        }

        private IList<ICard> CreateCityCards(IList<ICity> cities)
        {
            IList<ICard> cityCards = new List<ICard>();
            foreach (ICity city in cities)
            {
                cityCards.Add(new CityCard(city));
            }
            return cityCards;
        }

        public void Setup(IList<IPlayer> players, int difficulty)
        {
            int i;

            switch(players.Count)
            {
                case 4:
                    i = 2;
                    break;
                case 3:
                    i = 1;
                    break;
                case 2:
                    i = 0;
                    break;
                default:
                    i = 4;
                    break;
            }

            while(i < 4)
            {
                foreach (IPlayer player in players)
                {
                    player.Hand.Add(Draw());
                }
                i++;
            }

            //Calculate the size of epidemic piles
            int deckDivision = cardPile.Count / difficulty;
            int remainder = cardPile.Count - (deckDivision * difficulty);
            List<int> deckSize = new List<int>();
            
            for (int j = 0; j < difficulty; j++)
            {
                deckSize.Add(deckDivision);
            }
            
            int index = 0;
            for (int w = remainder; w > 0; w--)
            {
                deckSize[index]++;
                index++;
            }

            //Add cards to epidemic piles
            List<IList<ICard>> epidemicPiles = new List<IList<ICard>>();
            foreach (int k in deckSize)
            {
                IList<ICard> miniPile = new List<ICard>();
                for (int v = 0; v < k; v++)
                {
                    miniPile.Add(cardPile.Pop());
                }
                miniPile.Add(new EpidemicCard());
                miniPile.Shuffle();
                epidemicPiles.Add(miniPile);
            }

            IList<ICard> setCardPile = new List<ICard>();
            foreach (IList<ICard> list in epidemicPiles)
            {
                foreach (ICard card in list)
                {
                    setCardPile.Add(card);
                }
            }

            cardPile = new Stack<ICard>(setCardPile);
        }

        public event EventHandler<EventArgs> GameOver;
    }
}
