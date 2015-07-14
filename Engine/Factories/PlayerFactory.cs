using Engine.Contracts;
using Engine.Implementations;
using Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private List<string> roles = new List<string>() { "Medic", "Dispatcher", "Scientist" };

        public IList<IPlayer> GetPlayers(IList<string> names)
        {
            List<IPlayer> players = new List<IPlayer>();
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
                }
            }
            return players;
        }
    }
}
