using Engine.Implementations;
using Engine.Implementations.ActionItems;
using Presentation.WPF.Context;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Implementations
{
    public class NodeViewModel : ViewModelBase, INodeViewModel
    {
        private IContext<Player> currentPlayer;
        private ActionManager actionManager;

        private Node node;
        public Node Node
        {
            get { return node; }
        }

        private IBoardViewModel boardViewModel;
        public IBoardViewModel BoardViewModel
        {
            get { return boardViewModel; }
        }

        private IEnumerable<IPlayerViewModel> players;
        public IEnumerable<IPlayerViewModel> Players
        {
            get { return players; }
        }

        private IEnumerable<INodeDiseaseCounterViewModel> nodeCounters;
        public IEnumerable<INodeDiseaseCounterViewModel> NodeCounters
        {
            get { return nodeCounters; }
        }

        private ICommandsViewModel commandsViewModel;
        public ICommandsViewModel CommandsViewModel
        {
            get { return commandsViewModel; }
        }

        public string Name
        {
            get { return node.City.Name; }
        }

        public Disease Disease
        {
            get { return node.Disease; }
        }

        public bool ResearchStation
        {
            get { return node.ResearchStation; }
        }

        public NodeStatus Status
        {
            get
            {
                NodeStatus status = NodeStatus.White;

                if (nodeCounters == null)
                    return status;

                foreach (INodeDiseaseCounterViewModel nodeCounter in nodeCounters)
                {
                    switch(nodeCounter.Count)
                    {
                        case 1:
                            status = NodeStatus.Yellow;
                            break;
                        case 2:
                            status = NodeStatus.Orange;
                            break;
                        case 3:
                            status = NodeStatus.Red;
                            break;
                    }
                }

                return status;
            }
        }

        public INodeDiseaseCounterViewModel YellowCounter
        {
            get { return nodeCounters != null ? nodeCounters.Single(i => i.Disease.Type == DiseaseType.Yellow) : null; }
        }

        public INodeDiseaseCounterViewModel RedCounter
        {
            get { return nodeCounters != null ? nodeCounters.Single(i => i.Disease.Type == DiseaseType.Red) : null; }
        }

        public INodeDiseaseCounterViewModel BlueCounter
        {
            get { return nodeCounters != null ? nodeCounters.Single(i => i.Disease.Type == DiseaseType.Blue) : null; }
        }

        public INodeDiseaseCounterViewModel BlackCounter
        {
            get { return nodeCounters != null ? nodeCounters.Single(i => i.Disease.Type == DiseaseType.Black) : null; }
        }

        public IPlayerViewModel PlayerOneViewModel
        {
            get { return players != null && node.Players.SingleOrDefault(i => i.TurnOrder == 1) != null ? players.SingleOrDefault(i => i.Player.TurnOrder == 1) : null; }
        }

        public IPlayerViewModel PlayerTwoViewModel
        {
            get { return players != null && node.Players.SingleOrDefault(i => i.TurnOrder == 2) != null ? players.SingleOrDefault(i => i.Player.TurnOrder == 2) : null; }
        }

        public IPlayerViewModel PlayerThreeViewModel
        {
            get { return players != null && node.Players.SingleOrDefault(i => i.TurnOrder == 3) != null ? players.SingleOrDefault(i => i.Player.TurnOrder == 3) : null; }
        }

        public IPlayerViewModel PlayerFourViewModel
        {
            get { return players != null && node.Players.SingleOrDefault(i => i.TurnOrder == 4) != null ? players.SingleOrDefault(i => i.Player.TurnOrder == 4) : null; }
        }

        public IPlayerViewModel CurrentPlayerViewModel
        {
            get { return currentPlayer != null && currentPlayer.Context != null && players != null ? players.SingleOrDefault(i => i.Player == currentPlayer.Context && currentPlayer.Context.Location == Node) : null; }
        }

        public DriveDestinationItem DriveDestinationItem
        {
            get { return actionManager != null && actionManager.DriveDestinations != null ? actionManager.DriveDestinations.SingleOrDefault(i => i.Node == Node) : null; }
        }

        public bool CanDrive
        {
            get { return actionManager != null ? actionManager.CanDrive(DriveDestinationItem) : false; }
        }

        public DirectFlightItem DirectFlightItem
        {
            get { return actionManager != null && actionManager.DirectFlightDestinations != null ? actionManager.DirectFlightDestinations.SingleOrDefault(i => i.CityCard.Node == Node) : null; }
        }

        public bool CanDirectFlight
        {
            get { return actionManager != null ? actionManager.CanDirectFlight(DirectFlightItem) : false; }
        }

        public NodeViewModel(Node node, ActionManager actionManager, IContext<Player> currentPlayer, IEnumerable<INodeDiseaseCounterViewModel> nodeCounters, IEnumerable<IPlayerViewModel> players, IBoardViewModel boardViewModel, Notifier notifier)
        {
            this.node = node;
            this.currentPlayer = currentPlayer;
            this.actionManager = actionManager;
            this.nodeCounters = nodeCounters;
            this.players = players;
            this.boardViewModel = boardViewModel;
            commandsViewModel = new CommandsViewModel(this.actionManager, notifier);
            notifier.SubscribeToViewModel(this);
        }

        public enum NodeStatus {White, Yellow, Orange, Red}
    }
}
