﻿using Engine.Implementations;
using Presentation.WPF.Context;
using Presentation.WPF.Contracts;
using Presentation.WPF.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Presentation.WPF.Implementations
{
    public class PawnViewModel : ViewModelBase, IPawnViewModel
    {
        private IContext<Player> selectedPlayer;

        private IBoardViewModel boardViewModel;
        public IBoardViewModel BoardViewModel
        {
            get { return boardViewModel; }
        }

        private object content;
        public object Content
        {
            get { return content; }
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
            set { top = value; NotifyPropertyChanged(); }
        }

        public PawnViewModel(IBoardViewModel boardViewModel, IContext<Player> selectedPlayer)
        {
            this.boardViewModel = boardViewModel;
            this.selectedPlayer = selectedPlayer;
            this.selectedPlayer.ContextChanged += SelectedPlayer_ContextChanged;
        }

        private void SelectedPlayer_ContextChanged(object sender, ContextChangedEventArgs<Player> e)
        {
            Icons icons = new Icons();
            switch (selectedPlayer.Context.Role)
            {
                case "Dispatcher":
                    content = icons["pawnPurple"];
                    break;
                case "Medic":
                    content = icons["pawnOrange"];
                    break;
                case "Scientist":
                    content = icons["pawnWhite"];
                    break;
                case "Quarantine Specialist":
                    content = icons["pawnDarkGreen"];
                    break;
                case "Researcher":
                    content = icons["pawnBrown"];
                    break;
                case "Operations Expert":
                    content = icons["pawnGreen"];
                    break;
            }

            Point location = GetLocation();
            Left = location.X;
            Top = location.Y;

            NotifyPropertyChanged("Content");
        }

        private Point GetLocation()
        {
            IAnchorViewModel avm = boardViewModel.AnchorViewModels.SingleOrDefault(i => i.Node == selectedPlayer.Context.Location);
            if (avm != null)
                return new Point(avm.Left, avm.Top);
            else
                return new Point(0, 0);
        }

        private DispatcherTimer timer;

        public void AnimateDrive(PathGeometry data)
        {
            if (timer != null) timer.Stop();

            timer = new DispatcherTimer(DispatcherPriority.Send) { Interval = TimeSpan.FromTicks(10000) };

            List<LineSegment> segments = new List<LineSegment>();
            Dictionary<int, bool> teleport = new Dictionary<int, bool>();

            foreach (PathFigure figure in data.Figures)
            {
                segments.Add(new LineSegment(figure.StartPoint, false));
                teleport.Add(segments.Count - 1, true);
                foreach (LineSegment segment in figure.Segments)
                {
                    segments.Add(segment);
                    teleport.Add(segments.Count - 1, false);
                }
            }

            int index = 0;
            double x = segments[index].Point.X;
            double y = segments[index].Point.Y;
            double xOffset = x - Left;
            double yOffset = y - Top;
            double xFactor = 1;
            double yFactor = 1;

            if (Math.Abs(xOffset) > Math.Abs(yOffset))
            {
                yFactor = yOffset / xOffset;
            }
            else if(Math.Abs(xOffset) < Math.Abs(yOffset))
            {
                xFactor = xOffset / yOffset;
            }

            timer.Tick += (object sender, EventArgs e) =>
            {
                Point current = segments[index].Point;
                if (teleport[index] == true)
                {
                    Left = current.X;
                    Top = current.Y;
                }


                if((xOffset > 1 && yOffset > 1 && Left >= current.X && Top >= current.Y)
                || (xOffset > 1 && yOffset < 1 && Left >= current.X && Top <= current.Y)
                || (xOffset < 1 && yOffset > 1 && Left <= current.X && Top >= current.Y)
                || (xOffset < 1 && yOffset < 1 && Left <= current.X && Top <= current.Y))
                {
                    index++;
                    if (index == segments.Count)
                    {
                        timer.Stop();
                    }
                    else
                    {
                        x = segments[index].Point.X;
                        y = segments[index].Point.Y;
                        xOffset = x - Left;
                        yOffset = y - Top;
                        xFactor = 1;
                        yFactor = 1;

                        if (Math.Abs(xOffset) > Math.Abs(yOffset))
                        {
                            yFactor = yOffset / xOffset;
                        }
                        else if (Math.Abs(xOffset) < Math.Abs(yOffset))
                        {
                            xFactor = xOffset / yOffset;
                        }
                    }
                }


                if (Left < x)
                {
                    Left = Left + (20 * Math.Abs(xFactor));
                    if (x - Left < (x - Left) * 0.0001)
                        Left = x;
                }
                else if (Left > x)
                {
                    Left = Left - (20 * Math.Abs(xFactor));
                    if (Left - x < (Left - x) * 0.0001)
                        Left = x;
                }

                if (Top < y)
                {
                    Top = Top + (20 * Math.Abs(yFactor));
                    if (y - Top < (y - top) * 0.0001)
                        Top = y;
                }
                else if (Top > y)
                {
                    Top = Top - (20 * Math.Abs(yFactor));
                    if (Top - y < (Top - y) * 0.0001)
                        Top = y;
                }

            };

            timer.Start();
        }
    }
}
