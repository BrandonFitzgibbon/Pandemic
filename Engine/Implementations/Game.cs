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
        public IDeck PlayerDeck { get; private set; }
        public IDeck InfectionDeck { get; private set; }       
        public ICount OutbreakCounter { get; private set; }
        public IInfectionRateCounter InfectionRateCounter { get; private set; }

        private IEnumerable<ICityCard> cityCards;
        private IEnumerable<IInfectionCard> infectionCards;
        private IEnumerable<IEpidemicCard> epidemicCards;

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
            Players = PlayerFactory.GetPlayers(playerNames);

            SubscribeNodesToMovers();

            Implementations.OutbreakCounter outbreakCounter = new OutbreakCounter(NodeCounters);
            outbreakCounter.GameOver += GameOver;
            OutbreakCounter = outbreakCounter;

            InfectionRateCounter = new InfectionRateCounter();

            cityCards = GetCityCards(Nodes);
            infectionCards = GetInfectionCards(NodeCounters);            

            PlayerDeck = new PlayerDeck(cityCards.ToList());
            InfectionDeck = new InfectionDeck(infectionCards.ToList());

            epidemicCards = GetEpidemicCards((IIncrease)InfectionRateCounter, (IInfectionDeck)InfectionDeck);

            StartGame((int)difficulty);

            Players = Players.OrderBy(i => i.Hand.CityCards.OrderBy(j => j.Node.City.Population).First().Node.City.Population).ToList();
            this.playerQueue = new PlayerQueue(Players.ToList());

            NextPlayer();
        }

        private void StartGame(int diff)
        {
            SetStartingLocation();
            InitialInfection();
            InitialHands();
            IPlayerDeck playerDeck = (IPlayerDeck)PlayerDeck;
            playerDeck.AddEpidemics(diff, new Stack<IEpidemicCard>(epidemicCards.ToList()));
        }

        public void NextPlayer()
        {
            foreach (IPlayer player in playerQueue.NextPlayer)
            {
                currentPlayer = player;
            }
        }

        private IEnumerable<ICityCard> GetCityCards(IEnumerable<INode> nodes)
        {
            List<CityCard> cityCards = new List<CityCard>();
            foreach (INode node in nodes)
            {
                cityCards.Add(new CityCard(node));
            }
            return cityCards;
        }

        private IEnumerable<IInfectionCard> GetInfectionCards(IEnumerable<INodeDiseaseCounter> nodeDiseaseCounters)
        {
            List<InfectionCard> infectionCards = new List<InfectionCard>();
            foreach (INodeDiseaseCounter ndc in nodeDiseaseCounters)
            {
                if (ndc.Disease == ndc.Node.Disease)
                    infectionCards.Add(new InfectionCard(ndc));
            }
            return infectionCards;
        }

        private IEnumerable<IEpidemicCard> GetEpidemicCards(IIncrease infectionRateCounter, IInfectionDeck infectionDeck)
        {
            List<EpidemicCard> epidemicCards = new List<EpidemicCard>();
            for (int i = 0; i < 6; i++)
            {
                epidemicCards.Add(new EpidemicCard(infectionRateCounter, infectionDeck));
            }
            return epidemicCards;
        }

        private void SubscribeNodesToMovers()
        {
            foreach (INode node in Nodes)
            {
                foreach (IMove mover in Players)
                {
                    node.SubscribeToMover(mover);
                }
            }
        }

        private void SetStartingLocation()
        {
            INode atlanta = this.Nodes.Single(i => i.City.Name == "Atlanta");

            foreach (IMove mover in Players)
            {
                mover.Move(atlanta);
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

        private void InitialHands()
        {
            int i = 0;
            switch (Players.ToList().Count)
            {
                case 4:
                    i = 2;
                    break;
                case 3:
                    i = 1;
                    break;
                case 2:
                    i = 0;
                    break;
                default:
                    i = 4;
                    break;
            }

            //issue initial cards
            while (i < 4)
            {
                foreach (IPlayer player in Players)
                {
                    player.Hand.AddToHand(PlayerDeck.Draw());
                }
                i++;
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
