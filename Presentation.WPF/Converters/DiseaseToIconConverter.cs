using Engine.Contracts;
using Presentation.WPF.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Presentation.WPF.Converters
{
    public class DiseaseToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IDisease disease = (IDisease)value;
            Icons icons = new Icons();
            switch(disease.Type)
            {
                case DiseaseType.Yellow:
                    return icons["compoundYellow"];
                case DiseaseType.Red:
                    return icons["compoundRed"];
                case DiseaseType.Blue:
                    return icons["compoundBlue"];
                case DiseaseType.Black:
                    return icons["compoundBlack"];
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
