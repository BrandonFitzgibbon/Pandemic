using Engine.Contracts;
using Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class Game : IGame
    {
        private IList<IDisease> diseases;
        public IList<IDisease> Diseases
        {
            get { return diseases; }
        }

        private IList<ICity> cities;
        public IEnumerable<ICity> Cities
        {
            get { return cities; }
        }

        private IList<IPlayer> players;
        public IList<IPlayer> Players
        {
            get { return players; }
        }

        private PlayerDeck playerDeck;
        public PlayerDeck PlayerDeck
        {
            get { return playerDeck; }
        }

        public Game(IList<string> playerNames, Difficulty difficulty)
        {
           
        }

    }

    public enum Difficulty
    {
        Introductory = 4, Standard = 5, Heroic = 6
    }
}
