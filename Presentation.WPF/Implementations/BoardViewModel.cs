using Engine.Implementations;
using Presentation.WPF.Context;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Implementations
{
    public class BoardViewModel : ViewModelBase, IBoardViewModel
    {
        IContext<Player> currentPlayer;
        IContext<Player> selectedPlayer;

        internal ActionManager ActionManager { get; private set; }

        private IEnumerable<IPlayerViewModel> playerViewModels;

        private IDrawViewModel drawViewModel;
        public IDrawViewModel DrawViewModel
        {
            get { return drawViewModel; }
            set { drawViewModel = value; NotifyPropertyChanged(); }
        }

        private IInfectionViewModel infectionViewModel;
        public IInfectionViewModel InfectionViewModel
        {
            get { return infectionViewModel; }
            set { infectionViewModel = value; NotifyPropertyChanged(); }
        }

        private INextTurnViewModel nextTurnViewModel;
        public INextTurnViewModel NextTurnViewModel
        {
            get { return nextTurnViewModel; }
            set { nextTurnViewModel = value;  NotifyPropertyChanged(); }
        }

        private ICommandsViewModel commandsViewModel;
        public ICommandsViewModel CommandsViewModel
        {
            get { return commandsViewModel; }
        }

        private IPlayersViewModel playersViewModel;
        public IPlayersViewModel PlayersViewModel
        {
            get { return playersViewModel; }
            set { playersViewModel = value;  NotifyPropertyChanged(); }
        }

        private IGameStatusViewModel gameStatusViewModel;
        public IGameStatusViewModel GameStatusViewModel
        {
            get { return gameStatusViewModel; }
            set { gameStatusViewModel = value; NotifyPropertyChanged(); }
        }

        private IEnumerable<INodeViewModel> nodeViewModels;
        public IEnumerable<INodeViewModel> NodeViewModels
        {
            get { return nodeViewModels;  }
        }

        public IPlayerViewModel SelectedPlayerViewModel
        {
            get { return selectedPlayer != null && selectedPlayer.Context != null && playerViewModels != null ? playerViewModels.SingleOrDefault(i => i.Player == selectedPlayer.Context) : null; }
        }

        public IPlayerViewModel CurrentPlayerViewModel
        {
            get { return currentPlayer != null && currentPlayer.Context != null && playerViewModels != null ? playerViewModels.SingleOrDefault(i => i.Player == currentPlayer.Context) : null; }
        }

        public BoardViewModel(Game game, IContext<Player> currentPlayer, IContext<Player> selectedPlayer, Notifier notifier)
        {
            this.currentPlayer = currentPlayer;
            this.selectedPlayer = selectedPlayer;
            selectedPlayer.Context = currentPlayer.Context;

            this.selectedPlayer.ContextChanged += SelectedPlayer_ContextChanged;

            drawViewModel = new DrawViewModel(game.DrawManager, notifier);
            infectionViewModel = new InfectionViewModel(game.InfectionManager, notifier);
            playerViewModels = CreatePlayerViewModels(game, notifier);
            playersViewModel = new PlayersViewModel(currentPlayer, playerViewModels, notifier);
            nextTurnViewModel = new NextTurnViewModel(game, currentPlayer, notifier);
            nextTurnViewModel.TurnChanged += NextTurnViewModel_TurnChanged;
            gameStatusViewModel = new GameStatusViewModel(game.OutbreakCounter, game.InfectionRateCounter, game.ResearchStationCounter, CreateDiseaseCounterViewModels(game, notifier), notifier);
            commandsViewModel = new CommandsViewModel(game.ActionManager, selectedPlayer, notifier);
            nodeViewModels = CreateNodeViewModels(game, selectedPlayer, this, notifier);

            notifier.SubscribeToViewModel(this);
        }

        private void NextTurnViewModel_TurnChanged(object sender, EventArgs e)
        {
            selectedPlayer.Context = currentPlayer.Context;
        }

        private void SelectedPlayer_ContextChanged(object sender, ContextChangedEventArgs<Player> e)
        {
            NotifyChanges();
        }

        private static IEnumerable<INodeViewModel> CreateNodeViewModels(Game game, IContext<Player> selectedPlayer, IBoardViewModel boardViewModel, Notifier notifier)
        {
            List<INodeViewModel> nodeViewModels = new List<INodeViewModel>();
            foreach (Node node in game.Nodes)
            {
                List<INodeDiseaseCounterViewModel> nodeDiseaseCounterViewModels = new List<INodeDiseaseCounterViewModel>();
                foreach (NodeDiseaseCounter ndc in game.NodeCounters.Where(i => i.Node == node))
                {
                    NodeDiseaseCounterViewModel ndcvm = new NodeDiseaseCounterViewModel(ndc, notifier);
                    nodeDiseaseCounterViewModels.Add(ndcvm);
                }
                NodeViewModel nvm = new NodeViewModel(node, game.ActionManager, selectedPlayer, nodeDiseaseCounterViewModels, CreatePlayerViewModels(game, notifier), boardViewModel, notifier);
                nodeViewModels.Add(nvm);
            }
            return nodeViewModels;
        }

        private static IEnumerable<IDiseaseCounterViewModel> CreateDiseaseCounterViewModels(Game game, Notifier notifier)
        {
            List<IDiseaseCounterViewModel> diseaseCounterViewModels = new List<IDiseaseCounterViewModel>();
            foreach (DiseaseCounter disease in game.DiseaseCounters)
            {
                DiseaseCounterViewModel dcvm = new DiseaseCounterViewModel(disease, notifier);
                diseaseCounterViewModels.Add(dcvm);
            }
            return diseaseCounterViewModels;
        }

        private static IEnumerable<IPlayerViewModel> CreatePlayerViewModels(Game game, Notifier notifier)
        {
            List<IPlayerViewModel> playerViewModels = new List<IPlayerViewModel>();
            foreach (Player player in game.Players)
            {
                PlayerViewModel pvm = new PlayerViewModel(player, notifier);
                playerViewModels.Add(pvm);
            }
            return playerViewModels;
        }
    }
}
