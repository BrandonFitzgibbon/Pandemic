﻿using Engine.Implementations;
using Engine.Implementations.ActionItems;
using Presentation.WPF.Context;
using Presentation.WPF.Contracts;
using Presentation.WPF.CustomEventArgs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.WPF.Implementations
{
    public class ActionsViewModel : ViewModelBase, IActionsViewModel
    {
        private IContext<ActionManager> actionManager;
        private IContext<Player> currentPlayer;

        private Node selectedNode;
        public Node SelectedNode
        {
            get { return selectedNode; }
            set
            {
                selectedNode = value;
                ddi = DriveDestinations.SingleOrDefault(i => i.Node == value);
            }
        }

        public Player CurrentPlayer
        {
            get { return currentPlayer != null && currentPlayer.Context != null ? currentPlayer.Context : null; }
        }

        public int ActionsCount
        {
            get { return currentPlayer != null && currentPlayer.Context != null && currentPlayer.Context.ActionCounter != null ? currentPlayer.Context.ActionCounter.Count : 0; }
        }

        public ActionManager ActionManager
        {
            get { return actionManager != null ? actionManager.Context : null; }
        }

        public IEnumerable<DriveDestinationItem> DriveDestinations
        {
            get { return ActionManager != null ? ActionManager.DriveDestinations : null; }
        }

        public IEnumerable<DirectFlightItem> DirectFlightDestinations
        {
            get { return ActionManager != null ? ActionManager.DirectFlightDestinations : null; }
        }

        public IEnumerable<CharterFlightItem> CharterFlightDestinations
        {
            get { return ActionManager != null ? ActionManager.CharterFlightDestinations : null;}
        }

        public IEnumerable<ShuttleFlightItem> ShuttleFlightDestinations
        {
            get { return ActionManager != null ? ActionManager.ShuttleFlightDestinations : null; }
        }

        public IEnumerable<ResearchStationConstructionItem> ResearchStationTargets
        {
            get { return ActionManager != null ? ActionManager.ResearchStationTargets : null; }
        }

        public IEnumerable<TreatDiseaseItem> TreatmentTargets
        {
            get { return ActionManager != null ? ActionManager.TreatmentTargets : null; }
        }

        public IEnumerable<ShareKnowledgeItem> ShareTargets
        {
            get { return ActionManager != null ? ActionManager.ShareTargets : null; }
        }

        public IEnumerable<DiscoverCureItem> DiscoverTargets
        {
            get { return ActionManager != null ? ActionManager.DiscoverTargets : null; }
        }

        public IEnumerable<OperationsRelocationItem> RelocationDestinations
        {
            get { return ActionManager != null ? ActionManager.RelocationDestinations : null; }
        }

        private Player selectedDispatchPlayer;
        public Player SelectedDispatchPlayer
        {
            get { return selectedDispatchPlayer; }
            set { selectedDispatchPlayer = value; NotifyPropertyChanged(); NotifyPropertyChanged("DispatchDestinations"); }
        }

        private IEnumerable<Player> players;
        public IEnumerable<Player> Players
        {
            get { return players; }
            set { players = value; NotifyPropertyChanged(); }
        }

        public IEnumerable<DispatchItem> DispatchDestinations
        {
            get { return ActionManager != null && SelectedDispatchPlayer != null && ActionManager.DispatchDestinations != null ? ActionManager.DispatchDestinations.Where(i => i.Player == SelectedDispatchPlayer) : null; }
        }

        private DiscoverCureItem selectedCureTarget;
        public DiscoverCureItem SelectedCureTarget
        {
            get { return selectedCureTarget; }
            set { selectedCureTarget = value; NotifyPropertyChanged(); NotifyPropertyChanged("CureCards"); }
        }

        public IEnumerable<CityCard> CureCards
        {
            get { return currentPlayer != null && currentPlayer.Context != null && currentPlayer.Context.Hand != null && currentPlayer.Context.Hand.CityCards != null && SelectedCureTarget != null ? currentPlayer.Context.Hand.CityCards.Where(i => i.Node.Disease == SelectedCureTarget.Disease) : null; }
        }

        private OperationsRelocationItem selectedRelocationTarget;
        public OperationsRelocationItem SelectedRelocationTarget
        {
            get { return selectedRelocationTarget; }
            set { selectedRelocationTarget = value;  NotifyPropertyChanged(); NotifyPropertyChanged("RelocationCards"); }
        }

        public IEnumerable<CityCard> RelocationCards
        {
            get { return currentPlayer != null && currentPlayer.Context != null && currentPlayer.Context.Hand != null && currentPlayer.Context.Hand.CityCards != null  ? currentPlayer.Context.Hand.CityCards : null; }
        }

        public ActionsViewModel(IContext<ActionManager> actionManager, IContext<Player> currentPlayer, IEnumerable<Player> players)
        {
            this.currentPlayer = currentPlayer;
            this.players = players;
            this.actionManager = actionManager;
            this.actionManager.ContextChanged += ContextChanged;
        }

        private void ContextChanged(object sender, ContextChangedEventArgs<ActionManager> e)
        {
            NotifyPropertyChanged("ActionManager");
        }

        private DriveDestinationItem ddi;

        private RelayCommand driveCommand;
        public ICommand DriveCommand
        {
            get 
            {
                if (driveCommand == null)
                    driveCommand = new RelayCommand(ddi => Drive(), ddi => CanDrive());
                return driveCommand;
            }
        }

        private bool CanDrive()
        {
            return ActionManager.CanDrive(ddi);
        }

        private async void Drive()
        {
            await Task.Run(() => ActionManager.Drive(ddi));
            ddi = null;
            RaiseChangeNotificationRequested(new ChangeNotificationRequestedArgs(typeof(BoardViewModel)));
        }

        private RelayCommand directFlightCommand;
        public ICommand DirectFlightCommand
        {
            get
            {
                if (directFlightCommand == null)
                    directFlightCommand = new RelayCommand(dfi => DirectFlight((DirectFlightItem)dfi), dfi => CanDirectFlight((DirectFlightItem)dfi));
                return directFlightCommand;
            }
        }

        private bool CanDirectFlight(DirectFlightItem dfi)
        {
            return ActionManager.CanDirectFlight(dfi);
        }

        public void DirectFlight(DirectFlightItem dfi)
        {
            ActionManager.DirectFlight(dfi);
        }

        private RelayCommand charterFlightCommand;
        public ICommand CharterFlightCommand
        {
            get
            {
                if (charterFlightCommand == null)
                    charterFlightCommand = new RelayCommand(cfi => CharterFlight((CharterFlightItem)cfi), cfi => CanCharterFlight((CharterFlightItem)cfi));
                return charterFlightCommand;
            }
        }

        public bool CanCharterFlight(CharterFlightItem cfi)
        {
            return ActionManager.CanCharterFlight(cfi);
        }

        public void CharterFlight(CharterFlightItem cfi)
        {
            ActionManager.CharterFlight(cfi);
        }

        private RelayCommand shuttleFlightCommand;
        public ICommand ShuttleFlightCommand
        {
            get
            {
                if (shuttleFlightCommand == null)
                    shuttleFlightCommand = new RelayCommand(sfi => ShuttleFlight((ShuttleFlightItem)sfi), sfi => CanShuttleFlight((ShuttleFlightItem)sfi));
                return shuttleFlightCommand;
            }
        }

        public bool CanShuttleFlight(ShuttleFlightItem sfi)
        {
            return ActionManager.CanShuttleFlight(sfi);
        }

        public void ShuttleFlight(ShuttleFlightItem sfi)
        {
            ActionManager.ShuttleFlight(sfi);
        }

        private RelayCommand buildResearchStationCommand;
        public ICommand BuildResearchStationCommand
        {
            get 
            {
                if(buildResearchStationCommand == null)
                    buildResearchStationCommand = new RelayCommand(rci => BuildResearchStation((ResearchStationConstructionItem)rci), rci => CanBuildResearchStation((ResearchStationConstructionItem)rci));
                return buildResearchStationCommand;
            }
        }

        public bool CanBuildResearchStation(ResearchStationConstructionItem rci)
        {
            return ActionManager.CanBuildResearchStation(rci);
        }

        public void BuildResearchStation(ResearchStationConstructionItem rci)
        {
            ActionManager.BuildResearchStation(rci);
        }

        private RelayCommand treatCommand;
        public ICommand TreatCommand
        {
            get 
            {
                if (treatCommand == null)
                    treatCommand = new RelayCommand(tdi => Treat((TreatDiseaseItem)tdi), tdi => CanTreat((TreatDiseaseItem)tdi));
                return treatCommand;
            }
        }

        private bool CanTreat(TreatDiseaseItem tdi)
        {
           return ActionManager.CanTreatDisease(tdi);
        }

        private void Treat(TreatDiseaseItem tdi)
        {
            ActionManager.TreatDisease(tdi);
        }

        private RelayCommand shareKnowledgeCommand;
        public ICommand ShareKnowledgeCommand
        {
            get
            {
                if (shareKnowledgeCommand == null)
                    shareKnowledgeCommand = new RelayCommand(ski => ShareKnowledge((ShareKnowledgeItem)ski), ski => CanShareKnowledge((ShareKnowledgeItem)ski));
                return shareKnowledgeCommand;
            }
        }

        private bool CanShareKnowledge(ShareKnowledgeItem ski)
        {
            return ActionManager.CanShareKnowledge(ski);
        }

        private void ShareKnowledge(ShareKnowledgeItem ski)
        {
            ActionManager.ShareKnowledge(ski);
        }

        private RelayCommand discoverCureCommand;
        public ICommand DiscoverCureCommand
        {
            get 
            {
                if (discoverCureCommand == null)
                    discoverCureCommand = new RelayCommand(cards => DiscoverCure((dynamic)cards), cards => CanDiscoverCure((dynamic)cards));
                return discoverCureCommand;
            }
        }

        private bool CanDiscoverCure(dynamic selectedCards)
        {
            if (SelectedCureTarget != null)
            {
                SelectedCureTarget.Cards.Clear();
                foreach (CityCard cityCard in selectedCards)
                {
                    SelectedCureTarget.Cards.Add(cityCard);
                }
            }

            return ActionManager.CanDiscoverCure(SelectedCureTarget);
        }

        private void DiscoverCure(dynamic selectedCards)
        {
            ActionManager.DiscoverCure(SelectedCureTarget);
        }

        private RelayCommand dispatchCommand;
        public ICommand DispatchCommand
        {
            get
            {
                if (dispatchCommand == null)
                    dispatchCommand = new RelayCommand(dpi => Dispatch((DispatchItem)dpi), dpi => CanDispatch((DispatchItem)dpi));
                return dispatchCommand;
            }
        }

        private bool CanDispatch(DispatchItem dpi)
        {
            return ActionManager.CanDispatch(dpi);
        }

        private void Dispatch(DispatchItem dpi)
        {
            ActionManager.Dispatch(dpi);
        }

        private RelayCommand relocateCommand;
        public ICommand RelocateCommand
        {
            get
            {
                if (relocateCommand == null)
                    relocateCommand = new RelayCommand(cc => Relocate(), cc => CanRelocate((CityCard)cc));
                return relocateCommand;
            }
        }

        private bool CanRelocate(CityCard cityCard)
        {
            if (SelectedRelocationTarget != null)
            {
                SelectedRelocationTarget.DiscardOption = cityCard;
            }

            return ActionManager.CanRelocate(SelectedRelocationTarget);
        }

        private void Relocate()
        {
            ActionManager.Relocate(SelectedRelocationTarget);
        }
    }
}
