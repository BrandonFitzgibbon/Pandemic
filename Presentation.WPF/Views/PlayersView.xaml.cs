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
    /// Interaction logic for PlayersView.xaml
    /// </summary>
    public partial class PlayersView : UserControl
    {

        private TabControl tc;

        public PlayersView()
        {
            InitializeComponent();
        }

        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            if (tc != null)
            {
                if (tc.SelectedIndex == 0)
                    tc.SelectedIndex = tc.Items.Count - 1;
                else
                    tc.SelectedIndex = tc.SelectedIndex - 1;
            }
        }

        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            if (tc != null)
            {
                if (tc.SelectedIndex == tc.Items.Count - 1)
                    tc.SelectedIndex = 0;
                else
                    tc.SelectedIndex = tc.SelectedIndex + 1;
            }
        }

        private void TabControl_Loaded(object sender, RoutedEventArgs e)
        {
            TabControl tabControl = (TabControl)sender;
            if (tabControl != null)
            {
                tc = tabControl;
                
            }
        }
    }
}
