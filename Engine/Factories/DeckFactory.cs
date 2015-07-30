using Engine.Contracts;
using Engine.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    public static class DeckFactory
    {
        public static IInfectionDeck GetInfectionDeck(IEnumerable<INodeDiseaseCounter> nodeDiseaseCounters)
        {
            List<IInfectionCard> infectionCards = new List<IInfectionCard>();
            foreach (INodeDiseaseCounter nodeDiseaseCounter in nodeDiseaseCounters)
            {
                infectionCards.Add(new InfectionCard(nodeDiseaseCounter));
            }
            return new InfectionDeck(infectionCards);
        }

        public static IPlayerDeck GetPlayerDeck(IEnumerable<INode> nodes)
        {
            List<ICityCard> cityCards = new List<ICityCard>();
            foreach (INode node in nodes)
            {
                cityCards.Add(new CityCard(node.City));
            }
            return new PlayerDeck(cityCards);
        }
    }
}
