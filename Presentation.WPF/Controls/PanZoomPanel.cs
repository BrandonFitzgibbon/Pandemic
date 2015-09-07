using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Presentation.WPF.Controls
{
    [ContentProperty("Content")]
    [TemplatePart(Name = "PART_Content", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_Right", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_Top", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_Left", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_Bottom", Type = typeof(FrameworkElement))]
    public class PanZoomPanel : Control
    {
        private TransformGroup tg = new TransformGroup();
        private ScaleTransform st = new ScaleTransform();
        private TranslateTransform tt = new TranslateTransform();
        private DispatcherTimer timer;
        private DoubleAnimation ani;
        private int horizontal;
        private int vertical;

        Border Top;
        Border Left;
        Border Right;
        Border Bottom;

        static PanZoomPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PanZoomPanel), new FrameworkPropertyMetadata(typeof(PanZoomPanel)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            tg.Children.Add(st);
            tg.Children.Add(tt);

            ContentPresenter Content = (ContentPresenter)Template.FindName("PART_Content", this);
            Top = (Border)Template.FindName("PART_Top", this);
            Left = (Border)Template.FindName("PART_Left", this);
            Right = (Border)Template.FindName("PART_Right", this);
            Bottom = (Border)Template.FindName("PART_Bottom", this);

            MouseWheel += OnMouseWheel;
            Top.MouseLeftButtonDown += Top_MouseLeftButtonDown;
            Left.MouseLeftButtonDown += Left_MouseLeftButtonDown;
            Right.MouseLeftButtonDown += Right_MouseLeftButtonDown;
            Bottom.MouseLeftButtonDown += Bottom_MouseLeftButtonDown;
            MouseLeftButtonUp += PanZoomPanel_MouseLeftButtonUp;
            Loaded += PanZoomPanel_Loaded;

            timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 1, 0) };
            timer.Tick += Timer_Tick;
        }

        private void PanZoomPanel_Loaded(object sender, RoutedEventArgs e)
        {
            Top.Height = ActualHeight * 0.025;
            Left.Width = ActualHeight * 0.025;
            Right.Width = ActualHeight * 0.025;
            Bottom.Height = ActualHeight * 0.025;
            tt.X = -120;
        }

        private void PanZoomPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            horizontal = 0;
            vertical = 0;
            double tempX = tt.X;
            double tempY = tt.Y;
            tt.BeginAnimation(TranslateTransform.XProperty, null);
            tt.BeginAnimation(TranslateTransform.YProperty, null);
            tt.X = tempX;
            tt.Y = tempY;
        }

        private void Bottom_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ani = new DoubleAnimation(tt.Y, tt.Y - 1, TimeSpan.FromMilliseconds(5));
            ani.IsCumulative = true;
            ani.RepeatBehavior = RepeatBehavior.Forever;
            tt.BeginAnimation(TranslateTransform.YProperty, ani);
            timer.Start();
        }

        private void Right_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ani = new DoubleAnimation(tt.X, tt.X - 1, TimeSpan.FromMilliseconds(5));
            ani.IsCumulative = true;
            ani.RepeatBehavior = RepeatBehavior.Forever;
            tt.BeginAnimation(TranslateTransform.XProperty, ani);
            timer.Start();
        }

        private void Left_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ani = new DoubleAnimation(tt.X, tt.X + 1, TimeSpan.FromMilliseconds(5));
            ani.IsCumulative = true;
            ani.RepeatBehavior = RepeatBehavior.Forever;
            tt.BeginAnimation(TranslateTransform.XProperty, ani);
            timer.Start();
        }

        private void Top_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ani = new DoubleAnimation(tt.Y, tt.Y + 1, TimeSpan.FromMilliseconds(5));
            ani.IsCumulative = true;
            ani.RepeatBehavior = RepeatBehavior.Forever;
            tt.BeginAnimation(TranslateTransform.YProperty, ani);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (tt.X > Content.ActualWidth * Scale)
            {
                tt.BeginAnimation(TranslateTransform.XProperty, null);
                tt.X = -Content.ActualWidth * Scale;
                ani = new DoubleAnimation(tt.X, tt.X + 1, TimeSpan.FromMilliseconds(5));
                ani.IsCumulative = true;
                ani.RepeatBehavior = RepeatBehavior.Forever;
                tt.BeginAnimation(TranslateTransform.XProperty, ani);
            }

            if (tt.X < -1 * (Content.ActualWidth * Scale))
            {
                tt.BeginAnimation(TranslateTransform.XProperty, null);
                tt.X = Content.ActualWidth * Scale;
                ani = new DoubleAnimation(tt.X, tt.X - 1, TimeSpan.FromMilliseconds(5));
                ani.IsCumulative = true;
                ani.RepeatBehavior = RepeatBehavior.Forever;
                tt.BeginAnimation(TranslateTransform.XProperty, ani);
            }

            if (tt.Y > Content.ActualHeight * Scale)
            {
                tt.BeginAnimation(TranslateTransform.YProperty, null);
                tt.Y = -Content.ActualHeight * Scale;
                ani = new DoubleAnimation(tt.Y, tt.Y + 1, TimeSpan.FromMilliseconds(5));
                ani.IsCumulative = true;
                ani.RepeatBehavior = RepeatBehavior.Forever;
                tt.BeginAnimation(TranslateTransform.YProperty, ani);
            }

            if (tt.Y < -1 * (Content.ActualHeight * Scale))
            {
                tt.BeginAnimation(TranslateTransform.YProperty, null);
                tt.Y = Content.ActualHeight * Scale;
                ani = new DoubleAnimation(tt.Y, tt.Y - 1, TimeSpan.FromMilliseconds(5));
                ani.IsCumulative = true;
                ani.RepeatBehavior = RepeatBehavior.Forever;
                tt.BeginAnimation(TranslateTransform.YProperty, ani);
            }        
        }

        private void OnMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            double zoom = e.Delta > 0 ? 0.05 : -0.05;

            if(Content != null)
            {
                Point pt = Mouse.GetPosition(Content);
                Point parentPt = Mouse.GetPosition(this);

                if (Math.Round(st.ScaleX + zoom, 2) < 0.32 || Math.Round(st.ScaleY + zoom, 2) > 2.3)
                    return;

                st.ScaleX = Math.Round(st.ScaleX + zoom, 2);
                st.ScaleY = Math.Round(st.ScaleY + zoom, 2);

                Point ptScaled = new Point() { X = pt.X * st.ScaleX, Y = pt.Y * st.ScaleY };
                Point difference = new Point() { X = parentPt.X - ptScaled.X, Y = parentPt.Y - ptScaled.Y };

                tt.X = difference.X;
                tt.Y = difference.Y;

                Content.RenderTransform = tg;

                Scale = Scale + zoom;
            }
        }

        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.Register("Scale", typeof(double), typeof(PanZoomPanel), new PropertyMetadata(1.0, new PropertyChangedCallback(OnScaleChanged)));

        public static void OnScaleChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            PanZoomPanel pzp = (PanZoomPanel)o;
            pzp.st.ScaleX = (double)e.NewValue;
            pzp.st.ScaleY = (double)e.NewValue;
        }

        public FrameworkElement Content
        {
            get { return (FrameworkElement)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(FrameworkElement), typeof(PanZoomPanel), new PropertyMetadata(new PropertyChangedCallback(OnContentChanged)));

        public static void OnContentChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            PanZoomPanel pzp = (PanZoomPanel)o;
            FrameworkElement content = (FrameworkElement)e.NewValue;
            if (content != null)
            {
                content.RenderTransform = pzp.tg;
            }
        }
    }
}
