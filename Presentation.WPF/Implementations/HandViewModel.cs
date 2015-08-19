using Engine.Implementations;
using Presentation.WPF.Context;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Implementations
{
    public class HandViewModel : ViewModelBase, IHandViewModel
    {
        IContext<Player> selectedPlayer;
           
        public IEnumerable<Card> Hand
        {
            get { return selectedPlayer != null && selectedPlayer.Context != null && selectedPlayer.Context.Hand != null ? new List<Card>(selectedPlayer.Context.Hand.Cards) : null; }
        }

        public HandViewModel(IContext<Player> selectedPlayer)
        {
            this.selectedPlayer = selectedPlayer;
            this.selectedPlayer.ContextChanged += selectedPlayer_ContextChanged;
        }

        private void selectedPlayer_ContextChanged(object sender, ContextChangedEventArgs<Player> e)
        {
            NotifyPropertyChanged("Hand");
        }
    }
}
