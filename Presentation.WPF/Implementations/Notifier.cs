using Presentation.WPF.Contracts;
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

        private void ChangeNotificationRequested(object sender, EventArgs e)
        {
            foreach (IViewModelBase viewModel in viewModels)
            {
                viewModel.NotifyChanges();
            }
        }
    }
}
