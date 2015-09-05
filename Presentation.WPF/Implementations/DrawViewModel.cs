using Engine.Implementations;
using Presentation.WPF.Context;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.WPF.Implementations
{
    public class DrawViewModel : ViewModelBase, IDrawViewModel
    {
        private DrawManager drawManager;

        public DrawViewModel(DrawManager drawManager)
        {
            this.drawManager = drawManager;
        }

        private RelayCommand drawCommand;
        public ICommand DrawCommand
        {
            get
            {
                if (drawCommand == null)
                    drawCommand = new RelayCommand(m => Draw(), p => CanDraw());
                return drawCommand;
            }
        }

        private bool CanDraw()
        {
            return drawManager.CanDraw;
        }

        public void Draw()
        {
            drawManager.DrawPlayerCard();
            RaiseChangeNotificationRequested();
        }
    }
}
