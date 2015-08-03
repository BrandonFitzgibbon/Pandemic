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
        private bool IsSet;

        public PlayerDeck(IList<ICityCard> cityCards)
        {
            cityCards.Shuffle();
            foreach (ICityCard card in cityCards)
            {
                card.Discarded += CardDiscarded;
            }
            cardPile = new Stack<ICard>(cityCards);
            discardPile = new Stack<ICard>();
            IsSet = false;
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

        //Provides players with starting hand and sets up the deck with epidemics
        public void Setup(IList<IPlayer> players, int difficulty, Stack<IEpidemicCard> epidemicCards)
        {
            if (IsSet)
                return;

            int i;

            //determine initial hand size
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

            //issue initial cards
            while(i < 4)
            {
                foreach (IPlayer player in players)
                {
                    player.Hand.AddToHand(this.Draw());
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

            IsSet = true;
        }

        public event EventHandler<EventArgs> GameOver;
    }
}
