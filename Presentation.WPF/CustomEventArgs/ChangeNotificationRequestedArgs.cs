using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.CustomEventArgs
{
    public class ChangeNotificationRequestedArgs : EventArgs
    {
        public Type Type { get; private set; }

        public ChangeNotificationRequestedArgs(Type type)
        {
            Type = type;
        }
    }
}
