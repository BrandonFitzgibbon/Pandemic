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
        public IEnumerable<ICounter> Counters
        {
            get { return counters; }
        }

        private IList<IPlayer> playersInCity;
        public IEnumerable<IPlayer> PlayersInCity
        {
            get { return playersInCity; }
        }

        private bool hasResearchStation;
        public bool HasResearchStation
        {
            get { return hasResearchStation; }
        }

        public City(string name, string country, int population)
        {
            this.name = name;
            this.country = country;
            this.population = population;
            this.playersInCity = new List<IPlayer>();
            hasResearchStation = false;
        }

        public void InitializeGame(IGame game, IDataAccess data)
        {
            //resolve disease
            this.disease = game.Diseases.Single(i => i.Name == data.ResolveCityDisease(this));
            
            //resolve connections
            connections = new List<ICity>();
            foreach (string cityName in data.ResolveCityConnections(this))
            {
                connections.Add(game.Cities.Single(i => i.Name == cityName));
            }
            
            //create counters
            counters = new List<ICounter>();
            this.counters = data.GetCounters();

            //subscribe to players
            foreach (IPlayer player in game.Players)
            {
                player.Moved += playerMoved;
                player.ResearchStationChanged += playerResearchStationChanged;
            }

            //build research station in atlanta
            if (this.name == "Atlanta")
                hasResearchStation = true;
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
