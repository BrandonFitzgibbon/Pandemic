﻿using Engine.Implementations;
using Presentation.WPF.Context;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Implementations
{
    public class PlayerViewModel : ViewModelBase, IPlayerViewModel
    {
        private Player player;

        public string Name
        {
            get { return player != null ? player.Name : null; }
        }

        public string Role
        {
            get { return player != null ? player.Role : null; }
        }

        public Node Location
        {
            get { return player != null ? player.Location : null; }
        }

        public PlayerViewModel(Player player)
        {
            this.player = player;
        }
    }
}