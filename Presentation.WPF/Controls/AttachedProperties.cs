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

        public Thickness AnimatedBorderThickness { get; set; }
        public Thickness FrozenBorderThickness { get; set; }
        public Thickness AnimatedPadding { get; set; }
        public Thickness FrozenPadding { get; set; }

        public static void OnBorderColorChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ExtendedBorder bd = (ExtendedBorder)o;
            Storyboard sb = new Storyboard();

            ColorAnimation colAni = new ColorAnimation();
            ThicknessAnimation thkAni = new ThicknessAnimation();
            ThicknessAnimation padAni = new ThicknessAnimation();

            SolidColorBrush oldColor = (SolidColorBrush)e.OldValue;
            SolidColorBrush newColor = (SolidColorBrush)e.NewValue;                     

            if (oldColor == null)
                oldColor = newColor;

            if (newColor == null)
                return;

            RepeatBehavior rbYellow = new RepeatBehavior(TimeSpan.FromMilliseconds(750));
            TimeSpan durationYellow = TimeSpan.FromMilliseconds(750);

            RepeatBehavior rbOrange = new RepeatBehavior(TimeSpan.FromMilliseconds(500));
            TimeSpan durationOrange = TimeSpan.FromMilliseconds(500);

            RepeatBehavior rbRed = new RepeatBehavior(TimeSpan.FromMilliseconds(250));
            TimeSpan durationRed = TimeSpan.FromMilliseconds(250);

            if (oldColor.Color == System.Windows.Media.Brushes.LimeGreen.Color && newColor.Color == System.Windows.Media.Brushes.Yellow.Color)
            {
                colAni.From = System.Windows.Media.Brushes.LimeGreen.Color;
                colAni.To = System.Windows.Media.Brushes.Yellow.Color;
                colAni.Duration = durationYellow;
                colAni.RepeatBehavior = rbYellow;
                colAni.AutoReverse = true;

                thkAni.From = bd.FrozenBorderThickness;
                thkAni.To = new Thickness(bd.AnimatedBorderThickness.Left, bd.AnimatedBorderThickness.Top, bd.AnimatedBorderThickness.Right, bd.AnimatedBorderThickness.Bottom);
                thkAni.Duration = durationYellow;
                thkAni.RepeatBehavior = RepeatBehavior.Forever;
                thkAni.AutoReverse = true;

                padAni.From = bd.FrozenPadding;
                padAni.To = new Thickness(bd.AnimatedPadding.Left, bd.AnimatedPadding.Top, bd.AnimatedPadding.Right, bd.AnimatedPadding.Bottom);
                padAni.Duration = durationYellow;
                padAni.RepeatBehavior = RepeatBehavior.Forever;
                padAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.LimeGreen.Color && newColor.Color == System.Windows.Media.Brushes.Orange.Color)
            {
                colAni.From = System.Windows.Media.Brushes.LimeGreen.Color;
                colAni.To = System.Windows.Media.Brushes.Orange.Color;
                colAni.Duration = durationOrange;
                colAni.RepeatBehavior = rbOrange;
                colAni.AutoReverse = true;

                thkAni.From = bd.FrozenBorderThickness;
                thkAni.To = new Thickness(bd.AnimatedBorderThickness.Left, bd.AnimatedBorderThickness.Top, bd.AnimatedBorderThickness.Right, bd.AnimatedBorderThickness.Bottom);
                thkAni.Duration = durationOrange;
                thkAni.RepeatBehavior = RepeatBehavior.Forever;
                thkAni.AutoReverse = true;

                padAni.From = bd.FrozenPadding;
                padAni.To = new Thickness(bd.AnimatedPadding.Left, bd.AnimatedPadding.Top, bd.AnimatedPadding.Right, bd.AnimatedPadding.Bottom);
                padAni.Duration = durationOrange;
                padAni.RepeatBehavior = RepeatBehavior.Forever;
                padAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.LimeGreen.Color && newColor.Color == System.Windows.Media.Brushes.Red.Color)
            {
                colAni.From = System.Windows.Media.Brushes.LimeGreen.Color;
                colAni.To = System.Windows.Media.Brushes.Red.Color;
                colAni.Duration = durationRed;
                colAni.RepeatBehavior = rbRed;
                colAni.AutoReverse = true;

                thkAni.From = bd.FrozenBorderThickness;
                thkAni.To = new Thickness(bd.AnimatedBorderThickness.Left, bd.AnimatedBorderThickness.Top, bd.AnimatedBorderThickness.Right, bd.AnimatedBorderThickness.Bottom);
                thkAni.Duration = durationRed;
                thkAni.RepeatBehavior = RepeatBehavior.Forever;
                thkAni.AutoReverse = true;

                padAni.From = bd.FrozenPadding;
                padAni.To = new Thickness(bd.AnimatedPadding.Left, bd.AnimatedPadding.Top, bd.AnimatedPadding.Right, bd.AnimatedPadding.Bottom);
                padAni.Duration = durationRed;
                padAni.RepeatBehavior = RepeatBehavior.Forever;
                padAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Yellow.Color && newColor.Color == System.Windows.Media.Brushes.LimeGreen.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Yellow.Color;
                colAni.To = System.Windows.Media.Brushes.LimeGreen.Color;
                colAni.Duration = durationOrange;
                colAni.RepeatBehavior = rbOrange;
                colAni.AutoReverse = true;

                thkAni.From = bd.AnimatedBorderThickness;
                thkAni.To = new Thickness(bd.FrozenBorderThickness.Left, bd.FrozenBorderThickness.Top, bd.FrozenBorderThickness.Right, bd.FrozenBorderThickness.Bottom);
                thkAni.Duration = durationOrange;
                padAni.From = bd.AnimatedPadding;
                padAni.To = new Thickness(bd.FrozenPadding.Left, bd.FrozenPadding.Top, bd.FrozenPadding.Right, bd.FrozenPadding.Bottom);
                padAni.Duration = durationOrange;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Yellow.Color && newColor.Color == System.Windows.Media.Brushes.Yellow.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Yellow.Color;
                colAni.To = System.Windows.Media.Brushes.Yellow.Color;
                colAni.Duration = durationYellow;
                colAni.RepeatBehavior = rbYellow;
                colAni.AutoReverse = true;

                thkAni.From = bd.FrozenBorderThickness;
                thkAni.To = new Thickness(bd.AnimatedBorderThickness.Left, bd.AnimatedBorderThickness.Top, bd.AnimatedBorderThickness.Right, bd.AnimatedBorderThickness.Bottom);
                thkAni.Duration = durationYellow ;
                thkAni.RepeatBehavior = RepeatBehavior.Forever;
                thkAni.AutoReverse = true;

                padAni.From = bd.FrozenPadding;
                padAni.To = new Thickness(bd.AnimatedPadding.Left, bd.AnimatedPadding.Top, bd.AnimatedPadding.Right, bd.AnimatedPadding.Bottom);
                padAni.Duration = durationYellow;
                padAni.RepeatBehavior = RepeatBehavior.Forever;
                padAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Yellow.Color && newColor.Color == System.Windows.Media.Brushes.Orange.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Yellow.Color;
                colAni.To = System.Windows.Media.Brushes.Orange.Color;
                colAni.Duration = durationOrange;
                colAni.RepeatBehavior = rbOrange;
                colAni.AutoReverse = true;

                thkAni.From = bd.FrozenBorderThickness;
                thkAni.To = new Thickness(bd.AnimatedBorderThickness.Left, bd.AnimatedBorderThickness.Top, bd.AnimatedBorderThickness.Right, bd.AnimatedBorderThickness.Bottom);
                thkAni.Duration = durationOrange;
                thkAni.RepeatBehavior = RepeatBehavior.Forever;
                thkAni.AutoReverse = true;

                padAni.From = bd.FrozenPadding;
                padAni.To = new Thickness(bd.AnimatedPadding.Left, bd.AnimatedPadding.Top, bd.AnimatedPadding.Right, bd.AnimatedPadding.Bottom);
                padAni.Duration = durationOrange;
                padAni.RepeatBehavior = RepeatBehavior.Forever;
                padAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Yellow.Color && newColor.Color == System.Windows.Media.Brushes.Red.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Yellow.Color;
                colAni.To = System.Windows.Media.Brushes.Red.Color;
                colAni.Duration = durationRed;
                colAni.RepeatBehavior = rbRed;
                colAni.AutoReverse = true;

                thkAni.From = bd.FrozenBorderThickness;
                thkAni.To = new Thickness(bd.AnimatedBorderThickness.Left, bd.AnimatedBorderThickness.Top, bd.AnimatedBorderThickness.Right, bd.AnimatedBorderThickness.Bottom);
                thkAni.Duration = durationRed;
                thkAni.RepeatBehavior = RepeatBehavior.Forever;
                thkAni.AutoReverse = true;

                padAni.From = bd.FrozenPadding;
                padAni.To = new Thickness(bd.AnimatedPadding.Left, bd.AnimatedPadding.Top, bd.AnimatedPadding.Right, bd.AnimatedPadding.Bottom);
                padAni.Duration = durationRed;
                padAni.RepeatBehavior = RepeatBehavior.Forever;
                padAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Orange.Color && newColor.Color == System.Windows.Media.Brushes.LimeGreen.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Orange.Color;
                colAni.To = System.Windows.Media.Brushes.LimeGreen.Color;
                colAni.Duration = durationOrange;
                colAni.RepeatBehavior = rbOrange;
                colAni.AutoReverse = true;

                thkAni.From = bd.AnimatedBorderThickness;
                thkAni.To = new Thickness(bd.FrozenBorderThickness.Left, bd.FrozenBorderThickness.Top, bd.FrozenBorderThickness.Right, bd.FrozenBorderThickness.Bottom);
                thkAni.Duration = durationOrange;
                padAni.From = bd.AnimatedPadding;
                padAni.To = new Thickness(bd.FrozenPadding.Left, bd.FrozenPadding.Top, bd.FrozenPadding.Right, bd.FrozenPadding.Bottom);
                padAni.Duration = durationOrange;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Orange.Color && newColor.Color == System.Windows.Media.Brushes.Yellow.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Orange.Color;
                colAni.To = System.Windows.Media.Brushes.Yellow.Color;
                colAni.Duration = durationYellow;
                colAni.RepeatBehavior = rbYellow;
                colAni.AutoReverse = true;

                thkAni.From = bd.FrozenBorderThickness;
                thkAni.To = new Thickness(bd.AnimatedBorderThickness.Left, bd.AnimatedBorderThickness.Top, bd.AnimatedBorderThickness.Right, bd.AnimatedBorderThickness.Bottom);
                thkAni.Duration = durationYellow;
                thkAni.RepeatBehavior = RepeatBehavior.Forever;
                thkAni.AutoReverse = true;

                padAni.From = bd.FrozenPadding;
                padAni.To = new Thickness(bd.AnimatedPadding.Left, bd.AnimatedPadding.Top, bd.AnimatedPadding.Right, bd.AnimatedPadding.Bottom);
                padAni.Duration = durationYellow;
                padAni.RepeatBehavior = RepeatBehavior.Forever;
                padAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Orange.Color && newColor.Color == System.Windows.Media.Brushes.Orange.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Orange.Color;
                colAni.To = System.Windows.Media.Brushes.Orange.Color;
                colAni.Duration = durationOrange;
                colAni.RepeatBehavior = rbOrange;
                colAni.AutoReverse = true;

                thkAni.From = bd.FrozenBorderThickness;
                thkAni.To = new Thickness(bd.AnimatedBorderThickness.Left, bd.AnimatedBorderThickness.Top, bd.AnimatedBorderThickness.Right, bd.AnimatedBorderThickness.Bottom);
                thkAni.Duration = durationOrange;
                thkAni.RepeatBehavior = RepeatBehavior.Forever;
                thkAni.AutoReverse = true;

                padAni.From = bd.FrozenPadding;
                padAni.To = new Thickness(bd.AnimatedPadding.Left, bd.AnimatedPadding.Top, bd.AnimatedPadding.Right, bd.AnimatedPadding.Bottom);
                padAni.Duration = durationOrange;
                padAni.RepeatBehavior = RepeatBehavior.Forever;
                padAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Orange.Color && newColor.Color == System.Windows.Media.Brushes.Red.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Orange.Color;
                colAni.To = System.Windows.Media.Brushes.Red.Color;
                colAni.Duration = durationRed;
                colAni.RepeatBehavior = rbRed;
                colAni.AutoReverse = true;

                thkAni.From = bd.FrozenBorderThickness;
                thkAni.To = new Thickness(bd.AnimatedBorderThickness.Left, bd.AnimatedBorderThickness.Top, bd.AnimatedBorderThickness.Right, bd.AnimatedBorderThickness.Bottom);
                thkAni.Duration = durationRed;
                thkAni.RepeatBehavior = RepeatBehavior.Forever;
                thkAni.AutoReverse = true;

                padAni.From = bd.FrozenPadding;
                padAni.To = new Thickness(bd.AnimatedPadding.Left, bd.AnimatedPadding.Top, bd.AnimatedPadding.Right, bd.AnimatedPadding.Bottom);
                padAni.Duration = durationRed;
                padAni.RepeatBehavior = RepeatBehavior.Forever;
                padAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Red.Color && newColor.Color == System.Windows.Media.Brushes.LimeGreen.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Red.Color;
                colAni.To = System.Windows.Media.Brushes.LimeGreen.Color;
                colAni.Duration = durationOrange;
                colAni.RepeatBehavior = rbOrange;
                colAni.AutoReverse = true;

                thkAni.From = bd.AnimatedBorderThickness;
                thkAni.To = new Thickness(bd.FrozenBorderThickness.Left, bd.FrozenBorderThickness.Top, bd.FrozenBorderThickness.Right, bd.FrozenBorderThickness.Bottom);
                thkAni.Duration = durationOrange;
                padAni.From = bd.AnimatedPadding;
                padAni.To = new Thickness(bd.FrozenPadding.Left, bd.FrozenPadding.Top, bd.FrozenPadding.Right, bd.FrozenPadding.Bottom);
                padAni.Duration = durationOrange;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Red.Color && newColor.Color == System.Windows.Media.Brushes.Yellow.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Red.Color;
                colAni.To = System.Windows.Media.Brushes.Yellow.Color;
                colAni.Duration = durationYellow;
                colAni.RepeatBehavior = rbYellow;
                colAni.AutoReverse = true;

                thkAni.From = bd.FrozenBorderThickness;
                thkAni.To = new Thickness(bd.AnimatedBorderThickness.Left, bd.AnimatedBorderThickness.Top, bd.AnimatedBorderThickness.Right, bd.AnimatedBorderThickness.Bottom);
                thkAni.Duration = durationYellow;
                thkAni.RepeatBehavior = RepeatBehavior.Forever;
                thkAni.AutoReverse = true;

                padAni.From = bd.FrozenPadding;
                padAni.To = new Thickness(bd.AnimatedPadding.Left, bd.AnimatedPadding.Top, bd.AnimatedPadding.Right, bd.AnimatedPadding.Bottom);
                padAni.Duration = durationYellow;
                padAni.RepeatBehavior = RepeatBehavior.Forever;
                padAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Red.Color && newColor.Color == System.Windows.Media.Brushes.Orange.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Red.Color;
                colAni.To = System.Windows.Media.Brushes.Orange.Color;
                colAni.Duration = durationOrange;
                colAni.RepeatBehavior = rbOrange;
                colAni.AutoReverse = true;

                thkAni.From = bd.FrozenBorderThickness;
                thkAni.To = new Thickness(bd.AnimatedBorderThickness.Left, bd.AnimatedBorderThickness.Top, bd.AnimatedBorderThickness.Right, bd.AnimatedBorderThickness.Bottom);
                thkAni.Duration = durationOrange;
                thkAni.RepeatBehavior = RepeatBehavior.Forever;
                thkAni.AutoReverse = true;

                padAni.From = bd.FrozenPadding;
                padAni.To = new Thickness(bd.AnimatedPadding.Left, bd.AnimatedPadding.Top, bd.AnimatedPadding.Right, bd.AnimatedPadding.Bottom);
                padAni.Duration = durationOrange;
                padAni.RepeatBehavior = RepeatBehavior.Forever;
                padAni.AutoReverse = true;
            }

            if (oldColor.Color == System.Windows.Media.Brushes.Red.Color && newColor.Color == System.Windows.Media.Brushes.Red.Color)
            {
                colAni.From = System.Windows.Media.Brushes.Red.Color;
                colAni.To = System.Windows.Media.Brushes.Red.Color;
                colAni.Duration = durationRed;
                colAni.RepeatBehavior = rbRed;
                colAni.AutoReverse = true;

                thkAni.From = bd.FrozenBorderThickness;
                thkAni.To = new Thickness(bd.AnimatedBorderThickness.Left, bd.AnimatedBorderThickness.Top, bd.AnimatedBorderThickness.Right, bd.AnimatedBorderThickness.Bottom);
                thkAni.Duration = durationRed;
                thkAni.RepeatBehavior = RepeatBehavior.Forever;
                thkAni.AutoReverse = true;

                padAni.From = bd.FrozenPadding;
                padAni.To = new Thickness(bd.AnimatedPadding.Left, bd.AnimatedPadding.Top, bd.AnimatedPadding.Right, bd.AnimatedPadding.Bottom);
                padAni.Duration = durationRed;
                padAni.RepeatBehavior = RepeatBehavior.Forever;
                padAni.AutoReverse = true;
            }

            if (colAni.From != null && colAni.To != null && bd.BorderBrush != null)
            {
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
