using Engine.Implementations;
using Presentation.WPF.Implementations;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Presentation.WPF.Controls
{
    [TemplatePart(Name = "PART_GlowBorder", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_ContentBorder", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_CenterContent", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_UpperLeftContent", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_UpperRightContent", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_BottomLeftContent", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_BottomRightContent", Type = typeof(FrameworkElement))]
    public class Anchor : Control
    {
        private Border glowBorder;
        private Border contentBorder;

        private DoubleAnimation opacityAnimation;

        static Anchor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Anchor), new FrameworkPropertyMetadata(typeof(Anchor)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            opacityAnimation = new DoubleAnimation();

            glowBorder = (Border)Template.FindName("PART_GlowBorder", this);
            contentBorder = (Border)Template.FindName("PART_ContentBorder", this);

            glowBorder.Padding = new Thickness(1.1 * contentBorder.Padding.Left, 1.1 * contentBorder.Padding.Top, 1.1 * contentBorder.Padding.Right, 1.1 * contentBorder.Padding.Bottom);

            contentBorder.MouseRightButtonUp += ContentMouseRightButtonUp;
            contentBorder.MouseEnter += ContentMouseEnter;
            contentBorder.MouseLeave += ContentMouseLeave;

            ContentPresenter upperLeftContentPresenter = (ContentPresenter)Template.FindName("PART_UpperLeftContent", this);
            upperLeftContentPresenter.MouseLeftButtonDown += UpperLeftContentPresenter_MouseLeftButtonDown;

            ContentPresenter upperRightContentPresenter = (ContentPresenter)Template.FindName("PART_UpperRightContent", this);
            upperRightContentPresenter.MouseLeftButtonDown += UpperRightContentPresenter_MouseLeftButtonDown;

            ContentPresenter bottomLeftContentPresenter = (ContentPresenter)Template.FindName("PART_BottomLeftContent", this);
            bottomLeftContentPresenter.MouseLeftButtonDown += BottomLeftContentPresenter_MouseLeftButtonDown;

            ContentPresenter bottomRightContentPresenter = (ContentPresenter)Template.FindName("PART_BottomRightContent", this);
            bottomRightContentPresenter.MouseLeftButtonDown += BottomRightContentPresenter_MouseLeftButtonDown;
        }

        private void ContentMouseLeave(object sender, MouseEventArgs e)
        {
            MouseOverItem = null;
        }

        private void ContentMouseEnter(object sender, MouseEventArgs e)
        {
            MouseOverItem = DataContext;
        }

        public object MouseOverItem
        {
            get { return (object)GetValue(MouseOverItemProperty); }
            set { SetValue(MouseOverItemProperty, value); }
        }

        public static readonly DependencyProperty MouseOverItemProperty =
            DependencyProperty.Register("MouseOverItem", typeof(object), typeof(Anchor), new PropertyMetadata());

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

        #region ContentDP

        public object UpperLeftContent
        {
            get { return (object)GetValue(UpperLeftContentProperty); }
            set { SetValue(UpperLeftContentProperty, value); }
        }

        public static readonly DependencyProperty UpperLeftContentProperty =
            DependencyProperty.Register("UpperLeftContent", typeof(object), typeof(Anchor), new PropertyMetadata());


        public DataTemplate UpperLeftContentTemplate
        {
            get { return (DataTemplate)GetValue(UpperLeftContentTemplateProperty); }
            set { SetValue(UpperLeftContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty UpperLeftContentTemplateProperty =
            DependencyProperty.Register("UpperLeftContentTemplate", typeof(DataTemplate), typeof(Anchor), new PropertyMetadata());

        public object UpperRightContent
        {
            get { return (object)GetValue(UpperRightContentProperty); }
            set { SetValue(UpperRightContentProperty, value); }
        }

        public static readonly DependencyProperty UpperRightContentProperty =
            DependencyProperty.Register("UpperRightContent", typeof(object), typeof(Anchor), new PropertyMetadata());

        public DataTemplate UpperRightContentTemplate
        {
            get { return (DataTemplate)GetValue(UpperRightContentTemplateProperty); }
            set { SetValue(UpperRightContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty UpperRightContentTemplateProperty =
            DependencyProperty.Register("UpperRightContentTemplate", typeof(DataTemplate), typeof(Anchor), new PropertyMetadata());

        public object BottomLeftContent
        {
            get { return (object)GetValue(BottomLeftContentProperty); }
            set { SetValue(BottomLeftContentProperty, value); }
        }

        public static readonly DependencyProperty BottomLeftContentProperty =
            DependencyProperty.Register("BottomLeftContent", typeof(object), typeof(Anchor), new PropertyMetadata());

        public DataTemplate BottomLeftContentTemplate
        {
            get { return (DataTemplate)GetValue(BottomLeftContentTemplateProperty); }
            set { SetValue(BottomLeftContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty BottomLeftContentTemplateProperty =
            DependencyProperty.Register("BottomLeftContentTemplate", typeof(DataTemplate), typeof(Anchor), new PropertyMetadata());

        public object BottomRightContent
        {
            get { return (object)GetValue(BottomRightContentProperty); }
            set { SetValue(BottomRightContentProperty, value); }
        }

        public static readonly DependencyProperty BottomRightContentProperty =
            DependencyProperty.Register("BottomRightContent", typeof(object), typeof(Anchor), new PropertyMetadata());

        public DataTemplate BottomRightContentTemplate
        {
            get { return (DataTemplate)GetValue(BottomRightContentTemplateProperty); }
            set { SetValue(BottomRightContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty BottomRightContentTemplateProperty =
            DependencyProperty.Register("BottomRightContentTemplate", typeof(DataTemplate), typeof(Anchor), new PropertyMetadata());



        public object CenterContent
        {
            get { return (object)GetValue(CenterContentProperty); }
            set { SetValue(CenterContentProperty, value); }
        }

        public static readonly DependencyProperty CenterContentProperty =
            DependencyProperty.Register("CenterContent", typeof(object), typeof(Anchor), new PropertyMetadata());



        public DataTemplate CenterContentTemplate
        {
            get { return (DataTemplate)GetValue(CenterContentTemplateProperty); }
            set { SetValue(CenterContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty CenterContentTemplateProperty =
            DependencyProperty.Register("CenterContentTemplate", typeof(DataTemplate), typeof(Anchor), new PropertyMetadata());



        public bool CenterContentEnabled
        {
            get { return (bool)GetValue(CenterContentEnabledProperty); }
            set { SetValue(CenterContentEnabledProperty, value); }
        }

        public static readonly DependencyProperty CenterContentEnabledProperty =
            DependencyProperty.Register("CenterContentEnabled", typeof(bool), typeof(Anchor), new PropertyMetadata(true));



        public object TopCenterContent
        {
            get { return (object)GetValue(TopCenterContentProperty); }
            set { SetValue(TopCenterContentProperty, value); }
        }

        public static readonly DependencyProperty TopCenterContentProperty =
            DependencyProperty.Register("TopCenterContent", typeof(object), typeof(Anchor), new PropertyMetadata());

        public DataTemplate TopCenterContentTemplate
        {
            get { return (DataTemplate)GetValue(TopCenterContentTemplateProperty); }
            set { SetValue(TopCenterContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty TopCenterContentTemplateProperty =
            DependencyProperty.Register("TopCenterContentTemplate", typeof(DataTemplate), typeof(Anchor), new PropertyMetadata());

        public object BottomCenterContent
        {
            get { return (object)GetValue(BottomCenterContentProperty); }
            set { SetValue(BottomCenterContentProperty, value); }
        }

        public static readonly DependencyProperty BottomCenterContentProperty =
            DependencyProperty.Register("BottomCenterContent", typeof(object), typeof(Anchor), new PropertyMetadata());

        public DataTemplate BottomCenterContentTemplate
        {
            get { return (DataTemplate)GetValue(BottomCenterContentTemplateProperty); }
            set { SetValue(BottomCenterContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty BottomCenterContentTemplateProperty =
            DependencyProperty.Register("BottomCenterContentTemplate", typeof(DataTemplate), typeof(Anchor), new PropertyMetadata());

        #endregion

        #region Commands

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(Anchor), new PropertyMetadata(new PropertyChangedCallback(CommandChanged)));

        public static void CommandChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Anchor a = o as Anchor;
            ICommand command = e.NewValue as ICommand;
            if (command != null && o != null)
            {
                command.CanExecuteChanged += a.Command_CanExecuteChanged;
            }
        }

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(Anchor), new PropertyMetadata());

        internal void Command_CanExecuteChanged(object sender, EventArgs e)
        {
            if(Command.CanExecute(CommandParameter))
            {
                contentBorder.IsEnabled = true;
                glowBorder.IsEnabled = true;
                CenterContentEnabled = true;
            }
            else
            {
                contentBorder.IsEnabled = false;
                glowBorder.IsEnabled = false;
                CenterContentEnabled = false;
            }
        }

        private void ContentMouseRightButtonUp(object sender, MouseEventArgs e)
        {
            if (IsMouseOver && Command != null)
            {
                Command.Execute(CommandParameter);
            }
        }

        public ICommand UpperLeftCommand
        {
            get { return (ICommand)GetValue(UpperLeftCommandProperty); }
            set { SetValue(UpperLeftCommandProperty, value); }
        }

        public static readonly DependencyProperty UpperLeftCommandProperty =
            DependencyProperty.Register("UpperLeftCommand", typeof(ICommand), typeof(Anchor), new PropertyMetadata());

        public object UpperLeftCommandParameter
        {
            get { return (object)GetValue(UpperLeftCommandParameterProperty); }
            set { SetValue(UpperLeftCommandParameterProperty, value); }
        }

        public static readonly DependencyProperty UpperLeftCommandParameterProperty =
            DependencyProperty.Register("UpperLeftCommandParameter", typeof(object), typeof(Anchor), new PropertyMetadata());

        private void UpperLeftContentPresenter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContentPresenter cp = (ContentPresenter)sender;
            if (cp.IsMouseOver && UpperLeftCommand != null)
            {
                UpperLeftCommand.Execute(UpperLeftCommandParameter);
            }
        }

        public ICommand UpperRightCommand
        {
            get { return (ICommand)GetValue(UpperRightCommandProperty); }
            set { SetValue(UpperRightCommandProperty, value); }
        }

        public static readonly DependencyProperty UpperRightCommandProperty =
            DependencyProperty.Register("UpperRightCommand", typeof(ICommand), typeof(Anchor), new PropertyMetadata());

        public object UpperRightCommandParameter
        {
            get { return (object)GetValue(UpperRightCommandParameterProperty); }
            set { SetValue(UpperRightCommandParameterProperty, value); }
        }

        public static readonly DependencyProperty UpperRightCommandParameterProperty =
            DependencyProperty.Register("UpperRightCommandParameter", typeof(object), typeof(Anchor), new PropertyMetadata());

        private void UpperRightContentPresenter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContentPresenter cp = (ContentPresenter)sender;
            if (cp.IsMouseOver && UpperRightCommand != null)
            {
                UpperRightCommand.Execute(UpperRightCommandParameter);
            }
        }

        public ICommand BottomLeftCommand
        {
            get { return (ICommand)GetValue(BottomLeftCommandProperty); }
            set { SetValue(BottomLeftCommandProperty, value); }
        }

        public static readonly DependencyProperty BottomLeftCommandProperty =
            DependencyProperty.Register("BottomLeftCommand", typeof(ICommand), typeof(Anchor), new PropertyMetadata());

        public object BottomLeftCommandParameter
        {
            get { return (object)GetValue(BottomLeftCommandParameterProperty); }
            set { SetValue(BottomLeftCommandParameterProperty, value); }
        }

        public static readonly DependencyProperty BottomLeftCommandParameterProperty =
            DependencyProperty.Register("BottomLeftCommandParameter", typeof(object), typeof(Anchor), new PropertyMetadata());

        private void BottomLeftContentPresenter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContentPresenter cp = (ContentPresenter)sender;
            if (cp.IsMouseOver && BottomLeftCommand != null)
            {
                BottomLeftCommand.Execute(BottomLeftCommandParameter);
            }
        }

        public ICommand BottomRightCommand
        {
            get { return (ICommand)GetValue(BottomRightCommandProperty); }
            set { SetValue(BottomRightCommandProperty, value); }
        }

        public static readonly DependencyProperty BottomRightCommandProperty =
            DependencyProperty.Register("BottomRightCommand", typeof(ICommand), typeof(Anchor), new PropertyMetadata());

        public object BottomRightCommandParameter
        {
            get { return (object)GetValue(BottomRightCommandParameterProperty); }
            set { SetValue(BottomRightCommandParameterProperty, value); }
        }

        public static readonly DependencyProperty BottomRightCommandParameterProperty =
            DependencyProperty.Register("BottomRightCommandParameter", typeof(object), typeof(Anchor), new PropertyMetadata());

        private void BottomRightContentPresenter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContentPresenter cp = (ContentPresenter)sender;
            if (cp.IsMouseOver && BottomRightCommand != null)
            {
                BottomRightCommand.Execute(BottomRightCommandParameter);
            }
        }

        #endregion

        public bool FlashingOnMouseOver
        {
            get { return (bool)GetValue(FlashingOnMouseOverProperty); }
            set { SetValue(FlashingOnMouseOverProperty, value); }
        }

        public static readonly DependencyProperty FlashingOnMouseOverProperty =
            DependencyProperty.Register("FlashingOnMouseOver", typeof(bool), typeof(Anchor), new PropertyMetadata(false, new PropertyChangedCallback(OnFlashingOnMouseOverChange)));

        public static void OnFlashingOnMouseOverChange(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Anchor anchor = (Anchor)o;
            if ((bool)e.NewValue)
            {
                anchor.contentBorder.MouseEnter += anchor.FlashingEnter;
                anchor.contentBorder.MouseLeave += anchor.FlashingLeave;               
            }
            else
            {
                anchor.contentBorder.MouseEnter -= anchor.FlashingEnter;
                anchor.contentBorder.MouseLeave -= anchor.FlashingLeave;
            }
        }

        private Path p;

        private void FlashingEnter(object sender, MouseEventArgs e)
        {
            opacityAnimation.From = contentBorder.Opacity;
            opacityAnimation.To = 0;
            opacityAnimation.RepeatBehavior = RepeatBehavior.Forever;
            opacityAnimation.AutoReverse = true;
            opacityAnimation.Duration = TimeSpan.FromMilliseconds(750);
            contentBorder.BeginAnimation(Border.OpacityProperty, opacityAnimation);

            Canvas canvas = (Canvas)Parent;
            NodeViewModel nvm = (NodeViewModel)DataContext;

            foreach (Anchor child in canvas.Children)
            {
                NodeViewModel cvm = (NodeViewModel)child.DataContext;
                if (cvm.Node == nvm.BoardViewModel.SelectedPlayerViewModel.Location)
                {
                    Point origin = new Point(Canvas.GetLeft(child), Canvas.GetTop(child));
                    Point dest = new Point(Canvas.GetLeft(this), Canvas.GetTop(this));
                    Point bez1;
                    Point bez2;

                    if (Math.Abs(dest.X - origin.X) > Math.Abs(dest.Y - origin.Y))
                    {
                        bez1 = new Point(origin.X + 0.4 * (dest.X - origin.X), origin.Y + 0.5 * (dest.Y - origin.Y));
                        bez2 = new Point(origin.X + 0.6 * (dest.X - origin.X), origin.Y + -0.5 * (dest.Y - origin.Y));
                    }
                    else
                    {
                        bez1 = new Point(origin.X + 0.5 * (dest.X - origin.X), origin.Y + 0.4 * (dest.Y - origin.Y));
                        bez2 = new Point(origin.X + -0.5 * (dest.X - origin.X), origin.Y + 0.6 * (dest.Y - origin.Y));
                    }

                    p = new Path() { Stroke = Brushes.White, StrokeThickness = 4, StrokeDashArray = new DoubleCollection() { 5 } };
                    PathGeometry pg = new PathGeometry();
                    PathFigure pf = new PathFigure() { StartPoint = origin };
                    BezierSegment bezSegment = new BezierSegment(bez1, bez2, dest, true);
                    pf.Segments.Add(bezSegment);
                    pg.Figures.Add(pf);
                    p.Data = pg;

                    p.BeginAnimation(Path.OpacityProperty, opacityAnimation);

                    break;
                }
            }

            canvas.Children.Add(p);
        }

        private void FlashingLeave(object sender, MouseEventArgs e)
        {
            contentBorder.BeginAnimation(Border.OpacityProperty, null);
            Canvas canvas = (Canvas)Parent;
            canvas.Children.Remove(p);
        }
    }
}
