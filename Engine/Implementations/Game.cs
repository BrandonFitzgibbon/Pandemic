using Engine.Contracts;
using Engine.Factories;
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
        public IEnumerable<IDisease> Diseases { get; private set; }
        public IEnumerable<IDiseaseCounter> DiseaseCounters { get; private set; }
        public IEnumerable<INode> Nodes { get; private set; }
        public IEnumerable<INodeDiseaseCounter> NodeCounters { get; private set; }
        public IEnumerable<IPlayer> Players { get; private set; }
        public IPlayerDeck PlayerDeck { get; private set; }
        public IInfectionDeck InfectionDeck { get; private set; }
        public ICount OutbreakCounter { get; private set; }
        public IInfectionRateCounter InfectionRateCounter { get; private set; }

        private PlayerQueue playerQueue;
        private IPlayer currentPlayer;
        public IPlayer CurrentPlayer
        {
            get { return currentPlayer; }
        }

        public Game(IDataAccess dataAccess, IList<string> playerNames, Difficulty difficulty)
        {
            Diseases = dataAccess.GetDiseases();
            DiseaseCounters = dataAccess.GetDiseaseCounters();
            Nodes = dataAccess.GetNodes();
            NodeCounters = dataAccess.GetNodeDiseaseCounters();
            Players = PlayerFactory.GetPlayers(playerNames, this);

            Implementations.OutbreakCounter outbreakCounter = new OutbreakCounter(NodeCounters);
            outbreakCounter.GameOver += GameOver;
            OutbreakCounter = outbreakCounter;

            InfectionRateCounter = new InfectionRateCounter();

            PlayerDeck = DeckFactory.GetPlayerDeck(Nodes);
            InfectionDeck = DeckFactory.GetInfectionDeck(NodeCounters.Where(i => i.Disease == i.Node.Disease));

            //adds epidemics to player deck and gives players initial hands
            PlayerDeck.Setup(Players.ToList(), (int)difficulty);

            Players = Players.OrderBy(i => i.Hand.CityCards.OrderBy(j => j.City.Population).First().City.Population).ToList();
            this.playerQueue = new PlayerQueue(Players.ToList());
            InitialInfection();
            NextPlayer();
        }

        public void NextPlayer()
        {
            foreach (IPlayer player in playerQueue.NextPlayer)
            {
                currentPlayer = player;
            }
        }

        private void SetStartingLocation()
        {
            INode atlanta = this.Nodes.Single(i => i.City.Name == "Atlanta");

            foreach (Player player in Players)
            {
                player.Location = atlanta;
            }
        }

        private void InitialInfection()
        {
            //infect 3 cities with 3 counters each
            for (int i = 0; i < 3; i++)
            {
                IInfectionCard card = (IInfectionCard)InfectionDeck.Draw();
                card.Infect(3);
            }

            //infection 3 cities with 2 counters each
            for (int i = 0; i < 3; i++)
            {
                IInfectionCard card = (IInfectionCard)InfectionDeck.Draw();
                card.Infect(2);
            }

            //infection 3 cities with 1 counters each
            for (int i = 0; i < 3; i++)
            {
                IInfectionCard card = (IInfectionCard)InfectionDeck.Draw();
                card.Infect(1);
            }
        }

        private void GameOver(object sender, EventArgs e)
        {
            
        }
    }

    public class PlayerQueue
    {
        private Queue<IPlayer> players;

        public PlayerQueue(IList<IPlayer> players)
        {
            this.players = new Queue<IPlayer>(players);
        }

        public IEnumerable<IPlayer> NextPlayer
        {
            get 
            {
                IPlayer player = players.Dequeue();
                players.Enqueue(player);
                yield return player;
            }
        }
    }

    public enum Difficulty
    {
        Introductory = 4, Standard = 5, Heroic = 6
    }
}
