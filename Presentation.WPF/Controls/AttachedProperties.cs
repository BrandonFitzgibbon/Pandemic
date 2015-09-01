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

        public static SolidColorBrush GetBorderColor(DependencyObject o)
        {
            return (SolidColorBrush)o.GetValue(BorderColorProperty);
        }

        public static void SetBorderColor(DependencyObject o, SolidColorBrush value)
        {
            o.SetValue(BorderColorProperty, value);
        }

        public static readonly DependencyProperty BorderColorProperty =
            DependencyProperty.RegisterAttached("BorderColor", typeof(SolidColorBrush), typeof(ExtendedBorder), new PropertyMetadata(new PropertyChangedCallback(OnBorderColorChanged)));

        public static void OnBorderColorChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Border bd = (Border)o;
            Storyboard sb = new Storyboard();
            ColorAnimation colAni = new ColorAnimation();

            SolidColorBrush oldColor = (SolidColorBrush)e.OldValue;
            SolidColorBrush newColor = (SolidColorBrush)e.NewValue;

            if (oldColor == null)
                oldColor = newColor;

            if (newColor == null)
                return;

            RepeatBehavior rb = new RepeatBehavior(TimeSpan.FromMilliseconds(1500));
            TimeSpan duration = TimeSpan.FromMilliseconds(500);

            if (oldColor.Color == System.Windows.Media.Brushes.LimeGreen.Color && newColor.Color == System.Windows.Media.Brushes.Yellow.Color)
            {
                colAni.From = System.Windows.Media.Brushes.LimeGreen.Color;
                colAni.To = System.Windows.Media.Brushes.Yellow.Color;
                colAni.Duration = duration;
                colAni.RepeatBehavior = rb;
                colAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.LimeGreen.Color && newColor.Color == System.Windows.Media.Brushes.Orange.Color)
            {
                colAni.From = System.Windows.Media.Brushes.LimeGreen.Color;
                colAni.To = System.Windows.Media.Brushes.Orange.Color;
                colAni.Duration = duration;
                colAni.RepeatBehavior = rb;
                colAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.LimeGreen.Color && newColor.Color == System.Windows.Media.Brushes.Red.Color)
            {
                colAni.From = System.Windows.Media.Brushes.LimeGreen.Color;
                colAni.To = System.Windows.Media.Brushes.Red.Color;
                colAni.Duration = duration;
                colAni.RepeatBehavior = rb;
                colAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Yellow.Color && newColor.Color == System.Windows.Media.Brushes.LimeGreen.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Yellow.Color;
                colAni.To = System.Windows.Media.Brushes.LimeGreen.Color;
                colAni.Duration = duration;
                colAni.RepeatBehavior = rb;
                colAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Yellow.Color && newColor.Color == System.Windows.Media.Brushes.Yellow.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Yellow.Color;
                colAni.To = System.Windows.Media.Brushes.Yellow.Color;
                colAni.Duration = duration;
                colAni.RepeatBehavior = rb;
                colAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Yellow.Color && newColor.Color == System.Windows.Media.Brushes.Orange.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Yellow.Color;
                colAni.To = System.Windows.Media.Brushes.Orange.Color;
                colAni.Duration = duration;
                colAni.RepeatBehavior = rb;
                colAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Yellow.Color && newColor.Color == System.Windows.Media.Brushes.Red.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Yellow.Color;
                colAni.To = System.Windows.Media.Brushes.Red.Color;
                colAni.Duration = duration;
                colAni.RepeatBehavior = rb;
                colAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Orange.Color && newColor.Color == System.Windows.Media.Brushes.LimeGreen.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Orange.Color;
                colAni.To = System.Windows.Media.Brushes.LimeGreen.Color;
                colAni.Duration = duration;
                colAni.RepeatBehavior = rb;
                colAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Orange.Color && newColor.Color == System.Windows.Media.Brushes.Yellow.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Orange.Color;
                colAni.To = System.Windows.Media.Brushes.Yellow.Color;
                colAni.Duration = duration;
                colAni.RepeatBehavior = rb;
                colAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Orange.Color && newColor.Color == System.Windows.Media.Brushes.Orange.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Orange.Color;
                colAni.To = System.Windows.Media.Brushes.Orange.Color;
                colAni.Duration = duration;
                colAni.RepeatBehavior = rb;
                colAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Orange.Color && newColor.Color == System.Windows.Media.Brushes.Red.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Orange.Color;
                colAni.To = System.Windows.Media.Brushes.Red.Color;
                colAni.Duration = duration;
                colAni.RepeatBehavior = rb;
                colAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Red.Color && newColor.Color == System.Windows.Media.Brushes.LimeGreen.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Red.Color;
                colAni.To = System.Windows.Media.Brushes.LimeGreen.Color;
                colAni.Duration = duration;
                colAni.RepeatBehavior = rb;
                colAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Red.Color && newColor.Color == System.Windows.Media.Brushes.Yellow.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Red.Color;
                colAni.To = System.Windows.Media.Brushes.Yellow.Color;
                colAni.Duration = duration;
                colAni.RepeatBehavior = rb;
                colAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Red.Color && newColor.Color == System.Windows.Media.Brushes.Orange.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Red.Color;
                colAni.To = System.Windows.Media.Brushes.Orange.Color;
                colAni.Duration = duration;
                colAni.RepeatBehavior = rb;
                colAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Red.Color && newColor.Color == System.Windows.Media.Brushes.Red.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Red.Color;
                colAni.To = System.Windows.Media.Brushes.Red.Color;
                colAni.Duration = duration;
                colAni.RepeatBehavior = rb;
                colAni.AutoReverse = true;
            }

            if (colAni.From != null && colAni.To != null && bd.BorderBrush != null)
            {
                ThicknessAnimation thkAni = new ThicknessAnimation();
                thkAni.From = bd.BorderThickness;
                thkAni.To = new Thickness(bd.BorderThickness.Left + 4, bd.BorderThickness.Top + 4, bd.BorderThickness.Right + 4, bd.BorderThickness.Bottom + 4);
                thkAni.Duration = duration;
                thkAni.RepeatBehavior = new RepeatBehavior(rb.Duration + TimeSpan.FromMilliseconds(500));
                thkAni.AutoReverse = true;

                ThicknessAnimation padAni = new ThicknessAnimation();
                padAni.From = bd.Padding;
                padAni.To = new Thickness(bd.Padding.Left - 4, bd.Padding.Top - 4, bd.Padding.Right - 4, bd.Padding.Bottom - 4);
                padAni.Duration = duration;
                padAni.RepeatBehavior = new RepeatBehavior(rb.Duration + TimeSpan.FromMilliseconds(500));
                padAni.AutoReverse = true;

                sb.Children.Add(colAni);
                sb.Children.Add(thkAni);
                sb.Children.Add(padAni);

                Storyboard.SetTargetProperty(colAni, new PropertyPath("BorderBrush.Color"));
                Storyboard.SetTarget(colAni, bd);

                Storyboard.SetTargetProperty(thkAni, new PropertyPath("BorderThickness"));
                Storyboard.SetTarget(thkAni, bd);

                Storyboard.SetTargetProperty(padAni, new PropertyPath("Padding"));
                Storyboard.SetTarget(padAni, bd);

                sb.Begin();
            }
                
        }
    }
}
