using Engine.Implementations;
using Presentation.WPF.Contracts;
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
    public class PlayerInNodeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            Node node = (Node)values[0];
            IBoardViewModel bvm = (IBoardViewModel)values[1];
            int turn = int.Parse(parameter.ToString());

            if (bvm.CurrentPlayerViewModel.TurnOrder != turn && node.Players.Contains(bvm.PlayerViewModels.SingleOrDefault(i => i.TurnOrder == turn).Player))
            {
                return bvm.PlayerViewModels.SingleOrDefault(i => i.TurnOrder == turn).Pawn;
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
