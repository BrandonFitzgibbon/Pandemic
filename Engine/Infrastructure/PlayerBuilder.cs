using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Infrastructure
{
    public static class PlayerBuilder
    {
        public IList<IPlayer> BuildPlayers(IList<string> names)
        {
            IList<IPlayer> players = new List<IPlayer>();
            List<string> roles = new List<string>() { "Contingency Planner", "Dispatcher", "Medic", "Operations Expert", "Quarantine Specialist", "Researcher", "Scientist" };
            roles.Shuffle();
            Stack<string> randomRoles = new Stack<string>(roles);
            foreach (string name in names)
            {
                string role = randomRoles.Pop();
                switch(role)
                {
                    case "Contigency Planner":

                        break;
                    case "Dispatcher":

                        break;
                    case "Medic":

                        break;
                    case "Operations Expert":

                        break;
                    case "Quarantine Specialist":

                        break;
                    case "Researcher":

                        break;
                    case "Scientist":

                        break;
                }
            }
            return players;
        }
    }
}
