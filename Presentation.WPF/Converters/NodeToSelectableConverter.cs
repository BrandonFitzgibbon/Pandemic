using Engine.Implementations;
using Engine.Implementations.ActionItems;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Presentation.WPF.Converters
{
    public class NodeToSelectableConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            Node node = (Node)values[0];
            IEnumerable<DriveDestinationItem> ddi = (IEnumerable<DriveDestinationItem>)values[1];

            return ddi.Where(i => i.Node == node).Count() == 1;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
