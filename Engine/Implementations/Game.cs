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
        private IList<ICity> cities;
        private IList<IDisease> diseases;

        private IList<INode> nodes;
        public IList<INode> Nodes
        {
            get { return nodes; }
        }

        private IList<IPlayer> players;
        public IList<IPlayer> Players
        {
            get { return players; }
        }

        private IDeck playerDeck;
        public IDeck PlayerDeck
        {
            get { return playerDeck; }
        }

        private IDeck infectionDeck;
        public IDeck InfectionDeck
        {
            get { return infectionDeck; }
        }

        public Game(IList<INode> nodes, IList<ICity> cities, IList<IDisease> diseases, IList<string> playerNames, Difficulty difficulty)
        {
            this.nodes = nodes;
            this.cities = cities;
            this.diseases = diseases;
            this.players = PlayerBuilder.BuildPlayers(playerNames);
        }
    }

    public enum Difficulty
    {
        Introductory = 4, Standard = 5, Heroic = 6
    }
}
