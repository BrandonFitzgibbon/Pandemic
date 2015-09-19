using Engine.Implementations;
using Presentation.WPF.Implementations;
using System;
using System.Threading.Tasks;
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
    [TemplatePart(Name = "PART_Content", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_TopContent", Type = typeof(FrameworkElement))]

    public class Anchor : Control
    {
        private Border glowBorder;
        private Border contentBorder;
        private ContentPresenter topContent;

        static Anchor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Anchor), new FrameworkPropertyMetadata(typeof(Anchor)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            glowBorder = (Border)Template.FindName("PART_GlowBorder", this);
            contentBorder = (Border)Template.FindName("PART_ContentBorder", this);
            topContent = (ContentPresenter)Template.FindName("PART_TopContent", this);

            glowBorder.Padding = new Thickness(1.1 * contentBorder.Padding.Left, 1.1 * contentBorder.Padding.Top, 1.1 * contentBorder.Padding.Right, 1.1 * contentBorder.Padding.Bottom);

            contentBorder.MouseRightButtonUp += ContentMouseRightButtonUp;
            contentBorder.MouseEnter += ContentBorder_MouseEnter;
            contentBorder.MouseLeave += ContentBorder_MouseLeave;

            topContent.MouseLeftButtonUp += TopContentMouseLeftButtonUp;

        }

        private void ContentBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            if (MouseEnterCommand != null)
                MouseEnterCommand.Execute(null);
        }

        private void ContentBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            if (MouseLeaveCommand != null)
                MouseLeaveCommand.Execute(null);
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



        public double ContentOpacity
        {
            get { return (double)GetValue(ContentOpacityProperty); }
            set { SetValue(ContentOpacityProperty, value); }
        }

        public static readonly DependencyProperty ContentOpacityProperty =
            DependencyProperty.Register("ContentOpacity", typeof(double), typeof(Anchor), new PropertyMetadata(1.0));



        public object LeftContent
        {
            get { return (object)GetValue(LeftContentProperty); }
            set { SetValue(LeftContentProperty, value); }
        }

        public static readonly DependencyProperty LeftContentProperty =
            DependencyProperty.Register("LeftContent", typeof(object), typeof(Anchor), new PropertyMetadata());



        public DataTemplate LeftContentTemplate
        {
            get { return (DataTemplate)GetValue(LeftContentTemplateProperty); }
            set { SetValue(LeftContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty LeftContentTemplateProperty =
            DependencyProperty.Register("LeftContentTemplate", typeof(DataTemplate), typeof(Anchor), new PropertyMetadata());



        public object TopContent
        {
            get { return (object)GetValue(TopContentProperty); }
            set { SetValue(TopContentProperty, value); }
        }

        public static readonly DependencyProperty TopContentProperty =
            DependencyProperty.Register("TopContent", typeof(object), typeof(Anchor), new PropertyMetadata());



        public DataTemplate TopContentTemplate
        {
            get { return (DataTemplate)GetValue(TopContentTemplateProperty); }
            set { SetValue(TopContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty TopContentTemplateProperty =
            DependencyProperty.Register("TopContentTemplate", typeof(DataTemplate), typeof(Anchor), new PropertyMetadata());



        public ICommand TopCommand
        {
            get { return (ICommand)GetValue(TopCommandProperty); }
            set { SetValue(TopCommandProperty, value); }
        }

        public static readonly DependencyProperty TopCommandProperty =
            DependencyProperty.Register("TopCommand", typeof(ICommand), typeof(Anchor), new PropertyMetadata());



        private void TopContentMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (topContent.IsMouseOver && TopCommand != null)
            {
                TopCommand.Execute(null);
            }
        }



        public object RightContent
        {
            get { return (object)GetValue(RightContentProperty); }
            set { SetValue(RightContentProperty, value); }
        }

        public static readonly DependencyProperty RightContentProperty =
            DependencyProperty.Register("RightContent", typeof(object), typeof(Anchor), new PropertyMetadata());



        public DataTemplate RightContentTemplate
        {
            get { return (DataTemplate)GetValue(RightContentTemplateProperty); }
            set { SetValue(RightContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty RightContentTemplateProperty =
            DependencyProperty.Register("RightContentTemplate", typeof(DataTemplate), typeof(Anchor), new PropertyMetadata());



        public object BottomContent
        {
            get { return (object)GetValue(BottomContentProperty); }
            set { SetValue(BottomContentProperty, value); }
        }

        public static readonly DependencyProperty BottomContentProperty =
            DependencyProperty.Register("BottomContent", typeof(object), typeof(Anchor), new PropertyMetadata());



        public DataTemplate BottomContentTemplate
        {
            get { return (DataTemplate)GetValue(BottomContentTemplateProperty); }
            set { SetValue(BottomContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty BottomContentTemplateProperty =
            DependencyProperty.Register("BottomContentTemplate", typeof(DataTemplate), typeof(Anchor), new PropertyMetadata());



        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(Anchor), new PropertyMetadata());



        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty ContentTemplateProperty =
            DependencyProperty.Register("ContentTemplate", typeof(DataTemplate), typeof(Anchor), new PropertyMetadata());



        public bool ContentEnabled
        {
            get { return (bool)GetValue(ContentEnabledProperty); }
            set { SetValue(ContentEnabledProperty, value); }
        }

        public static readonly DependencyProperty ContentEnabledProperty =
            DependencyProperty.Register("ContentEnabled", typeof(bool), typeof(Anchor), new PropertyMetadata(true));



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

        internal void Command_CanExecuteChanged(object sender, EventArgs e)
        {
            if (Command != null && !Command.CanExecute(CommandParameter))
            {
                contentBorder.IsEnabled = false;
                glowBorder.IsEnabled = false;
                ContentEnabled = false;
            }
            else
            {
                contentBorder.IsEnabled = true;
                glowBorder.IsEnabled = true;
                ContentEnabled = true;
            }
        }



        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(Anchor), new PropertyMetadata());


        private void ContentMouseRightButtonUp(object sender, MouseEventArgs e)
        {
            if (IsMouseOver && Command != null)
            {
                Command.Execute(CommandParameter);
            }
        }



        public ICommand MouseEnterCommand
        {
            get { return (ICommand)GetValue(MouseEnterCommandProperty); }
            set { SetValue(MouseEnterCommandProperty, value); }
        }

        public static readonly DependencyProperty MouseEnterCommandProperty =
            DependencyProperty.Register("MouseEnterCommand", typeof(ICommand), typeof(Anchor), new PropertyMetadata());


        public ICommand MouseLeaveCommand
        {
            get { return (ICommand)GetValue(MouseLeaveCommandProperty); }
            set { SetValue(MouseLeaveCommandProperty, value); }
        }

        public static readonly DependencyProperty MouseLeaveCommandProperty =
            DependencyProperty.Register("MouseLeaveCommand", typeof(ICommand), typeof(Anchor), new PropertyMetadata());

    }
}
