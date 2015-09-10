using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Presentation.WPF.Contracts
{
    public interface IPathAnimationViewModel : IViewModelBase
    {
        PathGeometry Data { get; set; }
        double Left { get; set; }
        double Top { get; set; }
        DoubleCollection StrokeArray { get; set; }
    }
}
