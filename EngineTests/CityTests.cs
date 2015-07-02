using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Engine.Implementations;
using Engine.Contracts;
using System.Collections.Generic;

namespace EngineTests
{
    [TestClass]
    public class CityTests
    {
        [TestMethod]
        public void TestEqualsSameNameSameCountryExpectTrue()
        {
            City cityOne = new City("CityOne", "CountryOne", 1);
            City cityOneClone = new City("CityOne", "CountryOne", 2);
            Assert.IsTrue(cityOne.Equals(cityOneClone));
        }

        [TestMethod]
        public void TestEqualsSameNameDifferentCountryExpectFalse()
        {
            City cityOne = new City("CityOne", "CountryOne", 1);
            City cityTwo = new City("CityOne", "CountryTwo", 2);
            Assert.IsFalse(cityOne.Equals(cityTwo));
        }

        [TestMethod]
        public void TestEqualsDifferentNameDifferentCountryExpectFalse()
        {
            City cityOne = new City("CityOne", "CountryOne", 1);
            City cityTwo = new City("CityTwo", "CountryTwo", 2);
            Assert.IsFalse(cityOne.Equals(cityTwo));
        }

        [TestMethod]
        public void TestContainsInListExpectTrue()
        {
            City cityOne = new City("CityOne", "CountryOne", 1);
            City cityOneClone = new City("CityOne", "CountryOne", 2);
            List<City> cities = new List<City>();
            cities.Add(cityOne);
            Assert.IsTrue(cities.Contains(cityOneClone));
        }
    }
}
