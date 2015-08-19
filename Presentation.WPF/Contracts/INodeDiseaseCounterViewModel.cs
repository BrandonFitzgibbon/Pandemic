using Engine.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Contracts
{
    public interface INodeDiseaseCounterViewModel : IViewModelBase
    {
        Disease Disease { get; }
        int Count { get; }
    }
}
