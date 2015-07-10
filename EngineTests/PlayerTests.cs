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

        public class MockData : IDataAccess
        {
            public IList<IDisease> GetDiseases()
            {
                return new List<IDisease>()
                {
                    new Disease("Default"),
                };
            }

            public IList<ICity> GetCities()
            {
                return new List<ICity>()
                {
                    new City("Montreal", "Canada", 0),
                    new City("Toronto", "Canada", 0),
                    new City("London", "United Kingdom", 0)
                };
            }

            public IList<ICounter> GetCounters()
            {
                return new List<ICounter>
                {
                    new Counter(GetDiseases().First())
                };
            }

            public string ResolveCityDisease(ICity city)
            {
                return "Default";
            }

            public IList<string> ResolveCityConnections(ICity city)
            {
                switch(city.Name)
                {
                    case "Montreal":
                        return new List<string>() { "Toronto" };
                    case "Toronto":
                        return new List<string>() { "Montreal" };
                }
                return new List<string>();
            }
        }

        public class MockPlayer : Player, IPlayer
        {
            public MockPlayer(string name) : base(name) { }
            
            public void SetLocation(ICity city)
            {
                base.location = city;
            }
        }

        public class MockPlayerFactory : IPlayerFactory
        {
            public IList<IPlayer> GetPlayers(IList<string> names)
            {
                return new List<IPlayer>()
                {
                    new MockPlayer("John"),
                    new MockPlayer("Jane"),
                };
            }
        }

        private IGame game;

        [TestInitialize]
        public void Initialize()
        {
            this.game = new Game(new MockData(), new MockPlayerFactory(), new List<string> { "John", "Jane" }, Difficulty.Standard);
            foreach (IPlayer player in game.Players)
            {
                MockPlayer mp = (MockPlayer)player;
                mp.SetLocation(game.Cities.Single(i => i.Name == "Montreal"));
            }
        }

        [TestMethod] //Connection exists between both cities
        public void DriveTestExpectPlayerLocationIsDestination()
        {
            IPlayer player = game.Players.Single(i => i.Name == "John");
            ICity city = game.Cities.Single(i => i.Name == "Toronto");
            player.Drive(city);
            Assert.AreSame(city, player.Location);
            Assert.IsTrue(city.PlayersInCity.Contains(player));
        }

        [TestMethod] //Connection does not exist between both cities
        public void DriveTestExpectPlayerLocationIsUnchanged()
        {
            IPlayer player = game.Players.Single(i => i.Name == "John");
            ICity original = game.Cities.Single(i => i.Name == "Montreal");
            ICity city = game.Cities.Single(i => i.Name == "London");
            player.Drive(city);
            Assert.AreSame(original, player.Location);
        }

        [TestMethod] //Destination card exists in hand
        public void DirectFlightTestExpectPlayerLocationIsDestination()
        {

        }

        [TestMethod] //Location card exists in hand
        public void CharterFlightTestExpectPlayerLocationIsDestination()
        {

        }
    }
}
