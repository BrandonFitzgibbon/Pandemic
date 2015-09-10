using Presentation.WPF.Contracts;
using Presentation.WPF.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Presentation.WPF.Controls
{
    public partial class ExtendedBorder : Border
    {
        public static bool GetIsOpen(DependencyObject o)
        {
            return (bool)o.GetValue(IsOpenProperty);
        }

        public static void SetIsOpen(DependencyObject o, bool value)
        {
            o.SetValue(IsOpenProperty, value);
        }

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.RegisterAttached("IsOpen", typeof(bool), typeof(ExtendedBorder), new PropertyMetadata(new PropertyChangedCallback(OnIsOpenChanged)));

        public static void OnIsOpenChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                Border bd = (Border)o;
                Storyboard sb = new Storyboard();
                DoubleAnimation dblani = new DoubleAnimation() { Duration = TimeSpan.FromMilliseconds(300), From = -450, To = 0 };
                sb.Children.Add(dblani);
                Storyboard.SetTargetProperty(dblani, new PropertyPath("RenderTransform.X"));
                Storyboard.SetTarget(dblani, bd);
                sb.Begin();
            }
            else if((bool)e.NewValue == false)
            {
                Border bd = (Border)o;
                Storyboard sb = new Storyboard();
                DoubleAnimation dblani = new DoubleAnimation() { Duration = TimeSpan.FromMilliseconds(300), From = 0, To = -450 };
                sb.Children.Add(dblani);
                Storyboard.SetTargetProperty(dblani, new PropertyPath("RenderTransform.X"));
                Storyboard.SetTarget(dblani, bd);
                sb.Begin();
            }
        }
    }

    public partial class ExtendedCanvas : Canvas
    {
        public static IEnumerable<IAnchorViewModel> GetAnchorCollection(DependencyObject o)
        {
            return (IEnumerable<IAnchorViewModel>)o.GetValue(AnchorCollectionProperty);
        }

        public static void SetAnchorCollection(DependencyObject o, IEnumerable<IAnchorViewModel> value)
        {
            o.SetValue(AnchorCollectionProperty, value);
        }

        public static readonly DependencyProperty AnchorCollectionProperty =
            DependencyProperty.RegisterAttached("AnchorCollection", typeof(IEnumerable<IAnchorViewModel>), typeof(ExtendedCanvas), new PropertyMetadata(new PropertyChangedCallback(OnAnchorCollectionChanged)));

        public static void OnAnchorCollectionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            IEnumerable<IAnchorViewModel> anchorViewModels = e.NewValue as IEnumerable<IAnchorViewModel>;
            Canvas canvas = o as Canvas;

            if (anchorViewModels != null && canvas != null)
            {
                foreach (IAnchorViewModel avm in anchorViewModels)
                {
                    Anchor anchor = new Anchor();
                    anchor.DataContext = avm;
                    canvas.Children.Add(anchor);
                }
            }
        }

        public static IEnumerable<IConnectionViewModel> GetConnectionCollection(DependencyObject o)
        {
            return (IEnumerable<IConnectionViewModel>)o.GetValue(ConnectionCollectionProperty);
        }

        public static void SetConnectionCollection(DependencyObject o, IEnumerable<IConnectionViewModel> value)
        {
            o.SetValue(ConnectionCollectionProperty, value);
        }

        public static readonly DependencyProperty ConnectionCollectionProperty =
            DependencyProperty.RegisterAttached("ConnectionCollection", typeof(IEnumerable<IConnectionViewModel>), typeof(ExtendedCanvas), new PropertyMetadata(new PropertyChangedCallback(OnConnectionCollectionChanged)));

        public static void OnConnectionCollectionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            IEnumerable<IConnectionViewModel> connectionViewModels = e.NewValue as IEnumerable<IConnectionViewModel>;
            Canvas canvas = o as Canvas;

            if (connectionViewModels != null && canvas != null)
            {
                foreach (IConnectionViewModel cvm in connectionViewModels)
                {
                    Line connection = new Line();
                    connection.DataContext = cvm;
                    connection.SetBinding(Line.X1Property, new Binding("X1"));
                    connection.SetBinding(Line.X2Property, new Binding("X2"));
                    connection.SetBinding(Line.Y1Property, new Binding("Y1"));
                    connection.SetBinding(Line.Y2Property, new Binding("Y2"));
                    connection.SetBinding(Line.StrokeProperty, new Binding("Stroke"));
                    connection.SetBinding(Line.StrokeThicknessProperty, new Binding("Thickness"));
                    connection.SetBinding(Line.OpacityProperty, new Binding("Opacity"));
                    connection.SetBinding(Canvas.LeftProperty, new Binding("Left"));
                    connection.SetBinding(Canvas.TopProperty, new Binding("Top"));

                    canvas.Children.Add(connection);
                }
            }
        }
    }
}
