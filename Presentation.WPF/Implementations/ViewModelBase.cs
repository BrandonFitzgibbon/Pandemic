using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
    }
}
