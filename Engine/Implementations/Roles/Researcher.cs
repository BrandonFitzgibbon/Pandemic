using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.Roles
{
    internal class Researcher : Player
    {
        public Researcher(string name) : base(name) { Role = "Researcher"; }
    }
}
