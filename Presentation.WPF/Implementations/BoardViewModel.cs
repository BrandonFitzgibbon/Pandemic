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

        public BoardViewModel(Game game, IContext<Player> currentPlayer, IContext<Player> selectedPlayer, Notifier notifier)
        {
            playersViewModel = new PlayersViewModel(currentPlayer, selectedPlayer, CreatePlayerViewModels(game, notifier), notifier);
            gameStatusViewModel = new GameStatusViewModel(game.OutbreakCounter, game.InfectionRateCounter, game.ResearchStationCounter, CreateDiseaseCounterViewModels(game, notifier), notifier);
            nodeViewModels = CreateNodeViewModels(game, currentPlayer, this, notifier);
            notifier.SubscribeToViewModel(this);
        }

        private static IEnumerable<INodeViewModel> CreateNodeViewModels(Game game, IContext<Player> currentPlayer, IBoardViewModel boardViewModel, Notifier notifier)
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
                NodeViewModel nvm = new NodeViewModel(node, game.ActionManager, currentPlayer, nodeDiseaseCounterViewModels, CreatePlayerViewModels(game, notifier), boardViewModel, notifier);
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
