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
        private bool IsGameOn;

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

        public Game(IDataAccess data, IPlayerFactory playerFactory, IList<string> playerNames, IOutbreakCounter outbreakCounter, IInfectionRateCounter infectionRateCounter, Difficulty difficulty)
        {
            this.diseases = data.GetDiseases();
            this.cities = data.GetCities();
            this.outbreakCounter = outbreakCounter;
            this.infectionRateCounter = infectionRateCounter;
            this.players = playerFactory.GetPlayers(playerNames);
            this.cities.InitalizeCities(this, data);
            SetStartingLocation();
            this.infectionDeck = data.GetInfectionDeck();
            this.playerDeck = data.GetPlayerDeck();
            this.playerDeck.Setup(this.players, (int)difficulty);
            this.players = players.OrderBy(i => i.Hand.CityCards.OrderBy(j => j.City.Population).First().City.Population).ToList();
            SubscribeToOutbreak();
            SubscribeToGameOver();
            InitialInfection();
            GameLoop();
        }

        private void GameLoop()
        {
            do
            {
                foreach (IPlayer player in this.players)
                {

                }
            } while (IsGameOn);
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
                ICity city = card.City;
                IDiseaseCounter counter = city.Counters.Single(j => j.Disease == city.Disease);
                counter.Increase();
                counter.Increase();
                counter.Increase();
                card.Discard();
            }

            //infection 3 cities with 2 counters each
            for (int i = 0; i < 3; i++)
            {
                IInfectionCard card = (IInfectionCard)InfectionDeck.Draw();
                ICity city = card.City;
                IDiseaseCounter counter = city.Counters.Single(j => j.Disease == city.Disease);
                counter.Increase();
                counter.Increase();
                card.Discard();
            }

            //infection 3 cities with 1 counters each
            for (int i = 0; i < 3; i++)
            {
                IInfectionCard card = (IInfectionCard)InfectionDeck.Draw();
                ICity city = card.City;
                IDiseaseCounter counter = city.Counters.Single(j => j.Disease == city.Disease);
                counter.Increase();
                card.Discard();
            }
        }

        private void SubscribeToOutbreak()
        {
            foreach (ICity city in this.cities)
            {
                foreach (IDiseaseCounter diseaseCounter in city.Counters)
                {
                    diseaseCounter.Outbreak += DiseaseCounterOutbreak;
                }
            }
        }

        private void DiseaseCounterOutbreak(object sender, EventArgs e)
        {
            outbreakCounter.Increase();
        }

        private void SubscribeToGameOver()
        {
            //subscribe to disease counter game over
            foreach (IDisease disease in diseases)
            {
                disease.GameOver += GameOver;
            }

            outbreakCounter.GameOver += GameOver;
        }

        private void GameOver(object sender, EventArgs e)
        {
            IsGameOn = false;
        }
    }

    public enum Difficulty
    {
        Introductory = 4, Standard = 5, Heroic = 6
    }
}
