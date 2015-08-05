using Engine.Contracts;
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
        private static List<string> roles = new List<string>() { "Medic", "Dispatcher", "Scientist", "QuarantineSpecialist" };

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
                        players.Add(new Medic(name, new Hand()));
                        break;
                    case "Dispatcher":
                        players.Add(new Dispatcher(name, new Hand()));
                        break;
                    case "Scientist":
                        players.Add(new Scientist(name, new Hand()));
                        break;
                    case "QuarantineSpecialist":
                        players.Add(new QuarantineSpecialist(name, new Hand()));
                        break;
                }
            }
            return players;
        }
    }
}
