using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Context
{
    public class ObjectContext<T> : IContext<T>
    {
        private T context;
        public T Context
        {
            get { return context; }
            set { context = value; if (ContextChanged != null) ContextChanged(this, new ContextChangedEventArgs<T>(value)); }
        }

        public event EventHandler<ContextChangedEventArgs<T>> ContextChanged;
    }
}
