using Presentation.WPF.Contracts;
using Presentation.WPF.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Presentation.WPF.Implementations
{
    public abstract class ViewModelBase : IViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NotifyChanges()
        {
            foreach (PropertyInfo prop in this.GetType().GetProperties())
            {
                NotifyPropertyChanged(prop.Name);
            }
        }

        public event EventHandler<ChangeNotificationRequestedArgs> ChangeNotificationRequested;

        public void RaiseChangeNotificationRequested(ChangeNotificationRequestedArgs e)
        {
            if (ChangeNotificationRequested != null) ChangeNotificationRequested(this, e);
        }
    }
}
