using System;
using System.Linq;
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
            ICity city = new City("Montreal", "Canada", 1, null);
            ICity destination = new City("Toronto", "Canada", 2, null);
            city.FormConnection(destination);
            IPlayer player = new MockPlayer("Brandon");
            player.Location = city;
            player.Drive(destination);
            Assert.AreSame(destination, player.Location);
        }

        [TestMethod] //Connection does not exist between both cities
        public void DriveTestExpectPlayerLocationIsUnchanged()
        {
            ICity city = new City("Montreal", "Canada", 1, null);
            ICity destination = new City("Toronto", "Canada", 2, null);
            IPlayer player = new MockPlayer("Brandon");
            player.Location = city;
            player.Drive(destination);
            Assert.AreSame(city, player.Location);
        }

        [TestMethod] //Destination card exists in hand
        public void DirectFlightTestExpectPlayerLocationIsDestination()
        {
            ICity city = new City("Montreal", "Canada", 1, null);
            ICity destination = new City("Toronto", "Canada", 2, null);
            IPlayer player = new MockPlayer("Brandon");
            player.Location = city;
            IDeck deck = new PlayerDeck(new List<ICity>(){destination});
            player.Hand.Draw(deck);
            ICityCard destinationCard = player.Hand.CityCards.Single(i => i.City == destination);
            player.DirectFlight(destination, destinationCard);
            Assert.AreSame(destination, player.Location);
        }

        [TestMethod] //Location card exists in hand
        public void CharterFlightTestExpectPlayerLocationIsDestination()
        {
            ICity city = new City("Montreal", "Canada", 1, null);
            ICity destination = new City("Toronto", "Canada", 2, null );
            IPlayer player = new MockPlayer("Brandon");
            player.Location = city;
            IDeck deck = new PlayerDeck(new List<ICity>() { city });
            player.Hand.Draw(deck);
            ICityCard locationCard = player.Hand.CityCards.Single(i => i.City == city);
            player.CharterFlight(destination, locationCard);
            Assert.AreSame(destination, player.Location);
        }
    }
}
