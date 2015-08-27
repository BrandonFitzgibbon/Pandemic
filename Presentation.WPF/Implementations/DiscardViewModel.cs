using Engine.Implementations;
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
    public class DiscardViewModel : ViewModelBase, IDiscardViewModel
    {
        private DiscardManager discardManager;

        public ObservableCollection<Card> Cards
        {
            get { return discardManager != null && discardManager.Hand != null ? new ObservableCollection<Card>(discardManager.Hand.Cards) : null; }
        }

        public DiscardViewModel(DiscardManager discardManager)
        {
            this.discardManager = discardManager;
        }

        private RelayCommand discardCommand;
        public ICommand DiscardCommand
        {
            get
            {
                if (discardCommand == null)
                    discardCommand = new RelayCommand((card) => Discard((Card)card), (card) => CanDiscard((Card)card));
                return discardCommand;
            }
        }

        public bool CanDiscard(Card card)
        {
            return discardManager.CanDiscard(card);
        }

        public void Discard(Card card)
        {
            discardManager.Discard(card);
            NotifyPropertyChanged("Cards");
            RaiseChangeNotificationRequested();
        }
    }
}
