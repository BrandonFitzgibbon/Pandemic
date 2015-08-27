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

        public EpidemicManager EpidemicManager { get; private set; }
        public bool CanDraw { get; private set; }

        internal DrawManager(PlayerDeck playerDeck)
        {
            this.playerDeck = playerDeck;
            CanDraw = false;
        }

        public void DrawPlayerCard()
        {
            if (player.DrawCounter.Count > 0)
            {
                Card drawnCard = playerDeck.Draw();

                if(drawnCard is EpidemicCard)
                {
                    EpidemicManager = new EpidemicManager((EpidemicCard)drawnCard);
                    EpidemicManager.Resolved += EpidemicManagerResolved;
                    CanDraw = false;
                    if (EpidemicDrawn != null) EpidemicDrawn(this, EventArgs.Empty);
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
            CanDraw = true;
            this.player.DrawCounter.DrawsDepleted += DrawsDepleted;
        }

        private void EpidemicManagerResolved(object sender, EventArgs e)
        {
            CanDraw = true;
            EpidemicManager = null;
        }

        private void DrawsDepleted(object sender, EventArgs e)
        {
            CanDraw = false;
        }

        public event EventHandler EpidemicDrawn;
    }
}
