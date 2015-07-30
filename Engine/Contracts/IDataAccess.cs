using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IDataAccess
    {
        IEnumerable<IDisease> GetDiseases();
        IEnumerable<INode> GetNodes();
        IEnumerable<IDiseaseCounter> GetDiseaseCounters();
        IEnumerable<INodeDiseaseCounter> GetNodeDiseaseCounters();
    }
}
