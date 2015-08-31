using Engine.Contracts;
using Engine.Factories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Implementations
{
    public class Game
    {
        public ActionManager ActionManager { get; private set; }
        public DrawManager DrawManager { get; private set; }
        public InfectionManager InfectionManager { get; private set; }
        public IEnumerable<Disease> Diseases { get; private set; }
        public IEnumerable<DiseaseCounter> DiseaseCounters { get; private set; }
        public IEnumerable<NodeDiseaseCounter> NodeCounters { get; private set; }
        public IEnumerable<Node> Nodes { get; private set; }
        public IEnumerable<Player> Players { get; private set; }
        public OutbreakCounter OutbreakCounter { get; private set; }
        public InfectionRateCounter InfectionRateCounter { get; private set; }
        public ResearchStationCounter ResearchStationCounter { get; private set; }

        private IEnumerable<CityCard> cityCards;
        private IEnumerable<InfectionCard> infectionCards;
        private IEnumerable<EpidemicCard> epidemicCards;

        private PlayerDeck playerDeck;
        private InfectionDeck infectionDeck;
 
        private PlayerQueue playerQueue;
        public bool CanNextPlayer { get; private set; }

        public Player CurrentPlayer { get; private set; }

        private CureTracker CureTracker;

        public Game(IDataAccess dataAccess, IList<string> playerNames, Difficulty difficulty)
        {
            OutbreakCounter = new OutbreakCounter();

            Diseases = dataAccess.GetDiseases();
            Nodes = dataAccess.GetNodes();
            Players = PlayerFactory.GetPlayers(playerNames);

            NodeCounters = GetNodeDiseaseCounter(Nodes, Diseases, OutbreakCounter);
            DiseaseCounters = GetDiseaseCounters(Diseases, NodeCounters, OutbreakCounter);

            SubscribeNodesToMovers();
            SubscribeToPlayerCounters();

            InfectionRateCounter = new InfectionRateCounter();
            ResearchStationCounter = new ResearchStationCounter();

            cityCards = GetCityCards(Nodes);
            infectionCards = GetInfectionCards(NodeCounters);

            playerDeck = new PlayerDeck(cityCards.ToList());
            infectionDeck = new InfectionDeck(infectionCards.ToList());

            epidemicCards = GetEpidemicCards(InfectionRateCounter, infectionDeck);

            StartGame((int)difficulty);

            playerQueue = new PlayerQueue(Players.ToList());
            Players = Players.OrderBy(i => i.TurnOrder);

            ActionManager = new ActionManager();
            DrawManager = new DrawManager(this.playerDeck);
            InfectionManager = new InfectionManager(InfectionRateCounter, infectionDeck);
            InfectionManager.InfectionPhaseCompleted += InfectionPhaseCompleted;

            //game over
            OutbreakCounter.GameOver += GameOverNotified;
            playerDeck.GameOver += GameOverNotified;

            foreach (DiseaseCounter dc in DiseaseCounters)
            {
                dc.GameOver += GameOverNotified;
            }

            //game won
            CureTracker = new CureTracker(Diseases);
            CureTracker.GameWon += GameWonNotified;

            NextPlayer();
        }

        public event EventHandler GameOver;

        private void GameOverNotified(object sender, EventArgs e)
        {
            if (GameOver != null) GameOver(this, e);
            InfectionManager.gameOver = true;
        }

        public event EventHandler GameWon;

        private void GameWonNotified(object sender, EventArgs e)
        {
            if (GameWon != null) GameWon(this, e);
        }

        private void StartGame(int diff)
        {
            InitialInfection();
            InitialHands();
            SetStartingLocation();
            playerDeck.AddEpidemics(diff, new Stack<EpidemicCard>(epidemicCards.ToList()));
        }

        public void NextPlayer()
        {
            foreach (Player player in playerQueue.NextPlayer)
            {
                CanNextPlayer = false;
                CurrentPlayer = player;
                CurrentPlayer.ActionCounter.ResetActions();
                ActionManager.SetPlayer(CurrentPlayer, Players, Nodes, NodeCounters, ResearchStationCounter, Diseases);
            }
        }

        private void ActionsDepleted(object sender, EventArgs e)
        {
            CurrentPlayer.DrawCounter.ResetDraws();
            DrawManager.SetPlayer(CurrentPlayer);
        }

        private void DrawsDepleted(object sender, EventArgs e)
        {
            InfectionManager.ResetInfectionManager();
        }

        private void InfectionPhaseCompleted(object sender, EventArgs e)
        {
            CanNextPlayer = true;
        }

        private IEnumerable<DiseaseCounter> GetDiseaseCounters(IEnumerable<Disease> diseases, IEnumerable<NodeDiseaseCounter> nodeCounters, OutbreakCounter outbreakCounter)
        {
            List<DiseaseCounter> diseaseCounters = new List<DiseaseCounter>();
            foreach (Disease disease in diseases)
            {
                diseaseCounters.Add(new DiseaseCounter(disease, nodeCounters.Where(i => i.Disease == disease).ToList(), outbreakCounter));
            }
            return diseaseCounters;
        }

        private IEnumerable<NodeDiseaseCounter> GetNodeDiseaseCounter(IEnumerable<Node> nodes, IEnumerable<Disease> diseases, OutbreakCounter outbreakCounter)
        {
            List<NodeDiseaseCounter> nodeDiseaseCounters = new List<NodeDiseaseCounter>();
            foreach (Disease disease in diseases)
            {
                foreach (Node node in nodes)
                {
                    NodeDiseaseCounter ndc = new NodeDiseaseCounter(disease, node);
                    nodeDiseaseCounters.Add(ndc);
                    outbreakCounter.SubcribeToNodeDiseaseCounter(ndc);
                }
            }
            return nodeDiseaseCounters;
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

        private IEnumerable<EpidemicCard> GetEpidemicCards(InfectionRateCounter infectionRateCounter, InfectionDeck infectionDeck)
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
            foreach (Node node in Nodes)
            {
                foreach (Player player in Players)
                {
                    node.SubscribeToMover(player);
                }
            }
        }

        private void SubscribeToPlayerCounters()
        {
            foreach (Player player in Players)
            {
                player.ActionCounter.ActionsDepleted += ActionsDepleted;
                player.DrawCounter.DrawsDepleted += DrawsDepleted;
            }
        }

        private void SetStartingLocation()
        {
            Node atlanta = Nodes.Single(i => i.City.Name == "Atlanta");

            foreach (Player player in Players)
            {
                player.Move(atlanta);
            }

            ResearchStationCounter.BuildResearchStation(atlanta);
        }

        private void InitialInfection()
        {
            //infect 3 cities with 3 counters each
            for (int i = 0; i < 3; i++)
            {
                InfectionCard card = this.infectionDeck.Draw();
                card.Infect(3);
            }

            //infection 3 cities with 2 counters each
            for (int i = 0; i < 3; i++)
            {
                InfectionCard card = this.infectionDeck.Draw();
                card.Infect(2);
            }

            //infection 3 cities with 1 counters each
            for (int i = 0; i < 3; i++)
            {
                InfectionCard card = this.infectionDeck.Draw();
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
                foreach (Player player in Players)
                {
                    player.Hand.AddToHand(playerDeck.Draw());
                }
                i++;
            }
        }
    }

    public class PlayerQueue
    {
        private Queue<Player> players;

        public PlayerQueue(IList<Player> players)
        {
            List<Player> temp = new List<Player>(players.OrderBy(i => i.Hand.CityCards.OrderByDescending(j => j.Node.City.Population).First().Node.City.Population));
            this.players = new Queue<Player>();
            int w = 1;
            foreach (Player player in temp)
            {
                player.TurnOrder = w;
                w++;
                this.players.Enqueue(player);
            }
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
