using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    internal class Scientist : Player
    {
        public Scientist(string name) : base(name) { Role = "Scientist"; }
    }
}
