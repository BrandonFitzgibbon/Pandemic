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
        private IContext<DrawManager> drawManager;

        public DrawManager DrawManager
        {
            get { return drawManager != null ? drawManager.Context : null; }
        }

        public DrawViewModel(IContext<DrawManager> drawManager)
        {
            this.drawManager = drawManager;
        }

        private void ContextChanged(object sender, ContextChangedEventArgs<DrawManager> e)
        {
            NotifyPropertyChanged("DrawManager");
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
            return DrawManager.CanDraw;
        }

        public void Draw()
        {
            DrawManager.DrawPlayerCard();
            RaiseChangeNotificationRequested();
        }
    }
}
