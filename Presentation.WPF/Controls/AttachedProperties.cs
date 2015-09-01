using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

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
}
