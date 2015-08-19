using Presentation.WPF.Context;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Implementations
{
    public class MessageViewModel : ViewModelBase, IMessageViewModel
    {
        private IContext<StringBuilder> message;

        public string Message
        {
            get { return message != null && message.Context != null ? message.Context.ToString() : null; }
        }

        public MessageViewModel(IContext<StringBuilder> message)
        {
            this.message = message;
            this.message.ContextChanged += message_ContextChanged;
        }

        void message_ContextChanged(object sender, ContextChangedEventArgs<StringBuilder> e)
        {
            NotifyPropertyChanged("Message");
        }
    }
}
