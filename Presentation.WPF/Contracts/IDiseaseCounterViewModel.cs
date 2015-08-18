using Engine.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Contracts
{
    public interface IDiseaseCounterViewModel : IViewModelBase
    {
        int Count { get; }
        Disease Disease { get; }
        bool IsEradicated { get; }
        bool IsCured { get; }
    }
}
