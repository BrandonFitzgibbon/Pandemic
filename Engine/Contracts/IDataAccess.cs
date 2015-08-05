using Engine.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IDataAccess
    {
        IEnumerable<Disease> GetDiseases();
        IEnumerable<Node> GetNodes();
        IEnumerable<DiseaseCounter> GetDiseaseCounters();
        IEnumerable<NodeDiseaseCounter> GetNodeDiseaseCounters();
    }
}
