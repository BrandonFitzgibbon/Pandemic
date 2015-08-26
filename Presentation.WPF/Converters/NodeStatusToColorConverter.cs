using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Presentation.WPF.Converters
{
    public class NodeStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Presentation.WPF.Implementations.NodeViewModel.NodeStatus status = (Implementations.NodeViewModel.NodeStatus)value;
            
            if (value == null)
                return System.Windows.Media.Brushes.White;

            switch(status)
            {
                case Implementations.NodeViewModel.NodeStatus.Yellow:
                    return System.Windows.Media.Brushes.Yellow;
                case Implementations.NodeViewModel.NodeStatus.Orange:
                    return System.Windows.Media.Brushes.Orange;
                case Implementations.NodeViewModel.NodeStatus.Red:
                    return System.Windows.Media.Brushes.Red;
            }

            return System.Windows.Media.Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
