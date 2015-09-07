using Presentation.WPF.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Presentation.WPF.Converters
{
    public class RoleToPawnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string role = (string)value;
            Icons icons = new Icons();
            switch (role)
            {
                case "Dispatcher":
                    return icons["pawnPurple"];
                case "Medic":
                    return icons["pawnOrange"];
                case "Scientist":
                    return icons["pawnWhite"];
                case "Quarantine Specialist":
                    return icons["pawnDarkGreen"];
                case "Researcher":
                    return icons["pawnBrown"];
                case "Operations Expert":
                    return icons["pawnGreen"];
            }

            return icons["pawnWhite"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
