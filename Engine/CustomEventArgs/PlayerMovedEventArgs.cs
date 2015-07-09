using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.CustomEventArgs
{
    public class PlayerMovedEventArgs : EventArgs
    {
        private ICity departedCity;
        public ICity DepartedCity
        {
            get { return departedCity; }
        }

        private ICity arrivedCity;
        public ICity ArrivedCity
        {
            get { return arrivedCity; }
        }

        public PlayerMovedEventArgs(ICity departedCity, ICity arrivedCity)
        {
            this.departedCity = departedCity;
            this.arrivedCity = arrivedCity;
        }
    }
}
