using Engine.Contracts;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Implementations
{
    public class PlayerViewModel : ViewModelBase, IPlayerViewModel
    {
        private IPlayer player;

        public string Name
        {
            get { return player != null ? player.Name : null; }
        }

        public string Location
        {
            get { return (player != null && player.Location != null) ? player.Location.Name : null; }
        }

        public PlayerViewModel(IPlayer player)
        {
            this.player = player;
        }

        public void NotifyChanges()
        {
            foreach (PropertyInfo prop in this.GetType().GetProperties())
            {
                NotifyPropertyChanged(prop.Name);
            }
        }
    }
}
