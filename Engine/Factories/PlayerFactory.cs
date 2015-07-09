using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        public IPlayer Get(string name, string role)
        {
            throw new NotImplementedException();
        }
    }
}
