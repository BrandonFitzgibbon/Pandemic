using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine.Contracts;
using Engine.Implementations;
using Engine.Infrastructure;
using System.Collections.Generic;

namespace EngineTests
{
    [TestClass]
    public class PlayerTests
    {
        private IPlayer player;

        public class MockPlayer : Player, IPlayer
        {
            public MockPlayer(string name) : base(name) { }
        }

        [TestInitialize]
        public void Initialize()
        {
            player = new MockPlayer("Brandon");
            INode node = new Node(new City(1, "Montreal", "Canada", 1), new Disease(1), null);
            node.BuildReasearchStation();
            player.Location = node;
        }

        [TestMethod]
        public void TestDriveExpectLocationChangeToDestination() //proper connection
        {
            
            INode destination = new Node(new City(2, "Toronto", "Canada", 2), new Disease(2), null);
            player.Location.FormConnection(destination);
            player.Drive(destination);
            Assert.AreSame(destination, player.Location);
        }

        [TestMethod]
        public void TestDriveExpectLocationNotToChange() //no connection
        {        
            INode destination = new Node(new City(2, "Toronto", "Canada", 2), new Disease(2), null);
            player.Drive(destination);
            Assert.AreNotSame(destination, player.Location);
        }

        [TestMethod]
        public void TestDirectFlightExpectLocationChangeToDestination() //card in hand
        {
            INode destination = new Node(new City(2, "Toronto", "Canada", 2), new Disease(2), null);
            IPlayerDeck deck = new PlayerDeck(new List<ICity>() { destination.City });
            player.Hand.Draw(deck);
            player.DirectFlight(destination);
            Assert.AreSame(destination, player.Location);
        }

        [TestMethod]
        public void TestCharterFlightExpectLocationChangedToDestination() //card in hand
        {
            INode destination = new Node(new City(2, "Toronto", "Canada", 2), new Disease(2), null);
            IPlayerDeck deck = new PlayerDeck(new List<ICity>() { player.Location.City });
            player.Hand.Draw(deck);
            player.CharterFlight(destination);
            Assert.AreSame(destination, player.Location);
        }

        [TestMethod]
        public void TestShuttleFlightExpectLocationChangedToDestination()
        {
            INode destination = new Node(new City(2, "Toronto", "Canada", 2), new Disease(2), null);
            destination.BuildReasearchStation();
            player.ShuttleFlight(destination);
            Assert.AreSame(destination, player.Location);
        }

        [TestMethod]
        public void TestBuildResearchStationExpectBuilt()
        {
            IPlayerDeck deck = new PlayerDeck(new List<ICity>() { player.Location.City });
            player.Hand.Draw(deck);
            player.Location.DestroyResearchStation();
            player.BuildResearchStation();
            Assert.IsTrue(player.Location.HasResearchStation);
        }
    }
}
