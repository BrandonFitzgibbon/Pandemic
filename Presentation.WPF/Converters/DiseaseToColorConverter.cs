using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Presentation.WPF.Converters
{
    public class DiseaseToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IDisease disease = (IDisease)value;
            if (disease != null)
            {
                switch(disease.Name)
                {
                    case "Yellow":
                        return Brushes.Yellow;
                    case "Red":
                        return Brushes.Red;
                    case "Blue":
                        return Brushes.Blue;
                    case "Black":
                        return Brushes.Black;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
