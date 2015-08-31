using System;
using System.Globalization;
using System.Windows.Data;

namespace Presentation.WPF.Converters
{
    public class RoleToOperationsExpertConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string role = (string)value;
            if (role == "Operations Expert")
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
