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

        private List<ICity> connections;
        public IEnumerable<ICity> Connections
        {
            get { return connections; }
        }

        private IDisease disease;
        public IDisease Disease
        {
            get { return disease; }
        }

        private IEnumerable<IDiseaseCounter> counters;
        public IEnumerable<IDiseaseCounter> Counters
        {
            get { return counters; }
        }

        private List<IPlayer> playersInCity;
        public IEnumerable<IPlayer> PlayersInCity
        {
            get { return playersInCity; }
        }

        private bool hasResearchStation;
        public bool HasResearchStation
        {
            get { return hasResearchStation; }
        }

        public City(string name, string country, int population, IDisease disease, IDiseaseCounterFactory counterFactory)
        {
            this.name = name;
            this.country = country;
            this.population = population;
            this.disease = disease;
            this.counters = counterFactory.GetCounters(this);
            this.connections = new List<ICity>();
            this.playersInCity = new List<IPlayer>();
            hasResearchStation = false;
        }

        public void Subscribe(IPlayer player)
        {
            player.Moved += playerMoved;
        }

        public void Connect(ICity city)
        {
            this.connections.Add(city);
        }

        private void playerResearchStationChanged(object sender, ResearchStationChangedEventArgs e)
        {
            IPlayer player = (IPlayer)sender;
            if (player != null)
            {
                if (e.OldCity == this)
                    hasResearchStation = false;
                if (e.NewCity == this)
                    hasResearchStation = true;
            }
        }

        private void playerMoved(object sender, PlayerMovedEventArgs e)
        {
            IPlayer player = (IPlayer)sender;
            if (player != null)
            {
                if (e.DepartedCity == this)
                    playersInCity.Remove(player);
                if (e.ArrivedCity == this)
                    playersInCity.Add(player);
            }
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
