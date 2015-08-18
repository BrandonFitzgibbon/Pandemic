using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class DrawManager
    {
        private Player player;
        private PlayerDeck playerDeck;

        private bool canDraw;
        public bool CanDraw
        {
            get { return canDraw; }
        }

        internal DrawManager(PlayerDeck playerDeck)
        {
            this.playerDeck = playerDeck;
            this.canDraw = false;
        }

        public void DrawPlayerCard()
        {
            if (player.DrawCounter.Count > 0)
            {
                Card drawnCard = playerDeck.Draw();

                if(drawnCard is EpidemicCard)
                {
                    if (EpidemicDrawn != null) EpidemicDrawn(this, EventArgs.Empty);
                    EpidemicCard ec = (EpidemicCard)drawnCard;
                    ec.Increase();
                    ec.Infect();
                    ec.Intensify();
                    ec.Discard();
                }

                else
                {
                    player.Hand.AddToHand(drawnCard);
                }

                player.DrawCounter.Draw();
            }
        }

        internal void SetPlayer(Player player)
        {
            this.player = player;
            canDraw = true;
            this.player.DrawCounter.DrawsDepleted += DrawsDepleted;
        }

        private void DrawsDepleted(object sender, EventArgs e)
        {
            canDraw = false;
        }

        public event EventHandler EpidemicDrawn;
    }
}
