using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.CustomEventArgs
{
    public class ResearchStationChangedEventArgs : EventArgs
    {
        private ICity newCity;
        public ICity NewCity
        {
            get { return newCity; }
        }

        private ICity oldCity;
        public ICity OldCity
        {
            get { return oldCity; }
        }

        public ResearchStationChangedEventArgs(ICity newCity, ICity oldCity)
        {
            this.newCity = newCity;
            this.oldCity = OldCity;
        }
    }
}
