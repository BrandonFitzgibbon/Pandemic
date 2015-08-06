using Engine.Contracts;
using Engine.Implementations;
using Presentation.WPF.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Presentation.WPF.Converters
{
    public class DiseaseStatusToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;
            Disease disease = (Disease)value;
            Icons icons = new Icons();
            if(disease.IsCured)
            {
                switch (disease.Type)
                {
                    case DiseaseType.Yellow:
                        return icons["curedYellow"];
                    case DiseaseType.Red:
                        return icons["curedRed"];
                    case DiseaseType.Blue:
                        return icons["curedBlue"];
                    case DiseaseType.Black:
                        return icons["curedBlack"];
                }
            }
            else if(disease.IsEradicated)
            {
                switch (disease.Type)
                {
                    case DiseaseType.Yellow:
                        return icons["eradicatedYellow"];
                    case DiseaseType.Red:
                        return icons["eradicatedRed"];
                    case DiseaseType.Blue:
                        return icons["eradicatedBlue"];
                    case DiseaseType.Black:
                        return icons["eradicatedBlack"];
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
