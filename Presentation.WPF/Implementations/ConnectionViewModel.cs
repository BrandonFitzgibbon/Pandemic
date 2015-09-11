using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Presentation.WPF.Implementations
{
    public class ConnectionViewModel : ViewModelBase, IConnectionViewModel
    {
        private IAnchorViewModel originator;
        public IAnchorViewModel Originator
        {
            get { return originator; }
        }

        private IAnchorViewModel destination;
        public IAnchorViewModel Destination
        {
            get { return destination; }
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

        private double x1;
        public double X1
        {
            get { return x1; }
            set { x1 = value;  NotifyPropertyChanged(); }
        }

        private double x2;
        public double X2
        {
            get { return x2; }
            set { x2 = value; NotifyPropertyChanged(); }
        }

        private double y1;
        public double Y1
        {
            get { return y1; }
            set { y1 = value;  NotifyPropertyChanged(); }
        }

        private double y2;
        public double Y2
        {
            get { return y2; }
            set { y2 = value; NotifyPropertyChanged(); }
        }

        private SolidColorBrush stroke;
        public SolidColorBrush Stroke
        {
            get { return stroke; }
            set { stroke = value; NotifyPropertyChanged(); }
        }

        private double thickness;
        public double Thickness
        {
            get { return thickness; }
            set { thickness = value;  NotifyPropertyChanged(); }
        }

        private double opacity;
        public double Opacity
        {
            get { return opacity; }
            set { opacity = value;  NotifyPropertyChanged(); }
        }

        public ConnectionViewModel(IAnchorViewModel originator, IAnchorViewModel destination)
        {
            this.originator = originator;
            this.destination = destination;

            Left = originator.Left;
            Top = originator.Top;
            X1 = 0;
            Y1 = 0;

            double xDirection = 1;
            double yDirection = 1;

            if (originator.Node.City.Name == "San Francisco" && destination.Node.City.Name == "Tokyo" || originator.Node.City.Name == "Tokyo" && destination.Node.City.Name == "San Francisco")
            {
                xDirection = -0.1;
                yDirection = 0.1;
            }
            if (originator.Node.City.Name == "San Francisco" && destination.Node.City.Name == "Manila" || originator.Node.City.Name == "Manila" && destination.Node.City.Name == "San Francisco")
            {
                xDirection = -0.1;
                yDirection = 0.1;
            }
            if (originator.Node.City.Name == "Los Angeles" && destination.Node.City.Name == "Sydney" || originator.Node.City.Name == "Sydney" && destination.Node.City.Name == "Los Angeles")
            {
                xDirection = -0.1;
                yDirection = 0.1;
            }

            X2 = (destination.Left - originator.Left) * xDirection;
            Y2 = (destination.Top - originator.Top) * yDirection;
            Stroke = Brushes.White;
            Thickness = 3;
            Opacity = 1;
        }
    }
}
