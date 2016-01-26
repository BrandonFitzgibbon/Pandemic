using Engine.Implementations;
using Engine.Implementations.ActionItems;
using Presentation.WPF.Contracts;
using Presentation.WPF.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Presentation.WPF.Implementations
{
    public class AnchorViewModel : ViewModelBase, IAnchorViewModel
    {
        private Node node;
        public Node Node
        {
            get { return node; }
        }

        private IEnumerable<NodeDiseaseCounter> nodeDiseaseCounters;

        public DriveDestinationItem DriveDestinationItem
        {
            get { return BoardViewModel.CommandsViewModel.GetDriveDestinationItem(Node); }
        }

        public DispatchItem DispatchItem
        {
            get { return BoardViewModel.CommandsViewModel.GetDispatchItem(Node); }
        }

        public DirectFlightItem DirectFlightItem
        {
            get { return BoardViewModel.CommandsViewModel.GetDirectFlightItem(Node); }
        }

        private IBoardViewModel boardViewModel;
        public IBoardViewModel BoardViewModel
        {
            get { return boardViewModel; }
        }

        private double left;
        public double Left
        {
            get { return left; }
            set { left = value; NotifyPropertyChanged(); }
        }

        private double top;
        public double Top
        {
            get { return top; }
            set { top = value;  NotifyPropertyChanged(); }
        }

        private double opacity = 1;
        public double Opacity
        {
            get { return opacity; }
            set { opacity = value;  NotifyPropertyChanged(); }
        }

        private object actionContent;
        public object ActionContent
        {
            get { return actionContent; }
            set { actionContent = value; }
        }

        private object alertActionContent;
        public object AlertActionContent
        {
            get { return alertActionContent; }
            set { alertActionContent = value;  NotifyPropertyChanged(); }
        }

        private SolidColorBrush alertBackground;
        public SolidColorBrush AlertBackground
        {
            get { return alertBackground; }
            set { alertBackground = value; NotifyPropertyChanged(); }
        }

        private SolidColorBrush alertFill;
        public SolidColorBrush AlertFill
        {
            get { return alertFill; }
            set { alertFill = value; NotifyPropertyChanged(); }
        }

        private SolidColorBrush alertForegound;
        public SolidColorBrush AlertForeground
        {
            get { return alertForegound; }
            set { alertForegound = value;  NotifyPropertyChanged(); }
        }

        private SolidColorBrush background;
        public SolidColorBrush Background
        {
            get { return background; }
            set { background = value; NotifyPropertyChanged(); }
        }

        private SolidColorBrush fill;
        public SolidColorBrush Fill
        {
            get { return fill; }
            set { fill = value;  NotifyPropertyChanged(); }
        }

        private SolidColorBrush glowBrush = Brushes.White;
        public SolidColorBrush GlowBrush
        {
            get { return glowBrush; }
            set { glowBrush = value;  NotifyPropertyChanged(); }
        }

        private SolidColorBrush contentForeground;
        public SolidColorBrush ContentForeground
        {
            get { return contentForeground; }
            set { contentForeground = value; NotifyPropertyChanged(); }
        }

        private double nameplateFontSize = 16;
        public double NameplateFontSize
        {
            get { return nameplateFontSize; }
            set { nameplateFontSize = value;  NotifyPropertyChanged(); }
        }

        private int i = 0;
        private DispatcherTimer opacityTimer;
        private int opacityDireciton;

        public AnchorViewModel(Node node, IEnumerable<NodeDiseaseCounter> diseaseCounters, IBoardViewModel boardViewModel, Notifier notifier)
        {
            this.node = node;
            this.nodeDiseaseCounters = diseaseCounters;
            this.boardViewModel = boardViewModel;

            foreach (NodeDiseaseCounter ndc in diseaseCounters)
            {
                ndc.Infected += Ndc_Infected;
                ndc.Treated += Ndc_Treated;
            }

            notifier.SubscribeToViewModel(this);

            opacityTimer = new DispatcherTimer(DispatcherPriority.Render) { Interval = TimeSpan.FromMilliseconds(1) };
            opacityTimer.Tick += opacityTimer_Tick;

            GetAlertColors();
        }

        private void Ndc_Treated(object sender, Engine.CustomEventArgs.TreatedEventArgs e)
        {
            GetAlertColors();
        }

        private void Ndc_Infected(object sender, Engine.CustomEventArgs.InfectionEventArgs e)
        {
            GetAlertColors();
        }

        private void GetAlertColors()
        {
            int max = nodeDiseaseCounters.Max(i => i.Count);
            switch (max)
            {
                case 1:
                    AlertBackground = Brushes.Yellow;
                    AlertForeground = Brushes.Black;
                    AlertFill = Brushes.Black;
                    break;
                case 2:
                    AlertBackground = Brushes.Orange;
                    AlertForeground = Brushes.White;
                    AlertFill = Brushes.White;
                    break;
                case 3:
                    AlertBackground = Brushes.Red;
                    AlertForeground = Brushes.White;
                    AlertFill = Brushes.White;
                    break;
                default:
                    AlertBackground = Brushes.LimeGreen;
                    AlertForeground = Brushes.Black;
                    AlertFill = Brushes.Black;
                    break;
            }
        }

        #region TopSelectPlayerCommand

        private RelayCommand topSelectPlayerCommand;
        public ICommand TopSelectPlayerCommand
        {
            get
            {
                if (topSelectPlayerCommand == null)
                    topSelectPlayerCommand = new RelayCommand(a => TopSelectPlayer());
                return topSelectPlayerCommand;
            }
        }

        private void TopSelectPlayer()
        {
            BoardViewModel.SelectPlayer(1);
        }

        #endregion

        #region LeftSelectPlayerCommand

        private RelayCommand leftSelectPlayerCommand;
        public ICommand LeftSelectPlayerCommand
        {
            get
            {
                if (leftSelectPlayerCommand == null)
                    leftSelectPlayerCommand = new RelayCommand(a => LeftSelectPlayer());
                return leftSelectPlayerCommand;
            }
        }

        private void LeftSelectPlayer()
        {
            BoardViewModel.SelectPlayer(3);
        }

        #endregion

        #region BottomSelectPlayerCommand

        private RelayCommand bottomSelectPlayerCommand;
        public ICommand BottomSelectPlayerCommand
        {
            get
            {
                if (bottomSelectPlayerCommand == null)
                    bottomSelectPlayerCommand = new RelayCommand(a => BottomSelectPlayer());
                return bottomSelectPlayerCommand;
            }
        }

        private void BottomSelectPlayer()
        {
            BoardViewModel.SelectPlayer(2);
        }

        #endregion

        #region RightSelectPlayerCommand

        private RelayCommand rightSelectPlayerCommand;
        public ICommand RightSelectPlayerCommand
        {
            get
            {
                if (rightSelectPlayerCommand == null)
                    rightSelectPlayerCommand = new RelayCommand(a => RightSelectPlayer());
                return rightSelectPlayerCommand;
            }
        }

        private void RightSelectPlayer()
        {
            BoardViewModel.SelectPlayer(4);
        }

        #endregion

        #region MouseEnterDrive/MouseLeaveDrive Command

        private RelayCommand mouseEnterDriveCommand;
        public ICommand MouseEnterDriveCommand
        {
            get
            {
                if (mouseEnterDriveCommand == null)
                    mouseEnterDriveCommand = new RelayCommand(a => MouseEnterDrive());
                return mouseEnterDriveCommand;
            }
        }

        private void MouseEnterDrive()
        {
            List<Node> path = NodeGrapher.GetNodePath(BoardViewModel.SelectedPlayerViewModel.Location, Node).ToList();
            List<IConnectionViewModel> connectionPath = new List<IConnectionViewModel>();
            for (int i = 0; i < path.Count - 1; i++)
            {
                foreach (IConnectionViewModel cvm in BoardViewModel.ConnectionViewModels.Where(j => (j.Originator.Node == path[i] && j.Destination.Node == path[i + 1]) || (j.Destination.Node == path[i] && j.Originator.Node == path[i + 1])))
                {
                    cvm.Opacity = 0;

                    if (cvm.Originator.Node == path[i] && cvm.Destination.Node == path[i + 1])
                        connectionPath.Add(cvm);
                }
            }

            AnimatePath(connectionPath);
        }

        private void opacityTimer_Tick(object sender, EventArgs e)
        {
            if (i % 10 == 0)
                BoardViewModel.PathAnimationViewModel.StrokeArray = new DoubleCollection() { 5 };
            else if (i % 5 == 0)
                BoardViewModel.PathAnimationViewModel.StrokeArray = new DoubleCollection() { 3 };

            if (Opacity == 0)
                opacityDireciton = 1;
            else if (Opacity == 1)
                opacityDireciton = -1;

            if (opacityDireciton == -1)
                Opacity = Opacity - 0.05 < 0 ? 0 : Opacity - 0.05;
            else if (opacityDireciton == 1)
                Opacity = Opacity + 0.05 > 1 ? 1 : Opacity + 0.05;

            i++;
        }

        private void AnimatePath(List<IConnectionViewModel> Connections)
        {
            if (Connections.Count > 0)
            {
                BoardViewModel.PathAnimationViewModel.Left = 0;
                boardViewModel.PathAnimationViewModel.Top = 0;
                PathGeometry pg = new PathGeometry(new PathFigureCollection());
                PathFigure pf = new PathFigure(new Point(Connections[0].Originator.Left, Connections[0].Originator.Top), new PathSegmentCollection(), false);

                PathFigure pFlipped = new PathFigure();
                bool pathFlipped = false;
                int flippedIndex = 0;

                for (int i = 0; i < Connections.Count; i++)
                {
                    if ((Connections[i].Originator.Node.City.Name == "Los Angeles" && Connections[i].Destination.Node.City.Name == "Sydney") || (Connections[i].Originator.Node.City.Name == "Sydney" && Connections[i].Destination.Node.City.Name == "Los Angeles"))
                    {
                        pathFlipped = true;
                        flippedIndex = i;
                        LineSegment flipSegment = new LineSegment(new Point(Connections[i].Originator.Left + Connections[i].X2, Connections[i].Originator.Top + Connections[i].Y2), true);
                        pf.Segments.Add(flipSegment);
                    }

                    if (pathFlipped)
                        break;

                    if ((Connections[i].Originator.Node.City.Name == "San Francisco" && Connections[i].Destination.Node.City.Name == "Tokyo") || (Connections[i].Originator.Node.City.Name == "Tokyo" && Connections[i].Destination.Node.City.Name == "San Francisco"))
                    {
                        pathFlipped = true;
                        flippedIndex = i;
                        LineSegment flipSegment = new LineSegment(new Point(Connections[i].Originator.Left + Connections[i].X2, Connections[i].Originator.Top + Connections[i].Y2), true);
                        pf.Segments.Add(flipSegment);
                    }

                    if (pathFlipped)
                        break;

                    if ((Connections[i].Originator.Node.City.Name == "San Francisco" && Connections[i].Destination.Node.City.Name == "Manila") || (Connections[i].Originator.Node.City.Name == "Manila" && Connections[i].Destination.Node.City.Name == "San Francisco"))
                    {
                        pathFlipped = true;
                        flippedIndex = i;
                        LineSegment flipSegment = new LineSegment(new Point(Connections[i].Originator.Left + Connections[i].X2, Connections[i].Originator.Top + Connections[i].Y2), true);
                        pf.Segments.Add(flipSegment);
                    }

                    if (pathFlipped)
                        break;

                    LineSegment segment = new LineSegment(new Point(Connections[i].Destination.Left, Connections[i].Destination.Top), true);
                    pf.Segments.Add(segment);
                }

                if(pathFlipped)
                {
                    Point origin = new Point(Connections[flippedIndex].Destination.Left - Connections[flippedIndex].X2, Connections[flippedIndex].Destination.Top - Connections[flippedIndex].Y2);
                    pFlipped.StartPoint = origin;
                    pFlipped.Segments = new PathSegmentCollection();
                    pFlipped.IsClosed = false;

                    LineSegment initial = new LineSegment(new Point(Connections[flippedIndex].Destination.Left, Connections[flippedIndex].Destination.Top), true);
                    pFlipped.Segments.Add(initial);

                    for (int i = flippedIndex + 1; i < Connections.Count; i++)
                    {
                        LineSegment segment = new LineSegment(new Point(Connections[i].Destination.Left, Connections[i].Destination.Top), true);
                        pFlipped.Segments.Add(segment);
                    }
                }

                pg.Figures.Add(pf);
                if (pathFlipped) pg.Figures.Add(pFlipped);
                BoardViewModel.PathAnimationViewModel.Data = pg;
                opacityTimer.Start();
            }
        }

        private RelayCommand mouseLeaveDriveCommand;
        public ICommand MouseLeaveDriveCommand
        {
            get
            {
                if (mouseLeaveDriveCommand == null)
                    mouseLeaveDriveCommand = new RelayCommand(a => MouseLeaveDrive());
                return mouseLeaveDriveCommand;
            }
        }

        private void MouseLeaveDrive()
        {
            opacityTimer.Stop();
            foreach (ConnectionViewModel cvm in BoardViewModel.ConnectionViewModels)
            {
                cvm.Opacity = 1;
            }
            BoardViewModel.PathAnimationViewModel.Data = null;
            Opacity = 1;
        }

        #endregion

        #region MouseEnterDirectFlight/MouseLeaveDirectFlight Command

        private RelayCommand mouseEnterDirectFlightCommand;
        public ICommand MouseEnterDirectFlightCommand
        {
            get
            {
                if (mouseEnterDirectFlightCommand == null)
                    mouseEnterDirectFlightCommand = new RelayCommand(a => MouseEnterDirectFlight());
                return mouseEnterDirectFlightCommand;
            }
        }

        private void MouseEnterDirectFlight()
        {
            IAnchorViewModel sender = BoardViewModel.AnchorViewModels.SingleOrDefault(i => i.Node == BoardViewModel.SelectedPlayerViewModel.Location);
            if (sender != null)
                AnimateDispatchPath(sender);
        }

        private RelayCommand mouseLeaveDirectFlightCommand;
        public ICommand MouseLeaveDirectFlightCommand
        {
            get
            {
                if (mouseLeaveDirectFlightCommand == null)
                    mouseLeaveDirectFlightCommand = new RelayCommand(a => MouseLeaveDirectFlight());
                return mouseLeaveDirectFlightCommand;
            }
        }

        private void MouseLeaveDirectFlight()
        {
            MouseLeaveDrive();
        }

        #endregion

        #region MouseEnterDispatch/MouseLeaveDispatch Command

        private RelayCommand mouseEnterDispatchCommand;
        public ICommand MouseEnterDispatchCommand
        {
            get
            {
                if (mouseEnterDispatchCommand == null)
                    mouseEnterDispatchCommand = new RelayCommand(a => MouseEnterDispatch());
                return mouseEnterDispatchCommand;
            }
        }

        private void MouseEnterDispatch()
        {
            if (DispatchItem.Player.Location.Connections.Contains(Node) || Node.Players.Count() == 0)
            {
                MouseEnterDrive();
            }
            else
            {
                IAnchorViewModel sender = BoardViewModel.AnchorViewModels.SingleOrDefault(i => i.Node == DispatchItem.Player.Location);
                if (sender != null)
                    AnimateDispatchPath(sender);
            }
        }

        private void dispatchTimer_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AnimateDispatchPath(IAnchorViewModel sender)
        {
            PathGeometry pg = new PathGeometry();
            pg.Figures = new PathFigureCollection();
            pg.Figures.Add(new PathFigure() { StartPoint = new Point(sender.Left, sender.Top), Segments = new PathSegmentCollection(), IsClosed = false });
            pg.Figures.First().Segments.Add(new LineSegment(new Point(Left, Top), true));
            BoardViewModel.PathAnimationViewModel.Data = pg;
            opacityTimer.Start();
        }

        private RelayCommand mouseLeaveDispatchCommand;
        public ICommand MouseLeaveDispatchCommand
        {
            get
            {
                if (mouseLeaveDispatchCommand == null)
                    mouseLeaveDispatchCommand = new RelayCommand(a => MouseLeaveDispatch());
                return mouseLeaveDispatchCommand;
            }
        }

        private void MouseLeaveDispatch()
        {
            MouseLeaveDrive();
        }

        #endregion
    }
}
