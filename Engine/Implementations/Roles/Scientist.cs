using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class Scientist : Player, IScientist
    {
        public Scientist(string name, IHand hand) : base(name, hand) { }
    }
}
