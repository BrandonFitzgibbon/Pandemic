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
        public IList<ICity> Cities
        {
            get { return cities; }
            set { cities = value; NotifyPropertyChanged(); }
        }

        public BoardViewModel(IList<ICity> cities)
        {
            Cities = cities;
        }
    }
}
