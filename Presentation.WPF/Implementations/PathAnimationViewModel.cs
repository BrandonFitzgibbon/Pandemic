using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Presentation.WPF.Implementations
{
    public class PathAnimationViewModel : ViewModelBase, IPathAnimationViewModel
    {
        private PathGeometry data;
        public PathGeometry Data
        {
            get { return data; }
            set { data = value; NotifyPropertyChanged(); }
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

        private DoubleCollection strokeArray;
        public DoubleCollection StrokeArray
        {
            get { return strokeArray; }
            set { strokeArray = value;  NotifyPropertyChanged(); }
        }

        public PathAnimationViewModel()
        {
            StrokeArray = new DoubleCollection() { 5 };
        }

    }
}
