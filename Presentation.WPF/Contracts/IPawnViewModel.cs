using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Presentation.WPF.Contracts
{
    public interface IPawnViewModel : IViewModelBase
    {
        void AnimateDrive(PathGeometry data);
    }
}
