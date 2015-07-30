using Engine.Contracts;
using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class City : ICity
    {
        private string name;
        public string Name
        {
            get { return name; }
        }

        private string country;
        public string Country
        {
            get { return country; }
        }

        private int population;
        public int Population
        {
            get { return population; }
        }

        public City(string name, string country, int population)
        {
            this.name = name;
            this.country = country;
            this.population = population;
        }

        public override string ToString()
        {
            return name + ", " + country;
        }

        public override int GetHashCode()
        {
            return (name + country).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            ICity equalsTarget = (ICity)obj;
            if (equalsTarget != null)
                return equalsTarget.Name == this.name;
            else
                return false;
        }
    }
}
