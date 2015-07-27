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
        private bool isGameOn;
        public bool IsGameOn
        {
            get { return isGameOn; }
        }

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

        private PlayerQueue playerQueue;
        private IPlayer currentPlayer;
        public IPlayer CurrentPlayer
        {
            get { return currentPlayer; }
        }

        private IActions currentActions;
        public IActions CurrentActions
        {
            get { return currentActions; }
        }

        private DrawCounter currentDrawCounter;
        public DrawCounter CurrentDrawCounter
        {
            get { return currentDrawCounter; }
        }

        private IPlayerDeck playerDeck;
        public IPlayerDeck PlayerDeck
        {
            get { return playerDeck; }
        }

        private IInfectionDeck infectionDeck;
        public IInfectionDeck InfectionDeck
        {
            get { return infectionDeck; }
        }

        private IOutbreakCounter outbreakCounter;
        public IOutbreakCounter OutbreakCounter
        {
            get { return outbreakCounter; }
        }

        private IInfectionRateCounter infectionRateCounter;
        public IInfectionRateCounter InfectionRateCounter
        {
            get { return infectionRateCounter; }
        }

        public int NumberOfResearchStations
        {
            get { return cities.Where(i => i.HasResearchStation == true).Count(); }
        }

        public Game(IList<IDisease> diseases, IList<ICity> cities, IPlayerFactory playerFactory, IList<string> playerNames, IDeckFactory deckFactory, IOutbreakCounter outbreakCounter, IInfectionRateCounter infectionRateCounter, Difficulty difficulty)
        {
            this.diseases = diseases;
            this.cities = cities;
            this.outbreakCounter = outbreakCounter;
            this.infectionRateCounter = infectionRateCounter;
            this.players = playerFactory.GetPlayers(playerNames);
            this.infectionDeck = deckFactory.GetInfectionDeck();
            this.playerDeck = deckFactory.GetPlayerDeck();

            //adds epidemics to player deck and gives players initial hands
            this.playerDeck.Setup(this.players, (int)difficulty);

            //subscribes cities to plyaer movements
            this.cities.SubscribeCitiesToPlayerMovements(this.players);
            SetStartingLocation();

            this.players = players.OrderBy(i => i.Hand.CityCards.OrderBy(j => j.City.Population).First().City.Population).ToList();
            this.playerQueue = new PlayerQueue(this.players);
            SubscribeToGameOver();
            InitialInfection();
            isGameOn = true;
            NextPlayer();
        }

        public void NextPlayer()
        {
            foreach (IPlayer player in playerQueue.NextPlayer)
            {
                currentPlayer = player;
                currentActions = new Actions((Player)currentPlayer);
                currentDrawCounter = new DrawCounter(PlayerDeck, InfectionDeck, InfectionRateCounter, CurrentPlayer);
            }
        }

        private void SetStartingLocation()
        {
            ICity atlanta = this.cities.Single(i => i.Name == "Atlanta");

            foreach (IPlayer player in this.players)
            {
                player.SetStartingLocation(atlanta);
            }
        }

        private void InitialInfection()
        {
            //infect 3 cities with 3 counters each
            for (int i = 0; i < 3; i++)
            {
                IInfectionCard card = (IInfectionCard)InfectionDeck.Draw();
                card.Infect(3);
                card.Discard();
            }

            //infection 3 cities with 2 counters each
            for (int i = 0; i < 3; i++)
            {
                IInfectionCard card = (IInfectionCard)InfectionDeck.Draw();
                card.Infect(2);
                card.Discard();
            }

            //infection 3 cities with 1 counters each
            for (int i = 0; i < 3; i++)
            {
                IInfectionCard card = (IInfectionCard)InfectionDeck.Draw();
                card.Infect(1);
                card.Discard();
            }
        }

        private void SubscribeToGameOver()
        {
            //subscribe to disease counter game over
            foreach (IDisease disease in diseases)
            {
                disease.GameOver += GameOver;
            }

            //subscribe to outbreak counter game over
            outbreakCounter.GameOver += GameOver;
        }

        private void GameOver(object sender, EventArgs e)
        {
            isGameOn = false;
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
