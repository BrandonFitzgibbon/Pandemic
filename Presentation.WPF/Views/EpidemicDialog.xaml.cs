using Presentation.WPF.Implementations;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Presentation.WPF.Views
{
    /// <summary>
    /// Interaction logic for EpidemicDialog.xaml
    /// </summary>
    public partial class EpidemicDialog : Window
    {
        public EpidemicDialog()
        {
            InitializeComponent();
        }

        public static bool GetIsOpen(DependencyObject o)
        {
            return (bool)o.GetValue(IsOpenProperty);
        }

        public static void SetIsOpen(DependencyObject o, bool value)
        {
            o.SetValue(IsOpenProperty, value);
        }

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.RegisterAttached("IsOpen", typeof(bool), typeof(EpidemicDialog), new PropertyMetadata(true, new PropertyChangedCallback(OnIsOpenChanged)));

        public static void OnIsOpenChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == false)
            {
                EpidemicDialog ed = (EpidemicDialog)o;
                ed.Owner.Opacity = 1;
                ed.Close();
            }
        }

        public static bool GetIsLoaded(DependencyObject o)
        {
            return (bool)o.GetValue(IsLoadedProperty);
        }

        public static void SetIsLoaded(DependencyObject o, bool value)
        {
            o.SetValue(IsLoadedProperty, value);
        }

        public static readonly DependencyProperty IsLoadedProperty =
            DependencyProperty.RegisterAttached("IsLoaded", typeof(bool), typeof(EpidemicDialog), new PropertyMetadata(false));

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EpidemicDialog ed = (EpidemicDialog)sender;
            ed.SetValue(IsLoadedProperty, true);
        }
    }
}
