using Engine.Contracts;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.WPF.Implementations
{
    public class DeckViewModel : ViewModelBase, IDeckViewModel
    {
        private bool drawPhase;

        private IPlayerDeck playerDeck;
        public IPlayerDeck PlayerDeck
        {
            get { return playerDeck; }
        }

        public DeckViewModel(IPlayerDeck playerDeck, IMainViewModel mainViewModel)
        {
            this.playerDeck = playerDeck;
            mainViewModel.DrawPhase += mainViewModel_DrawPhase;
            drawPhase = false;
        }

        void mainViewModel_DrawPhase(object sender, EventArgs e)
        {
            drawPhase = true;
        }

        private RelayCommand drawPlayerCardCommand;
        public ICommand DrawPlayerCardCommand
        {
            get 
            {
                if (drawPlayerCardCommand == null)
                    drawPlayerCardCommand = new RelayCommand(a => DrawPlayerCard(), a => CanDrawPlayerCard());
                return drawPlayerCardCommand;
            }
        }

        public void DrawPlayerCard()
        {
            if (CardDrawn != null) CardDrawn(this, EventArgs.Empty);
        }

        public bool CanDrawPlayerCard()
        {
            return drawPhase;
        }

        public event EventHandler CardDrawn;
    }
}
