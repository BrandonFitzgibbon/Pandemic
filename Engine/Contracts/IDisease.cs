using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IDisease
    {
        string Name { get; set; }
        DiseaseType Type { get; }
        int Count { get; }
        bool IsCured { get; }
        bool IsEradicated { get; }
        void Increase();
        void Decrease();
        event EventHandler GameOver;
    }

    public enum DiseaseType
    {
        Yellow, Red, Blue, Black
    }
}
