using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Context
{
    public interface IContext<T>
    {
        T Context { get; set; }
        event EventHandler<ContextChangedEventArgs<T>> ContextChanged;
    }
}
