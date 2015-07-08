using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine.Contracts;
using Engine.Implementations;
using System.Collections.Generic;

namespace EngineTests
{
    [TestClass]
    public class PlayerTests
    {
        public class MockPlayer : Player, IPlayer
        {
            public MockPlayer(string name) : base(name) { }
        }

        [TestMethod] //Connection exists between both cities
        public void DriveTestExpectPlayerLocationIsDestination()
        {
            ICity city = new City(1, "Montreal", "Canada", 1, null, new List<IDisease>());
            ICity destination = new City(2, "Toronto", "Canada", 2, null, new List<IDisease>());
            city.FormConnection(destination);
            IPlayer player = new MockPlayer("Brandon");
            player.Location = city;
            player.Drive(destination);
            Assert.AreSame(destination, player.Location);
        }

        [TestMethod] //Connection does not exist between both cities
        public void DriveTestExpectPlayerLocationIsUnchanged()
        {
            ICity city = new City(1, "Montreal", "Canada", 1, null, new List<IDisease>());
            ICity destination = new City(2, "Toronto", "Canada", 2, null, new List<IDisease>());
            IPlayer player = new MockPlayer("Brandon");
            player.Location = city;
            player.Drive(destination);
            Assert.AreSame(city, player.Location);
        }

        [TestMethod]
        public void DirectFlightTestExpectPlayerLocationIsDestination()
        {
            ICity city = new City(1, "Montreal", "Canada", 1, null, new List<IDisease>());
            ICity destination = new City(2, "Toronto", "Canada", 2, null, new List<IDisease>());
            IPlayer player = new MockPlayer("Brandon");
            player.Location = city;
            IDeck deck = new PlayerDeck(new List<ICity>(){destination});
            player.Hand.Draw(deck);
            player.DirectFlight(destination);
            Assert.AreSame(destination, player.Location);
        }
    }
}
