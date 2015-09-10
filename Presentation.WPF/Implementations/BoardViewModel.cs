using Engine.Implementations;
using Presentation.WPF.Context;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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

        private IEnumerable<IAnchorViewModel> anchorViewModels;
        public IEnumerable<IAnchorViewModel> AnchorViewModels
        {
            get { return anchorViewModels; }
        }

        private IEnumerable<IConnectionViewModel> connectionViewModels;
        public IEnumerable<IConnectionViewModel> ConnectionViewModels
        {
            get { return connectionViewModels; }
        }

        private IPathAnimationViewModel pathAnimationViewModel;
        public IPathAnimationViewModel PathAnimationViewModel
        {
            get { return pathAnimationViewModel; }
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
            anchorViewModels = CreateAnchorViewModels(game.Nodes);
            connectionViewModels = CreateConnectionViewModels(game.Nodes, AnchorViewModels);
            pathAnimationViewModel = new PathAnimationViewModel();
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

        private IEnumerable<IAnchorViewModel> CreateAnchorViewModels(IEnumerable<Node> nodes)
        {
            List<AnchorViewModel> anchorViewModels = new List<AnchorViewModel>();
            foreach (Node node in nodes)
            {
                switch(node.City.Name)
                {
                    case "Atlanta":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 1400, Top = 1400, Background = Brushes.Blue });
                        break;
                    case "Montreal":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 1625, Top = 1175, Background = Brushes.Blue });
                        break;
                    case "Chicago":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 1325, Top = 1125, Background = Brushes.Blue });
                        break;
                    case "San Francisco":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 800, Top = 1200, Background = Brushes.Blue });
                        break;
                    case "New York":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 1950, Top = 1200, Background = Brushes.Blue });
                        break;
                    case "Washington":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 1800, Top = 1350, Background = Brushes.Blue });
                        break;
                    case "London":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 2625, Top = 1025, Background = Brushes.Blue });
                        break;
                    case "Madrid":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 2600, Top = 1300, Background = Brushes.Blue });
                        break;
                    case "Paris":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 2900, Top = 1175, Background = Brushes.Blue });
                        break;
                    case "Milan":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 3150, Top = 1125, Background = Brushes.Blue });
                        break;
                    case "St. Petersburg":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 3325, Top = 900, Background = Brushes.Blue });
                        break;
                    case "Essen":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 2975, Top = 975, Background = Brushes.Blue });
                        break;
                    case "Miami":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 1600, Top = 1575, Background = Brushes.Yellow });
                        break;
                    case "Los Angeles":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 800, Top = 1525, Background = Brushes.Yellow });
                        break;
                    case "Mexico City":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 1100, Top = 1600, Background = Brushes.Yellow });
                        break;
                    case "Lima":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 1450, Top = 2300, Background = Brushes.Yellow });
                        break;
                    case "Santiago":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 1525, Top = 2600, Background = Brushes.Yellow });
                        break;
                    case "Bogota":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 1600, Top = 1900, Background = Brushes.Yellow });
                        break;
                    case "Sao Paulo":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 2125, Top = 2275, Background = Brushes.Yellow });
                        break;
                    case "Buenos Aires":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 1900, Top = 2525, Background = Brushes.Yellow });
                        break;
                    case "Lagos":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 2800, Top = 1875, Background = Brushes.Yellow });
                        break;
                    case "Khartoum":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 3300, Top = 1825, Background = Brushes.Yellow });
                        break;
                    case "Kinshasa":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 3100, Top = 2100, Background = Brushes.Yellow });
                        break;
                    case "Johannesburg":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 3350, Top = 2375, Background = Brushes.Yellow });
                        break;
                    case "Algiers":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 2900, Top = 1450, Background = Brushes.Black });
                        break;
                    case "Cairo":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 3300, Top = 1550, Background = Brushes.Black });
                        break;
                    case "Moscow":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 3550, Top = 1100, Background = Brushes.Black });
                        break;
                    case "Istanbul":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 3300, Top = 1275, Background = Brushes.Black });
                        break;
                    case "Baghdad":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 3550, Top = 1400, Background = Brushes.Black });
                        break;
                    case "Tehran":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 3900, Top = 1250, Background = Brushes.Black });
                        break;
                    case "Karachi":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 4000, Top = 1550, Background = Brushes.Black });
                        break;
                    case "Delhi":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 4200, Top = 1400, Background = Brushes.Black });
                        break;
                    case "Riyadh":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 3625, Top = 1675, Background = Brushes.Black });
                        break;
                    case "Mumbai":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 4075, Top = 1750, Background = Brushes.Black });
                        break;
                    case "Chennai":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 4300, Top = 1875, Background = Brushes.Black });
                        break;
                    case "Kolkata":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 4525, Top = 1500, Background = Brushes.Black });
                        break;
                    case "Shanghai":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 4725, Top = 1275, Background = Brushes.Red });
                        break;
                    case "Beijing":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 4750, Top = 1000, Background = Brushes.Red });
                        break;
                    case "Seoul":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 5000, Top = 1050, Background = Brushes.Red });
                        break;
                    case "Tokyo":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 5250, Top = 1200, Background = Brushes.Red });
                        break;
                    case "Hong Kong":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 4775, Top = 1600, Background = Brushes.Red });
                        break;
                    case "Bangkok":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 4575, Top = 1750, Background = Brushes.Red });
                        break;
                    case "Taipei":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 5100, Top = 1600, Background = Brushes.Red });
                        break;
                    case "Osaka":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 5400, Top = 1475, Background = Brushes.Red });
                        break;
                    case "Ho Chi Minh City":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 4825, Top = 1925, Background = Brushes.Red });
                        break;
                    case "Manila":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 5175, Top = 1950, Background = Brushes.Red });
                        break;
                    case "Jakarta":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 4600, Top = 2125, Background = Brushes.Red });
                        break;
                    case "Sydney":
                        anchorViewModels.Add(new AnchorViewModel(node, this) { Left = 5350, Top = 2600, Background = Brushes.Red });
                        break;
                }
            }
            return anchorViewModels;
        }

        private static IEnumerable<IConnectionViewModel> CreateConnectionViewModels(IEnumerable<Node> nodes, IEnumerable<IAnchorViewModel> anchors)
        {
            List<ConnectionViewModel> connectionViewModels = new List<ConnectionViewModel>();
            foreach (Node node in nodes)
            {
                IAnchorViewModel avm = anchors.SingleOrDefault(i => i.Node == node);
                if (avm == null)
                    continue;
                foreach (Node conn in node.Connections)
                {
                    IAnchorViewModel cavm = anchors.SingleOrDefault(i => i.Node == conn);
                    if (cavm == null)
                        continue;
                    connectionViewModels.Add(new ConnectionViewModel(avm, cavm));   
                }
            }
            return connectionViewModels;
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
