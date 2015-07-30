using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IDisease
    {
        string Name { get; }
        DiseaseType Type { get; }
        bool IsCured { get; }
        bool IsEradicated { get; }
    }

    public enum DiseaseType
    {
        Yellow, Red, Blue, Black
    }
}
