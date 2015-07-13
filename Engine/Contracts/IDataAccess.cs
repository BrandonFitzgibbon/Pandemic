using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IDataAccess
    {
        IList<IDisease> GetDiseases();
        IList<ICity> GetCities();
        IList<IDiseaseCounter> GetCounters();
        IPlayerDeck GetPlayerDeck();
        IInfectionDeck GetInfectionDeck();
        string ResolveCityDisease(ICity city);
        IList<string> ResolveCityConnections(ICity city);
    }
}
