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
    public class AirliftViewModel : ViewModelBase, IActionCardViewModel
    {
        private ActionCardManager actionCardManager;
        private IContext<BaseActionCard> selectedActionCardContext;
        private ActionCard<AirliftItem> actionCard;
        private AirliftItem ali;

        public string Name
        {
            get { return actionCard != null ? actionCard.Name : null; }
        }

        public string Description
        {
            get { return actionCard != null ? actionCard.Description : null; }
        }

        public IEnumerable<AirliftItem> Targets
        {
            get { return actionCardManager != null && actionCardManager.AirliftTargets != null ? actionCardManager.AirliftTargets.Where(i => i.Player == SelectedPlayer).OrderBy(j => j.Node.City.Name) : null; }
        }

        public IEnumerable<Player> Players
        {
            get { return actionCardManager != null && actionCardManager.AirliftTargets != null ? actionCardManager.AirliftTargets.Select(i => i.Player).Distinct() : null; }
        }

        private Player selectedPlayer;
        public Player SelectedPlayer
        {
            get { return selectedPlayer; }
            set { selectedPlayer = value; NotifyPropertyChanged(); NotifyPropertyChanged("Targets"); }
        }

        public AirliftViewModel(IContext<BaseActionCard> selectedActionCardContext, ActionCardManager actionCardManager)
        {
            this.selectedActionCardContext = selectedActionCardContext;
            this.actionCardManager = actionCardManager;
            actionCard = selectedActionCardContext.Context as ActionCard<AirliftItem>;
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

        private RelayCommand airliftCommand;
        public ICommand AirliftCommand
        {
            get
            {
                if (airliftCommand == null)
                    airliftCommand = new RelayCommand(a => Airlift((AirliftItem)a), a => CanAirlift((AirliftItem)a));
                return airliftCommand;
            }
        }

        private bool CanAirlift(AirliftItem ali)
        {
            return actionCard.CanAction(ali);
        }

        private void Airlift(AirliftItem ali)
        {
            actionCard.Action(ali);
            RaiseChangeNotificationRequested();
        }
    }
}
