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
        public enum ConnectionType { TopLeftCorner, Top, TopRightCorner, Right, BottomRightCorner, Bottom, BottomLeftCorner, Left }

        public ConnectedContent BottomRightCornerConnection
        {
            get { return (ConnectedContent)GetValue(BottomRightCornerConnectionProperty); }
            set { SetValue(BottomRightCornerConnectionProperty, value); }
        }

        public static readonly DependencyProperty BottomRightCornerConnectionProperty =
            DependencyProperty.Register("BottomRightCornerConnection", typeof(ConnectedContent), typeof(ConnectedContent), new PropertyMetadata(null, new PropertyChangedCallback(OnBottomRightCornerConnectionChanged)));

        private static void OnBottomRightCornerConnectionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ConnectedContent originator = (ConnectedContent)o;
            ConnectedContent target = (ConnectedContent)e.NewValue;
            Canvas canvas = (Canvas)originator.Parent;
            originator.ConnectContent(canvas, originator, target, ConnectionType.BottomRightCorner, originator.BottomRightCornerType);
        }

        private ConnectionType bottomRightCornerType;
        public ConnectionType BottomRightCornerType
        {
            get { return bottomRightCornerType; }
            set { bottomRightCornerType = value; }
        }

        public ConnectedContent BottomLeftCornerConnection
        {
            get { return (ConnectedContent)GetValue(BottomLeftCornerConnectionProperty); }
            set { SetValue(BottomLeftCornerConnectionProperty, value); }
        }

        public static readonly DependencyProperty BottomLeftCornerConnectionProperty =
            DependencyProperty.Register("BottomLeftCornerConnection", typeof(ConnectedContent), typeof(ConnectedContent), new PropertyMetadata(null, new PropertyChangedCallback(OnBottomLeftCornerConnectionChanged)));

        private static void OnBottomLeftCornerConnectionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ConnectedContent originator = (ConnectedContent)o;
            ConnectedContent target = (ConnectedContent)e.NewValue;
            Canvas canvas = (Canvas)originator.Parent;
            originator.ConnectContent(canvas, originator, target, ConnectionType.BottomLeftCorner, originator.BottomLeftCornerType);
        }

        private ConnectionType bottomLeftCornerType;
        public ConnectionType BottomLeftCornerType
        {
            get { return bottomLeftCornerType; }
            set { bottomLeftCornerType = value; }
        }

        public ConnectedContent TopRightCornerConnection
        {
            get { return (ConnectedContent)GetValue(TopRightCornerConnectionProperty); }
            set { SetValue(TopRightCornerConnectionProperty, value); }
        }

        public static readonly DependencyProperty TopRightCornerConnectionProperty =
            DependencyProperty.Register("TopRightCornerConnection", typeof(ConnectedContent), typeof(ConnectedContent), new PropertyMetadata(null, new PropertyChangedCallback(OnTopRightCornerConnectionChanged)));

        private static void OnTopRightCornerConnectionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ConnectedContent originator = (ConnectedContent)o;
            ConnectedContent target = (ConnectedContent)e.NewValue;
            Canvas canvas = (Canvas)originator.Parent;
            originator.ConnectContent(canvas, originator, target, ConnectionType.TopRightCorner, originator.TopRightCornerType);
        }

        private ConnectionType topRightCornerType;
        public ConnectionType TopRightCornerType
        {
            get { return topRightCornerType; }
            set { topRightCornerType = value; }
        }

        public ConnectedContent TopLeftCornerConnection
        {
            get { return (ConnectedContent)GetValue(TopLeftCornerConnectionProperty); }
            set { SetValue(TopLeftCornerConnectionProperty, value); }
        }

        public static readonly DependencyProperty TopLeftCornerConnectionProperty =
            DependencyProperty.Register("TopLeftCornerConnection", typeof(ConnectedContent), typeof(ConnectedContent), new PropertyMetadata(null, new PropertyChangedCallback(OnTopLeftCornerConnectionChanged)));

        private static void OnTopLeftCornerConnectionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ConnectedContent originator = (ConnectedContent)o;
            ConnectedContent target = (ConnectedContent)e.NewValue;
            Canvas canvas = (Canvas)originator.Parent;
            originator.ConnectContent(canvas, originator, target, ConnectionType.TopLeftCorner, originator.TopLeftCornerType);
        }

        private ConnectionType topLeftCornerType;
        public ConnectionType TopLeftCornerType
        {
            get { return topLeftCornerType; }
            set { topLeftCornerType = value; }
        }

        public ConnectedContent BottomConnection
        {
            get { return (ConnectedContent)GetValue(BottomConnectionProperty); }
            set { SetValue(BottomConnectionProperty, value); }
        }

        public static readonly DependencyProperty BottomConnectionProperty =
            DependencyProperty.Register("BottomConnection", typeof(ConnectedContent), typeof(ConnectedContent), new PropertyMetadata(null, new PropertyChangedCallback(OnBottomConnectionChanged)));

        private static void OnBottomConnectionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ConnectedContent originator = (ConnectedContent)o;
            ConnectedContent target = (ConnectedContent)e.NewValue;
            Canvas canvas = (Canvas)originator.Parent;
            originator.ConnectContent(canvas, originator, target, ConnectionType.Bottom, originator.BottomType);
        }

        private ConnectionType bottomType;
        public ConnectionType BottomType
        {
            get { return bottomType; }
            set { bottomType = value; }
        }

        public ConnectedContent TopConnection
        {
            get { return (ConnectedContent)GetValue(TopConnectionProperty); }
            set { SetValue(TopConnectionProperty, value); }
        }

        public static readonly DependencyProperty TopConnectionProperty =
            DependencyProperty.Register("TopConnection", typeof(ConnectedContent), typeof(ConnectedContent), new PropertyMetadata(null, new PropertyChangedCallback(OnTopConnectionChanged)));

        private static void OnTopConnectionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ConnectedContent originator = (ConnectedContent)o;
            ConnectedContent target = (ConnectedContent)e.NewValue;
            Canvas canvas = (Canvas)originator.Parent;
            originator.ConnectContent(canvas, originator, target, ConnectionType.Top, originator.TopType);
        }

        private ConnectionType topType;
        public ConnectionType TopType
        {
            get { return topType; }
            set { topType = value; }
        }

        public ConnectedContent LeftConnection
        {
            get { return (ConnectedContent)GetValue(LeftConnectionProperty); }
            set { SetValue(LeftConnectionProperty, value); }
        }

        public static readonly DependencyProperty LeftConnectionProperty =
            DependencyProperty.Register("LeftConnection", typeof(ConnectedContent), typeof(ConnectedContent), new PropertyMetadata(null, new PropertyChangedCallback(OnLeftConnectionChanged)));

        private static void OnLeftConnectionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ConnectedContent originator = (ConnectedContent)o;
            ConnectedContent target = (ConnectedContent)e.NewValue;
            Canvas canvas = (Canvas)originator.Parent;
            originator.ConnectContent(canvas, originator, target, ConnectionType.Left, originator.LeftType);
        }

        private ConnectionType leftType;
        public ConnectionType LeftType
        {
            get { return leftType; }
            set { leftType = value; }
        }

        public ConnectedContent RightConnection
        {
            get { return (ConnectedContent)GetValue(RightConnectionProperty); }
            set { SetValue(RightConnectionProperty, value); }
        }

        public static readonly DependencyProperty RightConnectionProperty =
            DependencyProperty.Register("RightConnection", typeof(ConnectedContent), typeof(ConnectedContent), new PropertyMetadata(null, new PropertyChangedCallback(OnRightConnectionChanged)));      

        private static void OnRightConnectionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ConnectedContent originator = (ConnectedContent)o;
            ConnectedContent target = (ConnectedContent)e.NewValue;
            Canvas canvas = (Canvas)originator.Parent;
            originator.ConnectContent(canvas, originator, target, ConnectionType.Right, originator.RightType);
        }

        private ConnectionType rightType;
        public ConnectionType RightType
        {
            get { return rightType; }
            set { rightType = value; }
        }

        public void ConnectContent(Canvas canvas, ConnectedContent originator, ConnectedContent target, ConnectionType originatorType, ConnectionType targetType)
        {
            double x1 = 0;
            double x2 = 0;
            double y1 = 0;
            double y2 = 0;

            switch (originatorType)
            {
                case ConnectionType.BottomLeftCorner:
                    x1 = Canvas.GetLeft(originator);
                    y1 = Canvas.GetTop(originator) + originator.Height;
                    break;
                case ConnectionType.Bottom:
                    x1 = Canvas.GetLeft(originator) + (originator.Width / 2);
                    y1 = Canvas.GetTop(originator) + originator.Height;
                    break;
                case ConnectionType.BottomRightCorner:
                    x1 = Canvas.GetLeft(originator) + originator.Width;
                    y1 = Canvas.GetTop(originator) + originator.Height;
                    break;
                case ConnectionType.Left:
                    x1 = Canvas.GetLeft(originator);
                    y1 = Canvas.GetTop(originator) + (originator.Height / 2);
                    break;
                case ConnectionType.Right:
                    x1 = Canvas.GetLeft(originator) + originator.Width;
                    y1 = Canvas.GetTop(originator) + (originator.Height / 2);
                    break;
                case ConnectionType.TopLeftCorner:
                    x1 = Canvas.GetLeft(originator);
                    y1 = Canvas.GetTop(originator);
                    break;
                case ConnectionType.Top:
                    x1 = Canvas.GetLeft(originator) + (originator.Width / 2);
                    y1 = Canvas.GetTop(originator);
                    break;
                case ConnectionType.TopRightCorner:
                    x1 = Canvas.GetLeft(originator) + originator.Width;
                    y1 = Canvas.GetTop(originator);
                    break;
            }

            switch (targetType)
            {
                case ConnectionType.BottomLeftCorner:
                    x2 = Canvas.GetLeft(target);
                    y2 = Canvas.GetTop(target) + target.Height;
                    break;
                case ConnectionType.Bottom:
                    x2 = Canvas.GetLeft(target) + (target.Width / 2);
                    y2 = Canvas.GetTop(target) + target.Height;
                    break;
                case ConnectionType.BottomRightCorner:
                    x2 = Canvas.GetLeft(target) + target.Width;
                    y2 = Canvas.GetTop(target) + target.Height;
                    break;
                case ConnectionType.Left:
                    x2 = Canvas.GetLeft(target);
                    y2 = Canvas.GetTop(target) + (target.Height / 2);
                    break;
                case ConnectionType.Right:
                    x2 = Canvas.GetLeft(target) + target.Width;
                    y2 = Canvas.GetTop(target) + (target.Height / 2);
                    break;
                case ConnectionType.TopLeftCorner:
                    x2 = Canvas.GetLeft(target);
                    y2 = Canvas.GetTop(target);
                    break;
                case ConnectionType.Top:
                    x2 = Canvas.GetLeft(target) + (target.Width / 2);
                    y2 = Canvas.GetTop(target);
                    break;
                case ConnectionType.TopRightCorner:
                    x2 = Canvas.GetLeft(target) + target.Width;
                    y2 = Canvas.GetTop(target);
                    break;
            }

            canvas.Children.Add(new Line() { X1 = x1, X2 = x2, Y1 = y1, Y2 = y2, Stroke = System.Windows.Media.Brushes.LimeGreen, StrokeThickness = 2 });
        }
    }
}
