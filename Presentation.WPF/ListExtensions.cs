using Engine.Contracts;
using Presentation.WPF.Context;
using Presentation.WPF.Contracts;
using Presentation.WPF.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF
{
    public static class ListExtensions
    {
        public static List<IInfectionCardViewModel> GetInfectionCardViewModels(this IList<ICard> cardList)
        {
            List<IInfectionCardViewModel> infectionCardViewModels = new List<IInfectionCardViewModel>();
            foreach (ICard card in cardList)
            {
                if (card is IInfectionCard)
                    infectionCardViewModels.Add(new InfectionCardViewModel(new ObjectContext<IInfectionCard>() { Context = (IInfectionCard)card }));
            }
            return infectionCardViewModels;
        }

        public static void NotifyList<T>(this IList<T> viewModels) where T : IViewModelBase
        {
            foreach (IViewModelBase vm in viewModels)
            {
                vm.NotifyChanges();
            }
        }
    }
}
