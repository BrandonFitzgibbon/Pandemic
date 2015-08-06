using Engine.Contracts;
using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class City
    {
        public string Name { get; private set; }
        public string Country { get; private set; }
        public int Population { get; private set; }

        public City(string name, string country, int population)
        {
            Name = name;
            Country = country;
            Population = population;
        }

        public override string ToString()
        {
            return Name + ", " + Country;
        }

        public override int GetHashCode()
        {
            return (Name + Country).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            City equalsTarget = (City)obj;
            return equalsTarget != null ? equalsTarget.Name == Name : false;
        }
    }
}
