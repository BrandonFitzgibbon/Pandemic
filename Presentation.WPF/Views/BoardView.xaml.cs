using Presentation.WPF.Contracts;
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
        }

        Point start;
        Point origin;

        private void Border_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Border bd = (Border)sender;
            Canvas canvas = (Canvas)bd.Child;
            var st = (ScaleTransform)((TransformGroup)canvas.RenderTransform).Children.First(i => i is ScaleTransform);
            double zoom = e.Delta > 0 ? 0.1 : -0.1;

            if (st.ScaleX + zoom < 0.6)
                st.ScaleX = 0.6;
            else if (st.ScaleX + zoom > 2.3)
                st.ScaleX = 2.3;
            else
                st.ScaleX = st.ScaleX + zoom;

            if (st.ScaleY + zoom < 0.6)
                st.ScaleY = 0.6;
            else if (st.ScaleY + zoom > 2.3)
                st.ScaleY = 2.3;
            else
                st.ScaleY = st.ScaleY + zoom;

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
                Vector translate = start - e.GetPosition(bd);
                tr.X = origin.X - translate.X;
                tr.Y = origin.Y - translate.Y;
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
