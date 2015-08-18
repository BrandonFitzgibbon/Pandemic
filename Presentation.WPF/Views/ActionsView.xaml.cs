using Engine.Implementations;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentation.WPF.Views
{
    /// <summary>
    /// Interaction logic for ActionsView.xaml
    /// </summary>
    public partial class ActionsView : UserControl
    {
        private TabControl tc;

        public ActionsView()
        {
            InitializeComponent();
        }

        private void TabControl_Loaded(object sender, RoutedEventArgs e)
        {
            TabControl tabControl = (TabControl)sender;
            if (tabControl != null)
            {
                tc = tabControl;

            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActionsViewModel avm = DataContext as ActionsViewModel;
            if (avm.SelectedCureTarget != null)
            {
                ListBox lb = sender as ListBox;
                avm.SelectedCureTarget.Cards.Clear();
                foreach (CityCard card in lb.SelectedItems)
                {
                    avm.SelectedCureTarget.Cards.Add(card);
                }
            }
        }
    }
}
