using Presentation.WPF.Contracts;
using Presentation.WPF.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Implementations
{
    public class Notifier
    {
        private List<IViewModelBase> viewModels;

        public Notifier()
        {
            viewModels = new List<IViewModelBase>();
        }

        public void SubscribeToViewModel(IViewModelBase viewModel)
        {
            viewModel.ChangeNotificationRequested += ChangeNotificationRequested;
            viewModels.Add(viewModel);
        }

        private void ChangeNotificationRequested(object sender, ChangeNotificationRequestedArgs e)
        {
            foreach (IViewModelBase viewModel in viewModels)
            {
                if (e != null)
                {
                    if (e.Type.IsAssignableFrom(viewModel.GetType()))
                        viewModel.NotifyChanges();
                }
                else
                    viewModel.NotifyChanges();
            }
        }
    }
}
