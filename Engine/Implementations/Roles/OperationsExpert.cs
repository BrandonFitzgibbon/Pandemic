using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.Roles
{
    internal class OperationsExpert : Player
    {
        public OperationsExpert(string name) : base(name) { Role = "Operations Expert"; }
    }
}
