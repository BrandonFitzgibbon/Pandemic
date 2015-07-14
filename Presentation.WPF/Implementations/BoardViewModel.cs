using Engine.Contracts;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Implementations
{
    public class BoardViewModel : ViewModelBase, IBoardViewModel
    {
        private IList<ICity> cities;

        public BoardViewModel(IList<ICity> cities)
        {
            this.cities = cities;
        }
    }
}
