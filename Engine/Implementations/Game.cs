using Engine.Contracts;
using Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Engine.Implementations
{
    public class Game : IGame
    {
        private IList<IDisease> diseases;
        public IEnumerable<IDisease> Diseases
        {
            get { return diseases; }
        }

        private IList<ICity> cities;
        public IEnumerable<ICity> Cities
        {
            get { return cities; }
        }

        private IList<IPlayer> players;
        public IEnumerable<IPlayer> Players
        {
            get { return players; }
        }

        private IPlayerDeck playerDeck;
        public IPlayerDeck PlayerDeck
        {
            get { return playerDeck; }
        }

        public int NumberOfResearchStations
        {
            get { return cities.Where(i => i.HasResearchStation == true).Count(); }
        }

        public Game(IDataAccess data, IPlayerFactory playerFactory, IList<string> playerNames, Difficulty difficulty)
        {
            this.diseases = data.GetDiseases();
            this.cities = data.GetCities();
            this.players = playerFactory.GetPlayers(playerNames);
            this.cities.InitalizeCities(this, data);
        }

    }

    public enum Difficulty
    {
        Introductory = 4, Standard = 5, Heroic = 6
    }
}
