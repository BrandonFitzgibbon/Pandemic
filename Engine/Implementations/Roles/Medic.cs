using Engine.Contracts;
using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    internal class Medic : Player
    {
        public Medic(string name) : base(name) { Role = "Medic"; }
    }
}
