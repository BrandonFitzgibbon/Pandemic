using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Presentation.WPF.Controls
{
    public class ConnectedContent : ContentControl
    {
        public Canvas Canvas
        {
            get { return (Canvas)GetValue(CanvasProperty); }
            set { SetValue(CanvasProperty, value); }
        }

        public static readonly DependencyProperty CanvasProperty =
            DependencyProperty.Register("Canvas", typeof(Canvas), typeof(ConnectedContent), new PropertyMetadata(new PropertyChangedCallback(OnCanvasChanged)));

        public Anchor Target
        {
            get { return (Anchor)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target", typeof(Anchor), typeof(ConnectedContent), new PropertyMetadata(new PropertyChangedCallback(OnTargetChanged)));

        public Anchor Originator
        {
            get { return (Anchor)GetValue(OriginatorProperty); }
            set { SetValue(OriginatorProperty, value); }
        }

        public static readonly DependencyProperty OriginatorProperty =
            DependencyProperty.Register("Originator", typeof(Anchor), typeof(ConnectedContent), new PropertyMetadata(new PropertyChangedCallback(OnOriginatorChanged)));

        private bool hasCanvas;
        private bool hasTarget;
        private bool hasOriginator;

        public static void OnCanvasChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Canvas)
            {
                ConnectedContent cc = (ConnectedContent)o;
                cc.hasCanvas = true;

                if (cc.hasCanvas && cc.hasTarget && cc.hasOriginator)
                    cc.ConnectContent(cc.Canvas, cc.Target, cc.Originator);
            }
        }

        public static void OnTargetChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Anchor)
            {
                ConnectedContent cc = (ConnectedContent)o;
                cc.hasTarget = true;

                if (cc.hasCanvas && cc.hasTarget && cc.hasOriginator)
                    cc.ConnectContent(cc.Canvas, cc.Target, cc.Originator);
            }
        }

        public static void OnOriginatorChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Anchor)
            {
                ConnectedContent cc = (ConnectedContent)o;
                cc.hasOriginator = true;

                if (cc.hasCanvas && cc.hasTarget && cc.hasOriginator)
                    cc.ConnectContent(cc.Canvas, cc.Target, cc.Originator);
            }
        }

        public void ConnectContent(Canvas canvas, Anchor target, Anchor originator)
        {
            Line line = new Line();
            line.Stroke = System.Windows.Media.Brushes.White;
            line.StrokeThickness = 3;

            line.X1 = 0;
            line.Y1 = 0;

            Canvas.SetLeft(line, Canvas.GetLeft(originator));
            Canvas.SetTop(line, Canvas.GetTop(originator));

            line.X2 = Canvas.GetLeft(target) - Canvas.GetLeft(line);
            line.Y2 = Canvas.GetTop(target) - Canvas.GetTop(line);

            canvas.Children.Add(line);
        }
    }
}
