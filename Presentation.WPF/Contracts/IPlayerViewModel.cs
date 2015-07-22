using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Contracts
{
    public interface IPlayerViewModel : IViewModelBase
    {
        IPlayer Player { get; }
        string Name { get; }
        string Location { get; }
        string Role { get; }
    }
}
