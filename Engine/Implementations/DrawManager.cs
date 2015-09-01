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

        internal bool gameOver;

        public bool CanDraw
        {
            get { return Count > 0 && EpidemicManager == null && !gameOver; }
        }

        private int count;
        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                if (value == 0)
                    if (DrawPhaseCompleted != null) DrawPhaseCompleted(this, EventArgs.Empty);
            }
        }

        internal DrawManager(PlayerDeck playerDeck)
        {
            this.playerDeck = playerDeck;
        }

        public void DrawPlayerCard()
        {
            if (Count > 0)
            {
                Card drawnCard = playerDeck.Draw();

                if(drawnCard is EpidemicCard)
                {
                    EpidemicManager = new EpidemicManager((EpidemicCard)drawnCard);
                    EpidemicManager.Resolved += EpidemicManagerResolved;
                    if (EpidemicDrawn != null) EpidemicDrawn(this, EventArgs.Empty);
                }

                else
                {
                    player.Hand.AddToHand(drawnCard);
                }

                Count = Count - 1;
            }
        }

        internal void ResetDrawManager(Player player)
        {
            this.player = player;
            count = 2;
        }

        private void EpidemicManagerResolved(object sender, EventArgs e)
        {
            EpidemicManager = null;
        }

        public event EventHandler EpidemicDrawn;
        public event EventHandler DrawPhaseCompleted;
    }
}
