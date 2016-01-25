using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Presentation.WPF.Converters
{
    public class DiseaseToBackgroundConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int count = (int)value;
            switch (count)
            {
                case 1:
                    return Brushes.Yellow;
                case 2:
                    return Brushes.Orange;
                case 3:
                    return Brushes.Red;
                default:
                    return Brushes.Green;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
