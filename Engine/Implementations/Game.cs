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
        private ActionManager actionManager;
        
        public IEnumerable<IDisease> Diseases { get; private set; }
        public IEnumerable<IDiseaseCounter> DiseaseCounters { get; private set; }

        private IEnumerable<NodeDiseaseCounter> nodeCounters;
        public IEnumerable<INodeDiseaseCounter> NodeCounters
        {
            get { return nodeCounters; }
        }

        private IEnumerable<Node> nodes;
        public IEnumerable<INode> Nodes
        {
            get { return nodes; }
        }

        private IEnumerable<Player> players;
        public IEnumerable<IPlayer> Players
        {
            get { return players; }
        }

        public ICount OutbreakCounter { get; private set; }
        public IInfectionRateCounter InfectionRateCounter { get; private set; }

        private IEnumerable<CityCard> cityCards;
        private IEnumerable<InfectionCard> infectionCards;
        private IEnumerable<EpidemicCard> epidemicCards;

        private PlayerDeck playerDeck;
        public IDeck PlayerDeck
        {
            get { return playerDeck; }
        }

        private InfectionDeck infectionDeck;
        public IDeck InfectionDeck
        {
            get { return infectionDeck; }
        }
 
        private PlayerQueue playerQueue;
        
        private Player currentPlayer;
        public IPlayer CurrentPlayer
        {
            get { return currentPlayer; }
        }

        public Game(IDataAccess dataAccess, IList<string> playerNames, Difficulty difficulty)
        {
            Diseases = dataAccess.GetDiseases();
            DiseaseCounters = dataAccess.GetDiseaseCounters();
            this.nodes = dataAccess.GetNodes();
            this.nodeCounters = dataAccess.GetNodeDiseaseCounters();
            players = PlayerFactory.GetPlayers(playerNames);

            SubscribeNodesToMovers();

            Implementations.OutbreakCounter outbreakCounter = new OutbreakCounter(nodeCounters);
            outbreakCounter.GameOver += GameOver;
            OutbreakCounter = outbreakCounter;

            InfectionRateCounter = new InfectionRateCounter();

            cityCards = GetCityCards(this.nodes);
            infectionCards = GetInfectionCards(this.nodeCounters);            

            this.playerDeck = new PlayerDeck(cityCards.ToList());
            this.infectionDeck = new InfectionDeck(infectionCards.ToList());

            epidemicCards = GetEpidemicCards((IIncrease)InfectionRateCounter, this.infectionDeck);

            StartGame((int)difficulty);

            this.playerQueue = new PlayerQueue(players.ToList());

            actionManager = new ActionManager();

            NextPlayer();
        }

        private void StartGame(int diff)
        {
            InitialInfection();
            InitialHands();
            SetStartingLocation();
            this.playerDeck.AddEpidemics(diff, new Stack<EpidemicCard>(epidemicCards.ToList()));
        }

        public void NextPlayer()
        {
            foreach (Player player in playerQueue.NextPlayer)
            {
                currentPlayer = player;
                currentPlayer.ActionCounter.ResetActions();
                actionManager.SetPlayer(currentPlayer, Nodes);
            }
        }

        private IEnumerable<CityCard> GetCityCards(IEnumerable<Node> nodes)
        {
            List<CityCard> cityCards = new List<CityCard>();
            foreach (Node node in nodes)
            {
                cityCards.Add(new CityCard(node));
            }
            return cityCards;
        }

        private IEnumerable<InfectionCard> GetInfectionCards(IEnumerable<NodeDiseaseCounter> nodeDiseaseCounters)
        {
            List<InfectionCard> infectionCards = new List<InfectionCard>();
            foreach (NodeDiseaseCounter ndc in nodeDiseaseCounters)
            {
                if (ndc.Disease == ndc.Node.Disease)
                    infectionCards.Add(new InfectionCard(ndc));
            }
            return infectionCards;
        }

        private IEnumerable<EpidemicCard> GetEpidemicCards(IIncrease infectionRateCounter, InfectionDeck infectionDeck)
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
            foreach (Node node in nodes)
            {
                foreach (Player player in players)
                {
                    node.SubscribeToMover(player);
                }
            }
        }

        private void SetStartingLocation()
        {
            Node atlanta = this.nodes.Single(i => i.City.Name == "Atlanta");

            foreach (Player player in players)
            {
                player.Move(atlanta);
            }

            atlanta.ResearchStation = true;
        }

        private void InitialInfection()
        {
            //infect 3 cities with 3 counters each
            for (int i = 0; i < 3; i++)
            {
                IInfectionCard card = (IInfectionCard)this.infectionDeck.Draw();
                card.Infect(3);
            }

            //infection 3 cities with 2 counters each
            for (int i = 0; i < 3; i++)
            {
                IInfectionCard card = (IInfectionCard)this.infectionDeck.Draw();
                card.Infect(2);
            }

            //infection 3 cities with 1 counters each
            for (int i = 0; i < 3; i++)
            {
                IInfectionCard card = (IInfectionCard)this.infectionDeck.Draw();
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
                    player.Hand.AddToHand(this.playerDeck.Draw());
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
        private Queue<Player> players;

        public PlayerQueue(IList<Player> players)
        {
            this.players = new Queue<Player>(players.OrderBy(i => i.Hand.CityCards.OrderBy(j => j.Node.City.Population).First().Node.City.Population).ToList());
        }

        public IEnumerable<Player> NextPlayer
        {
            get 
            {
                Player player = players.Dequeue();
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
