using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IDiseaseCounter
    {
        IDisease Disease { get; }
        int Count { get; }
        void Increase(int rate);
        void Decrease(int rate);
        event EventHandler<OutbreakEventArgs> Outbreak;
    }
}
