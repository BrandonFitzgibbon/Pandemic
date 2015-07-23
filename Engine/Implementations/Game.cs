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
            }
        }

        public void DrawPhase()
        {
            ICard drawn;
            currentPlayer.Hand.Draw(playerDeck, out drawn);
            if (drawn is IEpidemicCard)
            {
                infectionRateCounter.Increase();
                IInfectionCard infectionCard = (IInfectionCard)infectionDeck.DrawBottom();
                infectionCard.Infect(3);
                infectionCard.Discard();
                infectionDeck.Intensify();
            }
        }

        public void InfectionPhase()
        {
            for (int i = 0; i < infectionRateCounter.InfectionRate; i++)
            {
                ICard card = infectionDeck.Draw();
                if(card is IInfectionCard)
                {
                    IInfectionCard iCard = (IInfectionCard)card;
                    iCard.Infect(1);
                }
                card.Discard();
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
