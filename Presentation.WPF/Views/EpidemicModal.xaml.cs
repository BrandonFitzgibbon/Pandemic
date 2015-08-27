using Engine.Implementations;
using Presentation.WPF.Contracts;
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

namespace Presentation.WPF.Views
{
    /// <summary>
    /// Interaction logic for EpidemicModal.xaml
    /// </summary>
    public partial class EpidemicModal : UserControl
    {
        public EpidemicModal()
        {
            InitializeComponent();
        }

        public IEpidemicViewModel EpidemicViewModel
        {
            get { return (IEpidemicViewModel)GetValue(EpidemicViewModelProperty); }
            set { SetValue(EpidemicViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EpidemicViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EpidemicViewModelProperty =
            DependencyProperty.Register("EpidemicViewModel", typeof(IEpidemicViewModel), typeof(EpidemicModal), new PropertyMetadata(OnEpidemicViewModelChanged));       

        private static void OnEpidemicViewModelChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue != null)
            {
                EpidemicDialog ed = new EpidemicDialog();
                ed.DataContext = e.NewValue;
                ed.Owner = Application.Current.MainWindow;
                ed.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                ed.ResizeMode = ResizeMode.NoResize;
                ed.Width = ed.Owner.ActualWidth - 20;
                ed.ShowDialog();
            }
        }

    }
}
