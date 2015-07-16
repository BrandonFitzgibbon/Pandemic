using Engine.Contracts;
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
        private IContext<IPlayer> context;

        public ICollection<ICard> Cards
        {
            get { return context.Context != null ? context.Context.Hand.Cards.ToList() : null;}
        }

        public HandViewModel(IContext<IPlayer> context)
        {
            this.context = context;
            this.context.ContextChanged += context_ContextChanged;
        }

        void context_ContextChanged(object sender, ContextChangedEventArgs<IPlayer> e)
        {
            NotifyPropertyChanged("Cards"); 
        }
    }
}
