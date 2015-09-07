using Engine.Implementations;
using Engine.Implementations.ActionItems;
using Presentation.WPF.Context;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.WPF.Implementations
{
    public class GovernmentGrantViewModel : ViewModelBase, IActionCardViewModel
    {
        private ActionCardManager actionCardManager;
        private IContext<BaseActionCard> selectedActionCardContext;
        private ActionCard<GovernmentGrantItem> actionCard;
        private GovernmentGrantItem ggi;

        public string Name
        {
            get { return actionCard != null ? actionCard.Name : null; }
        }

        public string Description
        {
            get { return actionCard != null ? actionCard.Description : null; }
        }

        public IEnumerable<GovernmentGrantItem> Targets
        {
            get { return actionCardManager != null && actionCardManager.GovernmentGrantTargets != null ? actionCardManager.GovernmentGrantTargets.Where(i => i.DeconstructionNode == SelectedNode) : null; }
        }

        public IEnumerable<Node> Nodes
        {
            get { return actionCardManager != null && actionCardManager.GovernmentGrantTargets != null ? actionCardManager.GovernmentGrantTargets.Where(i => i.DeconstructionNode != null).Select(j => j.DeconstructionNode).Distinct() : null;  }
        }

        private Node selectedNode;
        public Node SelectedNode
        {
            get { return selectedNode; }
            set { selectedNode = value;  NotifyPropertyChanged(); NotifyPropertyChanged("Targets"); }
        }

        public GovernmentGrantViewModel(IContext<BaseActionCard>selectedActionCardContext, ActionCardManager actionCardManager)
        {
            this.selectedActionCardContext = selectedActionCardContext;
            this.actionCardManager = actionCardManager;
            actionCard = selectedActionCardContext.Context as ActionCard<GovernmentGrantItem>;
            if (actionCard == null)
                this.selectedActionCardContext.Context = null;
        }

        private RelayCommand cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                    cancelCommand = new RelayCommand(a => Cancel());
                return cancelCommand;
            }
        }

        private void Cancel()
        {
            selectedActionCardContext.Context = null;
        }

        private RelayCommand governmentGrantCommand;
        public ICommand GovernmentGrantCommand
        {
            get
            {
                if (governmentGrantCommand == null)
                    governmentGrantCommand = new RelayCommand(a => GovernmentGrant((GovernmentGrantItem)a), a => CanGovernmentGrant((GovernmentGrantItem)a));
                return governmentGrantCommand;
            }
        }

        private bool CanGovernmentGrant(GovernmentGrantItem ggi)
        {
            return actionCard.CanAction(ggi);
        }

        private void GovernmentGrant(GovernmentGrantItem ggi)
        {
            actionCard.Action(ggi);
        }
    }
}
