using Engine.Implementations;
using Presentation.WPF.Contracts;
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

        private SolidColorBrush background;
        public SolidColorBrush Background
        {
            get { return background; }
            set { background = value; NotifyPropertyChanged(); }
        }

        private int i = 0;
        private DispatcherTimer timer;

        public AnchorViewModel(Node node, IBoardViewModel boardViewModel)
        {
            this.node = node;
            this.boardViewModel = boardViewModel;

            timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(100) };
            timer.Tick += Timer_Tick;
        }

        #region MouseEnter/MouseLeave Command

        private RelayCommand mouseEnterCommand;
        public ICommand MouseEnterCommand
        {
            get
            {
                if (mouseEnterCommand == null)
                    mouseEnterCommand = new RelayCommand(a => MouseEnter());
                return mouseEnterCommand;
            }
        }

        private void MouseEnter()
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

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (i % 2 == 0)
                BoardViewModel.PathAnimationViewModel.StrokeArray = new DoubleCollection() { 5 };
            else
                BoardViewModel.PathAnimationViewModel.StrokeArray = new DoubleCollection() { 3 };
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
                timer.Start();
            }
        }


        private RelayCommand mouseLeaveCommand;
        public ICommand MouseLeaveCommand
        {
            get
            {
                if (mouseLeaveCommand == null)
                    mouseLeaveCommand = new RelayCommand(a => MouseLeave());
                return mouseLeaveCommand;
            }
        }

        private void MouseLeave()
        {
            timer.Stop();
            foreach (ConnectionViewModel cvm in BoardViewModel.ConnectionViewModels)
            {
                cvm.Opacity = 1;
            }
            BoardViewModel.PathAnimationViewModel.Data = null;
        }

        #endregion
    }
}
