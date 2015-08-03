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
    public class PlayerDeck : IPlayerDeck
    {
        private Stack<ICard> cardPile;
        private Stack<ICard> discardPile;

        public PlayerDeck(IList<ICityCard> cityCards)
        {
            cityCards.Shuffle();
            foreach (ICityCard card in cityCards)
            {
                card.Discarded += CardDiscarded;
            }
            cardPile = new Stack<ICard>(cityCards);
            discardPile = new Stack<ICard>();
        }

        public ICard Draw()
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

        public void AddEpidemics(int difficulty, Stack<IEpidemicCard> epidemicCards)
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
            List<IList<ICard>> epidemicPiles = new List<IList<ICard>>();
            foreach (int k in deckSize)
            {
                IList<ICard> miniPile = new List<ICard>();
                for (int v = 0; v < k; v++)
                {
                    miniPile.Add(cardPile.Pop());
                }
                IEpidemicCard card = epidemicCards.Pop();
                card.Discarded += CardDiscarded;
                miniPile.Add(card);
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
