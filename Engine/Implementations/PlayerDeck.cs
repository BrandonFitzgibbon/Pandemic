using Engine.Contracts;
using Engine.CustomEventArgs;
using Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class PlayerDeck
    {
        private Stack<Card> cardPile;
        private Stack<Card> discardPile;

        public PlayerDeck(IList<CityCard> cityCards)
        {
            cityCards.Shuffle();
            foreach (CityCard card in cityCards)
            {
                card.Discarded += CardDiscarded;
            }
            cardPile = new Stack<Card>(cityCards);
            discardPile = new Stack<Card>();
        }

        public Card Draw()
        {
            if (cardPile.Count > 0)
                return cardPile.Pop();
            else
                if (GameOver != null) GameOver(this, EventArgs.Empty);
                return null;
        }

        private void CardDiscarded(object sender, DiscardedEventArgs e)
        {
            discardPile.Push(e.Card);
        }

        public void AddEpidemics(int difficulty, Stack<EpidemicCard> epidemicCards)
        {
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
            List<List<Card>> epidemicPiles = new List<List<Card>>();
            foreach (int k in deckSize)
            {
                List<Card> miniPile = new List<Card>();
                for (int v = 0; v < k; v++)
                {
                    miniPile.Add(cardPile.Pop());
                }
                EpidemicCard card = epidemicCards.Pop();
                card.Discarded += CardDiscarded;
                miniPile.Add(card);
                miniPile.Shuffle();
                epidemicPiles.Add(miniPile);
            }

            IList<Card> setCardPile = new List<Card>();
            foreach (IList<Card> list in epidemicPiles)
            {
                foreach (Card card in list)
                {
                    setCardPile.Add(card);
                }
            }

            cardPile = new Stack<Card>(setCardPile);
        }

        public event EventHandler<EventArgs> GameOver;
    }
}
