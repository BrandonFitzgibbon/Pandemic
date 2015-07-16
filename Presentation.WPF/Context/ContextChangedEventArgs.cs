using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Context
{
    public class ContextChangedEventArgs<T> : EventArgs
    {
        private T newContext;
        public T NewContext
        {
            get { return newContext; }
        }

        public ContextChangedEventArgs(T newContext)
        {
            this.newContext = newContext;
        }
    }
}
