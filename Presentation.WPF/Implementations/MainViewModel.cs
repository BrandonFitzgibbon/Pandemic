using Engine.Contracts;
using Engine.CustomEventArgs;
using Engine.Factories;
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
using System.Windows.Input;

namespace Presentation.WPF.Implementations
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        private Game game;
        private ActionCardManager actionCardManager;
        private IDataAccess data;

        private IContext<Player> currentPlayer;
        private IContext<Player> selectedPlayer;
        private IContext<ActionManager> actionManager;
        private IContext<DrawManager> drawManager;
        private IContext<InfectionManager> infectionManager;
        private IContext<StringBuilder> messageContext;
        private IContext<BaseActionCard> selectedActionCard;

        private bool actionCardViewOpen;
        public bool ActionCardViewOpen
        {
            get { return actionCardViewOpen; }
            set { actionCardViewOpen = value;  NotifyPropertyChanged(); }
        }

        private GameStatusViewModel gameStatusViewModel;
        public GameStatusViewModel GameStatusViewModel
        {
            get { return gameStatusViewModel; }
            set { gameStatusViewModel = value; NotifyPropertyChanged(); }
        }

        private IPlayersViewModel playersViewModel;
        public IPlayersViewModel PlayersViewModel
        {
            get { return playersViewModel; }
            set { playersViewModel = value; NotifyPropertyChanged(); }
        }

        private IActionsViewModel actionsViewModel;
        public IActionsViewModel ActionsViewModel
        {
            get { return actionsViewModel; }
            set { actionsViewModel = value; NotifyPropertyChanged(); }
        }

        private IBoardViewModel boardViewModel;
        public IBoardViewModel BoardViewModel
        {
            get { return boardViewModel; }
            set { boardViewModel = value; NotifyPropertyChanged(); }
        }

        private IHandViewModel handViewModel;
        public IHandViewModel HandViewModel
        {
            get { return handViewModel; }
            set { handViewModel = value; NotifyPropertyChanged(); }
        }

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

        private IMessageViewModel messageViewModel;
        public IMessageViewModel MessageViewModel
        {
            get { return messageViewModel; }
            set { messageViewModel = value; NotifyPropertyChanged(); }
        }

        private IEpidemicViewModel epidemicViewModel;
        public IEpidemicViewModel EpidemicViewModel
        {
            get { return epidemicViewModel; }
            set { epidemicViewModel = value; NotifyPropertyChanged(); }
        }

        private IDiscardViewModel discardViewModel;
        public IDiscardViewModel DiscardViewModel
        {
            get { return discardViewModel; }
            set { discardViewModel = value; NotifyPropertyChanged(); }
        }

        private IActionCardViewModel actionCardViewModel;
        public IActionCardViewModel ActionCardViewModel
        {
            get { return actionCardViewModel; }
            set { actionCardViewModel = value; NotifyPropertyChanged(); }
        }

        private IEnumerable<IPlayerViewModel> playerViewModels;
        private IEnumerable<IDiseaseCounterViewModel> diseaseCounterViewModels;
        private IEnumerable<INodeViewModel> nodeViewModels;

        public MainViewModel()
        {
            data = new DataAccess.Data();
            game = new Game(data, new List<string> { "Jessica", "Jack", "John", "Jane" }, Difficulty.Standard);

            actionCardManager = game.ActionCardManager;

            currentPlayer = new ObjectContext<Player>();
            currentPlayer.Context = game.CurrentPlayer;

            selectedPlayer = new ObjectContext<Player>();

            actionManager = new ObjectContext<ActionManager>();
            actionManager.Context = game.ActionManager;

            drawManager = new ObjectContext<DrawManager>();
            drawManager.Context = game.DrawManager;

            infectionManager = new ObjectContext<InfectionManager>();
            infectionManager.Context = game.InfectionManager;

            messageContext = new ObjectContext<StringBuilder>();
            messageContext.Context = new StringBuilder();

            selectedActionCard = new ObjectContext<BaseActionCard>();
            selectedActionCard.ContextChanged += SelectedActionCard_ContextChanged;

            playerViewModels = CreatePlayerViewModels(game.Players);
            diseaseCounterViewModels = CreateDiseaseCounterViewModels(game.DiseaseCounters);
            nodeViewModels = CreateNodeViewModels(game.Nodes, game.NodeCounters);           

            GameStatusViewModel = new GameStatusViewModel(game.OutbreakCounter, game.InfectionRateCounter, game.ResearchStationCounter, 
                diseaseCounterViewModels.Single(i => i.Disease.Type == DiseaseType.Yellow), 
                diseaseCounterViewModels.Single(i => i.Disease.Type == DiseaseType.Red), 
                diseaseCounterViewModels.Single(i => i.Disease.Type == DiseaseType.Blue), 
                diseaseCounterViewModels.Single(i => i.Disease.Type == DiseaseType.Black));

            PlayersViewModel = new PlayersViewModel(currentPlayer, selectedPlayer, playerViewModels);
            ActionsViewModel = new ActionsViewModel(actionManager, currentPlayer, game.Players);
            BoardViewModel = new BoardViewModel(nodeViewModels);
            HandViewModel = new HandViewModel(selectedPlayer, selectedActionCard);
            DrawViewModel = new DrawViewModel(drawManager);
            InfectionViewModel = new InfectionViewModel(infectionManager);
            MessageViewModel = new MessageViewModel(messageContext);

            GameStatusViewModel.ChangeNotificationRequested += ChangeNotificationRequested;
            PlayersViewModel.ChangeNotificationRequested += ChangeNotificationRequested;
            ActionsViewModel.ChangeNotificationRequested += ChangeNotificationRequested;
            BoardViewModel.ChangeNotificationRequested += ChangeNotificationRequested;
            HandViewModel.ChangeNotificationRequested += ChangeNotificationRequested;
            DrawViewModel.ChangeNotificationRequested += ChangeNotificationRequested;
            InfectionViewModel.ChangeNotificationRequested += ChangeNotificationRequested;
            MessageViewModel.ChangeNotificationRequested += ChangeNotificationRequested;

            foreach (NodeDiseaseCounter ndc in game.NodeCounters)
            {
                ndc.Infected += ndc_Infected;
                ndc.Treated += ndc_Treated;
                ndc.Outbreak += ndc_Outbreak;
                ndc.Prevented += ndc_Prevented;
            }

            foreach (Player player in game.Players)
            {
                player.Hand.DiscardManager.Block += DiscardManagerBlock;
                player.Hand.DiscardManager.Release += DiscardManagerRelease;
            }

            drawManager.Context.EpidemicDrawn += EpidemicDrawn;

            game.GameOver += Game_GameOver;
            game.GameWon += Game_GameWon;

            PlayersViewModel.SelectedPlayerViewModel = PlayersViewModel.PlayerViewModels.Single(i => i.Player == currentPlayer.Context);
        }

        private void Game_GameWon(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Game_GameOver(object sender, EventArgs e)
        {
            game = null;
            ChangeNotificationRequested(this, EventArgs.Empty);
        }

        private void SelectedActionCard_ContextChanged(object sender, ContextChangedEventArgs<BaseActionCard> e)
        {
            if (e.NewContext != null)
            {
                ActionCardViewModel = null;
                Type[] res = e.NewContext.GetType().GetGenericArguments();
                Type first = res.First();
                if (first != null)
                {
                    if (first == typeof(GovernmentGrantItem))
                        ActionCardViewModel = new GovernmentGrantViewModel(selectedActionCard, actionCardManager);
                    if (ActionCardViewModel != null) ActionCardViewModel.ChangeNotificationRequested += ChangeNotificationRequested;
                    ActionCardViewOpen = true;
                }
            }
            else
                ActionCardViewOpen = false;
        }

        private void DiscardManagerBlock(object sender, EventArgs e)
        {
            ChangeNotificationRequested(this, EventArgs.Empty);
            DiscardViewModel dvm = new DiscardViewModel((DiscardManager)sender);
            dvm.ChangeNotificationRequested += ChangeNotificationRequested;
            DiscardViewModel = dvm;
        }

        private void DiscardManagerRelease(object sender, EventArgs e)
        {
            ChangeNotificationRequested(this, EventArgs.Empty);
            DiscardViewModel = null;
        }

        private void EpidemicDrawn(object sender, EventArgs e)
        {
            EpidemicViewModel evm = new EpidemicViewModel(drawManager.Context.EpidemicManager, messageContext);
            evm.ChangeNotificationRequested += ChangeNotificationRequested;
            EpidemicViewModel = evm;
        }

        private void ndc_Prevented(object sender, PreventionEventArgs e)
        {
            messageContext.Context.AppendLine(e.Player.Role + " in " + e.Player.Location.City + " has prevented a " + e.Disease + " infection from occuring in " + e.Node.City);
        }

        private void ndc_Outbreak(object sender, OutbreakEventArgs e)
        {
            messageContext.Context.AppendLine("An outbreak has occurred in " + e.OriginCounter.Node.City + "!");

            foreach (NodeDiseaseCounter ndc in e.ChainCities)
            {
                messageContext.Context.AppendLine("\tA chain outbreak has occured in " + ndc.Node.City);
            }
            messageContext.Context.AppendLine("\t\tAffected Cities");
            foreach (NodeDiseaseCounter ndc in e.AffectedCities.Distinct())
            {
                messageContext.Context.AppendLine("\t\t\t" + ndc.Node.City);
            }
        }

        private void ndc_Treated(object sender, TreatedEventArgs e)
        {
            messageContext.Context.AppendLine(e.Treater.Role + " has treated " + e.Value + " unit(s) of " + e.NodeDiseaseCounter.Disease + " infection in " + e.NodeDiseaseCounter.Node.City);
        }

        private void ndc_Infected(object sender, InfectionEventArgs e)
        {
            messageContext.Context.AppendLine(e.Value + " unit(s) of " + e.NodeDiseaseCounter.Disease + " have infected " + e.NodeDiseaseCounter.Node.City);
        }

        private IEnumerable<IPlayerViewModel> CreatePlayerViewModels(IEnumerable<Player> players)
        {
            List<IPlayerViewModel> playerViewModels = new List<IPlayerViewModel>();
            foreach (Player player in players)
            {
                PlayerViewModel pvm = new PlayerViewModel(player);
                pvm.ChangeNotificationRequested += ChangeNotificationRequested;
                playerViewModels.Add(pvm);
            }
            return playerViewModels;
        }

        private IEnumerable<IDiseaseCounterViewModel> CreateDiseaseCounterViewModels(IEnumerable<DiseaseCounter> diseases)
        {
            List<IDiseaseCounterViewModel> diseaseCounterViewModels = new List<IDiseaseCounterViewModel>();
            foreach (DiseaseCounter disease in diseases)
            {
                DiseaseCounterViewModel dcvm = new DiseaseCounterViewModel(disease);
                dcvm.ChangeNotificationRequested += ChangeNotificationRequested;
                diseaseCounterViewModels.Add(dcvm);
            }
            return diseaseCounterViewModels;
        }

        private IEnumerable<INodeViewModel> CreateNodeViewModels(IEnumerable<Node> nodes, IEnumerable<NodeDiseaseCounter> nodeDiseaseCounters)
        {
            List<INodeViewModel> nodeViewModels = new List<INodeViewModel>();
            foreach (Node node in nodes)
            {
                List<INodeDiseaseCounterViewModel> nodeDiseaseCounterViewModels = new List<INodeDiseaseCounterViewModel>();
                foreach (NodeDiseaseCounter ndc in nodeDiseaseCounters.Where(i => i.Node == node))
                {
                    NodeDiseaseCounterViewModel ndcvm = new NodeDiseaseCounterViewModel(ndc);
                    ndcvm.ChangeNotificationRequested += ChangeNotificationRequested;
                    nodeDiseaseCounterViewModels.Add(ndcvm);
                }
                NodeViewModel nvm = new NodeViewModel(node, nodeDiseaseCounterViewModels);
                nvm.ChangeNotificationRequested += ChangeNotificationRequested;
                nodeViewModels.Add(nvm);
            }
            return nodeViewModels;
        }

        private new void ChangeNotificationRequested(object sender, EventArgs e)
        {
            NotifyChanges();

            GameStatusViewModel.NotifyChanges();
            PlayersViewModel.NotifyChanges();
            ActionsViewModel.NotifyChanges();
            BoardViewModel.NotifyChanges();
            HandViewModel.NotifyChanges();
            DrawViewModel.NotifyChanges();
            InfectionViewModel.NotifyChanges();
            MessageViewModel.NotifyChanges();

            foreach (IPlayerViewModel playerViewModel in playerViewModels)
            {
                playerViewModel.NotifyChanges();
            }

            foreach (IDiseaseCounterViewModel diseaseCounterViewModel in diseaseCounterViewModels)
            {
                diseaseCounterViewModel.NotifyChanges();
            }

            foreach (INodeViewModel nodeViewModel in nodeViewModels)
            {
                nodeViewModel.NotifyChanges();
                foreach (INodeDiseaseCounterViewModel ndcvm in nodeViewModel.NodeCounters)
                {
                    ndcvm.NotifyChanges();
                }
            }
        }

        private RelayCommand nextTurnCommand;
        public ICommand NextTurnCommand
        {
            get
            {
                if (nextTurnCommand == null)
                    nextTurnCommand = new RelayCommand(a => NextTurn(), p => CanNextTurn());
                return nextTurnCommand;
            }
        }

        public bool CanNextTurn()
        {
            return game != null && game.CanNextPlayer;
        }

        public void NextTurn()
        {
            game.NextPlayer();
            currentPlayer.Context = game.CurrentPlayer;
            PlayersViewModel.SelectedPlayerViewModel = PlayersViewModel.PlayerViewModels.Single(i => i.Player == currentPlayer.Context);
            ChangeNotificationRequested(this, EventArgs.Empty);
        }
    }
}
