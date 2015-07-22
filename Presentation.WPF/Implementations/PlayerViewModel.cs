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
        public IPlayer Player
        {
            get { return player; }
        }

        public string Name
        {
            get { return player != null ? player.Name : null; }
        }

        public string Location
        {
            get { return (player != null && player.Location != null) ? player.Location.Name : null; }
        }

        public string Role
        {
            get { return player.GetType().Name; }
        }

        public PlayerViewModel(IPlayer player)
        {
            this.player = player;
        }
    }
}
