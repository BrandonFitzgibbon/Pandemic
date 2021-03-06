﻿using Engine.Implementations;
using Presentation.WPF.Context;
using Presentation.WPF.Contracts;
using Presentation.WPF.Controls;
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
        public Player Player
        {
            get { return player; }
        }

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

        public int TurnOrder
        {
            get { return player != null ? player.TurnOrder : 0; }
        }

        private object pawn;
        public object Pawn
        {
            get { return pawn; }
        }

        public PlayerViewModel(Player player, Notifier notifier)
        {
            this.player = player;
            notifier.SubscribeToViewModel(this);

            Icons icons = new Icons();
            switch (player.Role)
            {
                case "Dispatcher":
                    pawn = icons["pawnPurple"];
                    break;
                case "Medic":
                    pawn = icons["pawnOrange"];
                    break;
                case "Scientist":
                    pawn = icons["pawnWhite"];
                    break;
                case "Quarantine Specialist":
                    pawn = icons["pawnDarkGreen"];
                    break;
                case "Researcher":
                    pawn = icons["pawnBrown"];
                    break;
                case "Operations Expert":
                    pawn = icons["pawnGreen"];
                    break;
            }
        }
    }
}
