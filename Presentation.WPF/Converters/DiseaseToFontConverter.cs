using Engine.Contracts;
using Engine.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Presentation.WPF.Converters
{
    public class DiseaseToFontConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Disease disease = (Disease)value;
            if (disease != null)
            {
                switch (disease.Type)
                {
                    case DiseaseType.Yellow:
                        return Brushes.Black;
                    case DiseaseType.Red:
                        return Brushes.White;
                    case DiseaseType.Blue:
                        return Brushes.White;
                    case DiseaseType.Black:
                        return Brushes.White;
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
