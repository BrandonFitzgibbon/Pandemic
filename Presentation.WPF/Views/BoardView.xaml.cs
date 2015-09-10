using Engine.Implementations;
using Presentation.WPF.Contracts;
using Presentation.WPF.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentation.WPF.Views
{
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : UserControl
    {
        public BoardView()
        {
            InitializeComponent();

            pathLine = new Path();
            pathGeo = new PathGeometry();
            pathFigure = new PathFigure();
            pathLine.Data = pathGeo;
            pathGeo.Figures.Add(pathFigure);
            pathLine.Stroke = Brushes.White;
            pathLine.StrokeThickness = 3;
            pathLine.StrokeDashArray = new DoubleCollection() { 5 };

            Canvas.Children.Add(pathLine);
        }

        Point start;
        Point origin;
        Path pathLine;
        PathGeometry pathGeo;
        PathFigure pathFigure;

        private void Border_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Border bd = (Border)sender;
            Canvas canvas = (Canvas)bd.Child;
            var st = (ScaleTransform)((TransformGroup)canvas.RenderTransform).Children.First(i => i is ScaleTransform);
            var tt = (TranslateTransform)((TransformGroup)canvas.RenderTransform).Children.First(i => i is TranslateTransform);
            double zoom = e.Delta > 0 ? 0.05 : -0.05;

            if ((double)Math.Round(st.ScaleX + zoom, 2) < 0.3 || (double)Math.Round(st.ScaleY + zoom, 2) > 2.3)
                return;

            st.ScaleX = (double)Math.Round(st.ScaleX + zoom, 2);
            st.ScaleY = (double)Math.Round(st.ScaleY + zoom, 2);
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border bd = (Border)sender;
            bd.Focus();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border bd = (Border)sender;
            Canvas canvas = (Canvas)bd.Child;
            var tr = (TranslateTransform)((TransformGroup)canvas.RenderTransform).Children.First(i => i is TranslateTransform);
            start = e.GetPosition(bd);
            origin = new Point(tr.X, tr.Y);
            canvas.CaptureMouse();
        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
            Border bd = (Border)sender;
            Canvas canvas = (Canvas)bd.Child;
            if (canvas.IsMouseCaptured)
            {
                var tr = (TranslateTransform)((TransformGroup)canvas.RenderTransform).Children.First(i => i is TranslateTransform);
                var st = (ScaleTransform)((TransformGroup)canvas.RenderTransform).Children.First(i => i is ScaleTransform);
                Vector translate = start - e.GetPosition(bd);
                double x = origin.X - translate.X;
                double y = origin.Y - translate.Y;
                tr.X = x;
                tr.Y = y;
            }
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Border bd = (Border)sender;
            Canvas canvas = (Canvas)bd.Child;
            canvas.ReleaseMouseCapture();
        }
    }
}
