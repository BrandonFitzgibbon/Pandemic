using Engine.Implementations;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentation.WPF.Views
{
    /// <summary>
    /// Interaction logic for EpidemicModal.xaml
    /// </summary>
    public partial class DialogController : UserControl
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(HandleRef hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        public DialogController()
        {
            InitializeComponent();
        }

        public IEpidemicViewModel EpidemicViewModel
        {
            get { return (IEpidemicViewModel)GetValue(EpidemicViewModelProperty); }
            set { SetValue(EpidemicViewModelProperty, value); }
        }

        public static readonly DependencyProperty EpidemicViewModelProperty =
            DependencyProperty.Register("EpidemicViewModel", typeof(IEpidemicViewModel), typeof(DialogController), new PropertyMetadata(OnEpidemicViewModelChanged));       

        private static void OnEpidemicViewModelChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue != null)
            {
                EpidemicDialog ed = new EpidemicDialog();
                ed.DataContext = e.NewValue;
                ed.Owner = Application.Current.MainWindow;
                ed.Width = 0;
                ed.Height = 300;
                ed.ResizeMode = ResizeMode.NoResize;
                ed.WindowStartupLocation = WindowStartupLocation.Manual;

                RECT rct;

                if (!GetWindowRect(new HandleRef(ed.Owner, new WindowInteropHelper(ed.Owner).Handle), out rct))
                {
                    MessageBox.Show("ERROR");
                    return;
                }

                ed.Left = rct.Left + 10;
                ed.Top = rct.Top + ed.Owner.ActualHeight / 2 - ed.Height / 2;

                DoubleAnimation widthAnimation = new DoubleAnimation(0, ed.Owner.ActualWidth - 20, new Duration(new TimeSpan(0, 0, 0, 0, 250)));
                ed.BeginAnimation(Window.WidthProperty, widthAnimation);
                ed.Owner.Opacity = 0.5;
                ed.ShowDialog();
            }
        }

        private DiscardDialog currentDiscardDialog;

        public IDiscardViewModel DiscardViewModel
        {
            get { return (IDiscardViewModel)GetValue(DiscardViewModelProperty); }
            set { SetValue(DiscardViewModelProperty, value); }
        }

        public static readonly DependencyProperty DiscardViewModelProperty =
            DependencyProperty.Register("DiscardViewModel", typeof(IDiscardViewModel), typeof(DialogController), new PropertyMetadata(new PropertyChangedCallback(OnDiscardViewModelChanged)));

        public static void OnDiscardViewModelChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            DialogController dc = (DialogController)o;

            if (e.NewValue != null)
            {
                DiscardDialog dd = new DiscardDialog();
                dd.DataContext = e.NewValue;
                dd.Owner = Application.Current.MainWindow;
                dd.Width = 0;
                dd.Height = 300;
                dd.ResizeMode = ResizeMode.NoResize;
                dd.WindowStartupLocation = WindowStartupLocation.Manual;

                RECT rct;

                if (!GetWindowRect(new HandleRef(dd.Owner, new WindowInteropHelper(dd.Owner).Handle), out rct))
                {
                    MessageBox.Show("ERROR");
                    return;
                }

                dd.Left = rct.Left + 10;
                dd.Top = rct.Top + dd.Owner.ActualHeight / 2 - dd.Height / 2;

                DoubleAnimation widthAnimation = new DoubleAnimation(0, dd.Owner.ActualWidth - 20, new Duration(new TimeSpan(0, 0, 0, 0, 250)));
                dd.BeginAnimation(Window.WidthProperty, widthAnimation);
                dc.currentDiscardDialog = dd;
                dd.Owner.Opacity = 0.50;
                dd.ShowDialog();
            }
            else if(e.NewValue == null && dc.currentDiscardDialog != null)
            {
                dc.currentDiscardDialog.Owner.Opacity = 1;
                dc.currentDiscardDialog.Close();
            }
        }

    }
}
