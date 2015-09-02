using System;
using System.Collections.Generic;
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

namespace Presentation.WPF.Controls
{
    [TemplatePart(Name = "PART_GlowBorder", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_ContentBorder", Type = typeof(FrameworkElement))] 
    public class Anchor : Control
    {
        static Anchor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Anchor), new FrameworkPropertyMetadata(typeof(Anchor)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Border glowBorder = (Border)Template.FindName("PART_GlowBorder", this);
            Border contentBorder = (Border)Template.FindName("PART_ContentBorder", this);

            glowBorder.Padding = new Thickness(1.1 * contentBorder.Padding.Left, 1.1 * contentBorder.Padding.Top, 1.1 * contentBorder.Padding.Right, 1.1 * contentBorder.Padding.Bottom);
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(Anchor), new PropertyMetadata(new CornerRadius(0)));


        public SolidColorBrush GlowBrush
        {
            get { return (SolidColorBrush)GetValue(GlowBrushProperty); }
            set { SetValue(GlowBrushProperty, value); }
        }

        public static readonly DependencyProperty GlowBrushProperty =
            DependencyProperty.Register("GlowBrush", typeof(SolidColorBrush), typeof(Anchor), new PropertyMetadata(System.Windows.Media.Brushes.Transparent));


    }
}
