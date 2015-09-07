using Presentation.WPF.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Contracts
{
    public interface IViewModelBase : INotifyPropertyChanged
    {
        event EventHandler<ChangeNotificationRequestedArgs> ChangeNotificationRequested;
        void RaiseChangeNotificationRequested(ChangeNotificationRequestedArgs e);
        void NotifyChanges();
    }
}
