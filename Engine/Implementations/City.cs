using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class City : ICity
    {
        private int id;
        public int Id
        {
            get { return id; }
        }

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

        private IList<ICity> connections;
        public IEnumerable<ICity> Connections
        {
            get { return connections; }
        }

        private IDisease disease;
        public IDisease Disease
        {
            get { return disease; }
        }

        private IList<ICounter> counters;
        public IList<ICounter> Counters
        {
            get { return counters; }
        }

        private IList<IPlayer> players;
        public IList<IPlayer> Players
        {
            get { return players; }
        }

        private bool hasResearchStation;
        public bool HasResearchStation
        {
            get { return hasResearchStation; }
        }

        public City(int id, string name, string country, int population, IDisease disease, IList<IDisease> diseases)
        {
            this.id = id;
            this.name = name;
            this.country = country;
            this.population = population;
            this.disease = disease;
            CreateCounters(diseases);
            this.players = new List<IPlayer>();
            hasResearchStation = false;
        }

        private void CreateCounters(IList<IDisease> diseases)
        {
            counters = new List<ICounter>();
            foreach (IDisease disease in diseases)
	        {
                counters.Add(new Counter(disease));
	        }
        }

        public override string ToString()
        {
            return name + ", " + country;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            ICity equalsTarget = (ICity)obj;
            if (equalsTarget != null)
                return equalsTarget.Id == this.id;
            else
                return false;
        }
    }
}
