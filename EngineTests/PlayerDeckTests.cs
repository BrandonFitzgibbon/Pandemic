using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Engine.Contracts;
using Engine.Implementations;

namespace EngineTests
{
    [TestClass]
    public class PlayerDeckTests
    {
        private List<ICity> cities;
        private List<IPlayer> players;

        public class MockPlayer : Player, IPlayer
        {
            public MockPlayer(string name) : base(name) { }
        }

        [TestInitialize]
        public void Initialize()
        {
            cities = new List<ICity>()
            {
                //Yellow
                new City(1,"Bogota", "Colombia", 8702000),
                new City(2,"Buenos Aires", "Argentina", 1363900),
                new City(3,"Johannesburg", "South Africa", 3888000),
                new City(4,"Khartoum", "Sudan", 4887000),
                new City(5,"Kinshasa", "Democratic Republic of Congo", 9046000),
                new City(6,"Lagos", "Nigeria", 11547000),
                new City(7,"Lima", "Peru", 9121000),
                new City(8,"Los Angeles", "United States", 14900000),
                new City(10,"Mexico City", "Mexico", 19463000),
                new City(11,"Miami", "United States", 5582000),
                new City(12,"Santiago", "Chile", 6015000),
                new City(13,"Sao Paulo", "Brazil", 20186000),

                //Red
                new City(14,"Bangkok", "Thailand", 7151000),
                new City(15,"Beijing", "People's Republic of China", 17311000),
                new City(16,"Ho Chi Minh City", "Vietnam", 8314000),
                new City(17,"Hong Kong", "People's Republic of China", 7106000),
                new City(18,"Jakarta", "Indonesia", 23063000),
                new City(19,"Manila", "Philippines", 20767000),
                new City(20,"Osaka", "Japan", 2871000),
                new City(21,"Seoul", "South Korea", 22547000),
                new City(22,"Shanghai", "People's Republic of China", 13482000),
                new City(23,"Sydney", "Australia", 3785000),
                new City(24,"Taipei", "Taiwan", 8338000),
                new City(25,"Tokyo", "Japan", 13189000),

                //Blue
                new City(26,"Atlanta", "United States", 4715000),
                new City(27,"Chicago", "United States", 9121000),
                new City(28,"Essen", "Germany", 575000),
                new City(29,"London", "United Kingdom", 8586000),
                new City(30,"Madrid", "Spain", 5427000),
                new City(31,"Milan", "Italy", 5232000),
                new City(32,"Montreal", "Canada", 3429000),
                new City(33,"New York", "United States", 20464000),
                new City(34,"Paris", "France", 10755000),
                new City(35,"San Francisco", "United States", 5864000),
                new City(36,"St. Petersburg", "Russia", 4879000),
                new City(37,"Washington", "United States", 4679000),

                //Black
                new City(38,"Algiers", "Algeria", 2946000),
                new City(39,"Baghdad", "Iraq", 6204000),
                new City(40,"Cairo", "Egypt", 14718000),
                new City(41,"Chennai", "India", 8865000),
                new City(42,"Delhi", "India", 22242000),
                new City(43,"Istanbul", "Turkey", 13576000),
                new City(44,"Karachi", "Pakistan", 20711000),
                new City(45,"Kolkata", "India", 14374000),
                new City(46,"Moscow", "Russia", 15512000),
                new City(47,"Mumbai", "India", 16910000),
                new City(48,"Riyadh", "Saudi Arabia", 5037000),
                new City(49,"Tehran", "Iran", 7419000)
               
            };

            players = new List<IPlayer>()
            {
                new MockPlayer("Marc"),
                new MockPlayer("Brandon"),
                new MockPlayer("Jane"),
                new MockPlayer("John")
            };
        }

        [TestMethod]
        public void TestSetUp()
        {
            PlayerDeck pd = new PlayerDeck(cities);
            pd.Setup(players, 6);   
        }
    }
}
