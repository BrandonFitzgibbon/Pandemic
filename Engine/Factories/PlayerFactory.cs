﻿using Engine.Contracts;
using Engine.Implementations;
using Engine.Implementations.Roles;
using Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    public static class PlayerFactory
    {
        private static List<string> roles = new List<string>() { "Medic", "Dispatcher", "Scientist", "QuarantineSpecialist", "OperationsExpert", "Researcher" };

        public static IEnumerable<Player> GetPlayers(IList<string> names)
        {
            List<Player> players = new List<Player>();
            roles.Shuffle();
            Stack<string> roleStack = new Stack<string>(roles);
            foreach (string name in names)
            {
                string role = roleStack.Pop();
                switch(role)
                {
                    case "Medic":
                        players.Add(new Medic(name));
                        break;
                    case "Dispatcher":
                        players.Add(new Dispatcher(name));
                        break;
                    case "Scientist":
                        players.Add(new Scientist(name));
                        break;
                    case "QuarantineSpecialist":
                        players.Add(new QuarantineSpecialist(name));
                        break;
                    case "OperationsExpert":
                        players.Add(new OperationsExpert(name));
                        break;
                    case "Researcher":
                        players.Add(new Researcher(name));
                        break;
                }
            }
            return players;
        }
    }
}
