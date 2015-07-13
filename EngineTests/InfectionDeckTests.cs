using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Engine.Contracts;
using Engine.Implementations;

namespace EngineTests
{
    [TestClass]
    public class InfectionDeckTests
    {
        [TestMethod]
        public void TestBottomDraw()
        {
            List<ICity> cities = new List<ICity>()
            {
                new City("Montreal", "Canada", 1),
                new City("Toronto", "Canada", 2),
                new City("Ottawa", "Canada", 3),
                new City("Vancouver", "Canada", 4),
                new City("New York", "United States", 5)
            };

            List<IInfectionCard> infectionCards = new List<IInfectionCard>()
            {
                new InfectionCard(cities[0]),
                new InfectionCard(cities[1]),
                new InfectionCard(cities[2]),
                new InfectionCard(cities[3]),
                new InfectionCard(cities[4]),
            };

            IInfectionDeck deck = new InfectionDeck(infectionCards);
            ICard card = deck.DrawBottom();
        }
    }
}
