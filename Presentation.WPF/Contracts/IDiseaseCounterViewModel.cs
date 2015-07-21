using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Contracts
{
    public interface IDiseaseCounterViewModel : IViewModelBase
    {
        ICity City { get; }
        IDisease Disease { get; }
        int Count { get; }
    }
}
