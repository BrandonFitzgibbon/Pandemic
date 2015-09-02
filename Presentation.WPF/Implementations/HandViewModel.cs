using Engine.Implementations;
using Presentation.WPF.Context;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.WPF.Implementations
{
    public class HandViewModel : ViewModelBase, IHandViewModel
    {
        private IContext<Player> selectedPlayer;
        private IContext<BaseActionCard> selectedActionCardContext;

        public Hand Hand
        {
            get { return selectedPlayer != null && selectedPlayer.Context != null && selectedPlayer.Context.Hand != null ? selectedPlayer.Context.Hand : null; }
        }

        public IEnumerable<CityCard> CityCards
        {
            get { return Hand != null ? Hand.CityCards : null; }
        }

        public IEnumerable<BaseActionCard> ActionCards
        {
            get { return Hand != null ? Hand.ActionCards : null; }
        }

        public string SelectedPlayerName
        {
            get { return selectedPlayer != null & selectedPlayer.Context != null ? selectedPlayer.Context.Name : null; }
        }

        private BaseActionCard selectedActionCard;
        public BaseActionCard SelectedActionCard
        {
            get { return selectedActionCard; }
            set { selectedActionCard = value; NotifyPropertyChanged(); selectedActionCardContext.Context = value; }
        }

        public HandViewModel(IContext<Player> selectedPlayer, IContext<BaseActionCard> selectedActionCardContext)
        {
            this.selectedPlayer = selectedPlayer;
            this.selectedPlayer.ContextChanged += selectedPlayer_ContextChanged;
            this.selectedActionCardContext = selectedActionCardContext;
            this.selectedActionCardContext.ContextChanged += SelectedActionCardContext_ContextChanged;
        }

        private void SelectedActionCardContext_ContextChanged(object sender, ContextChangedEventArgs<BaseActionCard> e)
        {
            if (e.NewContext == null || ActionCards.Contains(e.NewContext))
            {
                selectedActionCardContext.ContextChanged -= SelectedActionCardContext_ContextChanged;
                SelectedActionCard = e.NewContext;
                selectedActionCardContext.ContextChanged += SelectedActionCardContext_ContextChanged;
            }
        }

        private void selectedPlayer_ContextChanged(object sender, ContextChangedEventArgs<Player> e)
        {
            NotifyPropertyChanged("CityCards");
            NotifyPropertyChanged("ActionCards");
            NotifyPropertyChanged("SelectedPlayerName");
        }
    }
}
