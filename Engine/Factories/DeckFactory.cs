using Engine.Contracts;
using Engine.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    public class DeckFactory : IDeckFactory
    {
        private IList<ICity> cities;

        public IInfectionDeck GetInfectionDeck()
        {
            List<IInfectionCard> infectionCards = new List<IInfectionCard>();
            foreach (ICity city in this.cities)
            {
                infectionCards.Add(new InfectionCard(city));
            }
            return new InfectionDeck(infectionCards);
        }

        public IPlayerDeck GetPlayerDeck()
        {
            List<ICityCard> cityCards = new List<ICityCard>();
            foreach (ICity city in this.cities)
            {
                cityCards.Add(new CityCard(city));
            }
            return new PlayerDeck(cityCards);
        }

        public DeckFactory(IList<ICity> cities)
        {
            this.cities = cities;
        }
    }
}
